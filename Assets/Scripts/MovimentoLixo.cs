using UnityEngine;
using Assets.Scripts.Estruturas;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
public class MovimentoLixo : MonoBehaviour
{
    [SerializeField] float valor, peso;
    [SerializeField] string tipo;
    Rigidbody corpoRigido;
    Lixo lixo;

    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));

        lixo = new(valor, peso, tipo);
    }

    void Update() {
        if (transform.position.y > 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        else if (transform.position.y < -10)
            corpoRigido.velocity += corpoRigido.velocity.y * 2 * Vector3.down;

        if (Mathf.Abs(transform.position.x) > 50)
            corpoRigido.velocity += corpoRigido.velocity.x * 2 * Vector3.left;
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Player"))
        {
            Global.inst.PegarLixo(lixo);
            Destroy(gameObject);
        }
    }
}
