using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Global : MonoBehaviour
{
    [SerializeField] Transform luzGlobal;
    [SerializeField] TextMeshProUGUI lixoColetado;
    public static Global inst;
    EstatsJogador estatsJogador;
    public bool pausado = false;
    public int fase = 1;
    int cLixoColetado = 0;

    void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(inst);
    }

    // Start is called before the first frame update
    void Start()
    {
        lixoColetado.SetText($"Lixo coletado: {cLixoColetado}");
    }

    // FixedUpdate é chamado pelo Runtime do Unity.
    void FixedUpdate()
    {
        float angulo = Time.fixedTime / 5;
        luzGlobal.rotation = Quaternion.Euler(angulo, 0, 0);
    }

    void Update()
    {
        if (Time.time / 60 > 15)
        {
            estatsJogador.IncrementarLixoColetado(cLixoColetado);

            if (cLixoColetado > 100)
                CarregarFimJogo();
        }
    }

    void CarregarFimJogo()
    {
        SceneManager.LoadScene("Scenes/FimJogo");
    }

    public void PegarLixo()
    {
        cLixoColetado++;
        lixoColetado.SetText($"Lixo coletado: {cLixoColetado}");
    }

    public void CoordenarAnimacaoBool(Animator animador, string nomeParametro)
    {
        foreach (AnimatorControllerParameter param in animador.parameters)
            animador.SetBool(param.name, param.name == nomeParametro);
    }
}
