using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class MovimentoBarco : MonoBehaviour
{
    public int lado;
    [SerializeField] float modVelocidade = 5f;
    Rigidbody corpoRigido;
    Animator animador;
    float inputX;
    
    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        animador = GetComponent<Animator>();
        lado = 1;
    }

    // FixedUpdate is called by the Unity Runtime.
    void FixedUpdate()
    {
        Mover(inputX);
    }

    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
    }

    void Mover(float movimentoX)
    {
        if (movimentoX > 0)
        {
            Global.inst.CoordenarAnimacaoBool(animador, "VirarDireita");
            lado = 1;
        }
        else if (movimentoX < 0)
        {
            Global.inst.CoordenarAnimacaoBool(animador, "VirarEsquerda");
            lado = -1;
        }

        corpoRigido.MovePosition(corpoRigido.position + Time.deltaTime * modVelocidade * movimentoX * Vector3.right);
        if (corpoRigido.position.x > 202)
            corpoRigido.MovePosition(new Vector3(
                202, 
                corpoRigido.position.y,
                corpoRigido.position.z
            ));
        else if (corpoRigido.position.x < -185)
            corpoRigido.MovePosition(new Vector3(
                -185, 
                corpoRigido.position.y,
                corpoRigido.position.z
            ));
    }
}
