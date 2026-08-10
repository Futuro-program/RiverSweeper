using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(Rigidbody))]
public class MovimentoLixo : MonoBehaviour
{
    MeshFilter malha;
    Rigidbody corpoRigido;
    [SerializeField] Mesh[] malhasSel;

    // Start is called before the first frame update
    void Start()
    {
        malha = GetComponent<MeshFilter>();
        malha.mesh = malhasSel[Random.Range(0, malhasSel.Length - 1)];
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
    }

    void Update() {
        if (transform.position.y > 0)
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        else if (transform.position.y < -10)
            corpoRigido.velocity += corpoRigido.velocity.y * 2 * Vector3.down;

        if (Mathf.Abs(transform.position.x) > 50)
            corpoRigido.velocity += corpoRigido.velocity.x * 2 * Vector3.left;
    }
}
