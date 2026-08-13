using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LogicaAnzol : MonoBehaviour
{
    public Rigidbody corpoRigido;
    public Vector3 velocidade;
    public float accel = -1;
    public float massa = 1;
    [SerializeField] float densidadeAgua = 1;
    [SerializeField] float volume = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (transform.position.y < -300)
        {
            velocidade = Vector3.zero;
        }

        Vector3 velAnterior = new(velocidade.x, velocidade.y, velocidade.z);
        velocidade.y += accel;

        corpoRigido.MovePosition(corpoRigido.position + Time.deltaTime / 2 * (velocidade + velAnterior));
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
        {
            float empuxo = densidadeAgua * volume * 1;
            float peso = massa * 1;
            float resultante = empuxo - peso;
            accel = resultante / massa;
            velocidade -= velocidade * 0.2f;
        }
        else if (outro.gameObject.CompareTag("Lixo"))
        {
            outro.GetComponent<MovimentoLixo>();
        }
    }
}
