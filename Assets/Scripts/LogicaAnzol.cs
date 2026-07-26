using UnityEngine;

public class LogicaAnzol : MonoBehaviour
{
    public CharacterController controlador;
    public Vector3 velocidade;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Vector3 velAnterior = new(velocidade.x, velocidade.y, velocidade.z);
        controlador.Move(Time.deltaTime / 2 * (velocidade + velAnterior));
    }

    void OnTriggerEnter(Collider outro)
    {
        
    }
}
