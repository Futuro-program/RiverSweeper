using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
public class MovimentoPeixes : MonoBehaviour
{
    MeshFilter malha;
    Rigidbody corpoRigido;
    [SerializeField] Mesh[] malhasSel;
    int ampMovimento;

    // Start is called before the first frame update
    void Start()
    {
        malha = GetComponent<MeshFilter>();
        malha.mesh = malhasSel[Random.Range(0, malhasSel.Length - 1)];
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
    }

    void FixedUpdate()
    {
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
        corpoRigido.velocity = new Vector3(Mathf.Cos(Time.time / 2) * 10 + 1, Mathf.Sin(Time.time));
    }
}
