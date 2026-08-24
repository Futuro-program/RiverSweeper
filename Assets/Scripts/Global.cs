using UnityEngine;
using Assets.Scripts.Estruturas;

public class Global : MonoBehaviour
{
    public static Global inst;
    public bool pausado = false;
    public int fase = 1, minLixoFase;
    [SerializeField] Transform luzGlobal;
    const float TEMPOFIM = 3f;
    EstatsJogador estatsJogador;
    float tDinheiroGanho = 0;
    float tempoInicio, tempoRelativo;
    int cLixoColetado = 0, cPeixesColetados = 0;
    bool acabou = false, trocouCena = false;

    void Awake()
    {
        if (inst == null)
            inst = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        estatsJogador = FindObjectOfType<EstatsJogador>();
        tempoInicio = Time.fixedTime;
        tempoRelativo = 0;

        UIFases.inst.DefLixoColetado(cLixoColetado);
        UIFases.inst.DefDinheiroGanho(tDinheiroGanho);
        UIFases.inst.DefTempoRestante(TEMPOFIM, tempoRelativo);
        UIFases.inst.DefPeixesColetados(cPeixesColetados);

        UIFases.inst.Aparecer();
    }

    // FixedUpdate é chamado pelo Runtime do Unity.
    void FixedUpdate()
    {
        tempoRelativo = Time.fixedTime - tempoInicio;

        if (!acabou)
        {
            float angulo = 3 * tempoRelativo / TEMPOFIM;
            luzGlobal.rotation = Quaternion.Euler(angulo, 0, 0);
        }
    }

    void Update()
    {
        UIFases.inst.DefTempoRestante(TEMPOFIM, tempoRelativo);

        if (tempoRelativo / 60 > TEMPOFIM)
        {
            if (!acabou)
            {
                estatsJogador.IncrementarLixoColetado(cLixoColetado);
                estatsJogador.Vender(tDinheiroGanho);

                if (cLixoColetado >= minLixoFase)
                    UIFases.inst.MostrarBotaoPassar();
                else
                    UIFases.inst.Desvanecer();
                
                acabou = true;
            }   
            else if (cLixoColetado < minLixoFase && !trocouCena && tempoRelativo - TEMPOFIM * 60 > 1)
            {
                DestruirAoReiniciar scriptDestruct = gameObject.AddComponent<DestruirAoReiniciar>();
                scriptDestruct.faseNativa = fase;
                trocouCena = true;
                UIFases.inst.CarregarFimJogo();
            }
        }
        else if (tempoRelativo / 60 > TEMPOFIM / 3 && !acabou)
        {
            if (TEMPOFIM * 60 - tempoRelativo < 30)
            {
                UIFases.inst.MudarCorTempo(
                    new Color(0.9f, 0.5f, 0), new Color(0.9f, 0, 0)
                );
            }
            else
                UIFases.inst.MudarCorTempo(Color.yellow, new Color(0.8f, 0.8f, 0));
        }
    }

    public void PegarLixo(Lixo lixo)
    {
        cLixoColetado++;
        UIFases.inst.DefLixoColetado(cLixoColetado);
        tDinheiroGanho += lixo.valor;
        UIFases.inst.DefDinheiroGanho(tDinheiroGanho);
    }

    public void PegarPeixe(Peixe peixe)
    {
        cPeixesColetados++;
        UIFases.inst.DefPeixesColetados(cPeixesColetados);
        tDinheiroGanho -= peixe.valor;
        UIFases.inst.DefDinheiroGanho(tDinheiroGanho);
    }

    public void CoordenarAnimacaoBool(Animator animador, string nomeParametro)
    {
        foreach (AnimatorControllerParameter param in animador.parameters)
            animador.SetBool(param.name, param.name == nomeParametro);
    }
}
