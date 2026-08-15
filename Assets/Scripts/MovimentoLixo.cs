using UnityEngine;
using Assets.Scripts.Estruturas;

[RequireComponent(typeof(Rigidbody))]
public class MovimentoLixo : MonoBehaviour
{
    [SerializeField] AudioClip somSplash;
    [SerializeField] float valor, massa, volume;
    [SerializeField] string tipo;
    Rigidbody corpoRigido;
    const float ACCELGRAVIDADE = 10;
    Lixo lixo;
    float accel;
    bool submergido;

    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

        lixo = new(valor, massa, volume, tipo);
    }

    void FixedUpdate()
    {
        if (corpoRigido.position.y < -300)
        {
            corpoRigido.MovePosition(new Vector3(corpoRigido.position.x, -300));
            corpoRigido.AddForce(2 * corpoRigido.velocity.y * Vector3.down, ForceMode.VelocityChange);
            return;
        }

        if (submergido)
        {
            float empuxo = volume;
            float peso = massa;
            float resultante = (empuxo - peso) * ACCELGRAVIDADE;
            accel = resultante / massa;
            corpoRigido.drag = 1.5f;
        }
        else
        {
            accel = -ACCELGRAVIDADE;
            corpoRigido.drag = 0.02f;
        }

        corpoRigido.AddForce(new Vector3(0, accel), ForceMode.Acceleration);
    }

    /*void Update() {
        if (transform.position.y > 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        else if (transform.position.y < -10)
            corpoRigido.velocity += corpoRigido.velocity.y * 2 * Vector3.down;

        if (Mathf.Abs(transform.position.x) > 50)
            corpoRigido.velocity += corpoRigido.velocity.x * 2 * Vector3.left;
    }*/

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            Global.inst.PegarLixo(lixo);
            Destroy(gameObject);
        }
        else if (outro.gameObject.CompareTag("Water"))
            Audio.inst.TocarAudio(somSplash);
    }

    void OnTriggerStay(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
            submergido = true;
    }

    void OnTriggerExit(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
            submergido = false;
    }
}
