using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{
    [SerializeField] Transform alvo;
    [SerializeField] float distancia = 10f;
    [SerializeField] Vector3 desloc = new();

    void Start()
    {
        desloc.z -= distancia;
        transform.position += desloc;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direcCamera = (alvo.position - transform.position) * Vector2.right * 0.9f;
        transform.position += direcCamera;
    }
}
