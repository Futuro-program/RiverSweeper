using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovimentoBarco : MonoBehaviour
{
    public int lado;
    [SerializeField] float modVelocidade = 5f;
    CharacterController controlador;
    Animator animador;
    
    // Start is called before the first frame update
    void Start()
    {
        controlador = GetComponent<CharacterController>();
        animador = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float movimentoX = Input.GetAxis("Horizontal");

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

        controlador.Move(Time.deltaTime * modVelocidade * movimentoX * Vector3.right);
    }
}
