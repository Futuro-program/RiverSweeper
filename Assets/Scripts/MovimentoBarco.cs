using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class MovimentoBarco : MonoBehaviour
{
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
            Global.inst.CoordenarAnimacaoBool(animador, "VirarDireita");
        else if (movimentoX < 0)
            Global.inst.CoordenarAnimacaoBool(animador, "VirarEsquerda");

        controlador.Move(Time.deltaTime * modVelocidade * movimentoX * Vector3.right);
    }
}
