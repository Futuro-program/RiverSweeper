using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LogicaAnzol : MonoBehaviour
{
    public Vector3 velocidade;
    public float accel = -1;
    public float massa = 1;
    public bool travado;
    [SerializeField] AudioClip somSplash;
    [SerializeField] float volume = 2;
    const float ACCELGRAVIDADE = 1;
    Rigidbody corpoRigido;
    bool submergido;
    
    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (travado)
            corpoRigido.constraints = RigidbodyConstraints.FreezeAll;
        else
            corpoRigido.constraints = RigidbodyConstraints.FreezePositionZ;
    }

    void FixedUpdate()
    {
        if (transform.position.y < -300)
        {
            velocidade = Vector3.zero;
        }

        if (submergido)
        {
            float empuxo = volume;
            float peso = massa;
            float resultante = (empuxo - peso) * ACCELGRAVIDADE;
            accel = resultante / massa;
            velocidade -= velocidade * 0.2f;
        }
        else
        {
            accel = -ACCELGRAVIDADE;
        }

        Vector3 velAnterior = new(velocidade.x, velocidade.y, velocidade.z);
        velocidade.y += accel;

        corpoRigido.MovePosition(corpoRigido.position + Time.deltaTime / 2 * (velocidade + velAnterior));
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Lixo"))
        {
            outro.GetComponent<MovimentoLixo>();
        }
        else if (outro.gameObject.CompareTag("Water") && !travado)
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
