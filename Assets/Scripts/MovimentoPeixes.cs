using UnityEngine;
using Assets.Scripts.Estruturas;

[RequireComponent(typeof(Rigidbody))]
public class MovimentoPeixes : MonoBehaviour
{
    public Peixe peixe;
    public bool Travado {
        get
        {
            return corpoRigido.constraints.HasFlag(RigidbodyConstraints.FreezeAll);
        }
        set
        {
            if (value)
                corpoRigido.constraints = RigidbodyConstraints.FreezeAll;
            else
                corpoRigido.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
    Rigidbody corpoRigido;
    [SerializeField] AudioClip somColeta;
    [SerializeField] float valor;
    [SerializeField] int tamGrupo, ampMovimento;
    [SerializeField] string tipo;
    int direcao = 1;

    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();

        if (transform.position.x > 0)
            direcao = -1;
    }

    void FixedUpdate()
    {
        if (!Travado)
            Mover();
    }

    void Update() {

        if (transform.position.y > 0)
        {
            corpoRigido.velocity += corpoRigido.velocity.y * 2 * Vector3.down;
        }
        else if (transform.position.y < -10)
            Destroy(gameObject);

        if (Mathf.Abs(transform.position.x) > 200)
            corpoRigido.velocity += corpoRigido.velocity.x * 2 * Vector3.left;
    }

    void Mover()
    {
        corpoRigido.velocity = new Vector3(
            direcao * Mathf.Cos(10 * Time.time / (ampMovimento * ampMovimento)) * ampMovimento, 
            Mathf.Sin(Time.time)
        );
    }

    void OnCollisionEnter(Collision outro)
    {
        if (outro.gameObject.CompareTag("Player") && Travado)
        {
            Global.inst.PegarPeixe(peixe);
            Audio.inst.TocarAudio(somColeta);
            Destroy(gameObject);
        }
    }
}
