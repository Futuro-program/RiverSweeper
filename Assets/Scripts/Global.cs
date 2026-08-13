using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using Assets.Scripts.Estruturas;

public class Global : MonoBehaviour
{
    public static Global inst;
    public bool pausado = false;
    public int fase = 1;
    [SerializeField] Transform luzGlobal;
    [SerializeField] TextMeshProUGUI lixoColetado, dinheiroObtido, tempoRestante;
    [SerializeField] AudioClip somVitoria, somDerrota;
    const int TEMPOFIM = 5;
    readonly EstatsJogador estatsJogador;
    float tDinheiroGanho = 0;
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
        dinheiroObtido.SetText($"{tDinheiroGanho:C}");
        tempoRestante.SetText($"{TimeSpan.FromMinutes(TEMPOFIM - (double)Time.time / 60):mm\\:ss}");
    }

    // FixedUpdate é chamado pelo Runtime do Unity.
    void FixedUpdate()
    {
        float angulo = 3 * Time.fixedTime / TEMPOFIM;
        luzGlobal.rotation = Quaternion.Euler(angulo, 0, 0);
    }

    void Update()
    {
        tempoRestante.SetText($"{TimeSpan.FromMinutes(TEMPOFIM - (double)Time.time / 60):mm\\:ss}");

        if (Time.time / 60 > TEMPOFIM)
            CarregarFimJogo();
    }

    void CarregarFimJogo()
    {
        estatsJogador.IncrementarLixoColetado(cLixoColetado);
        estatsJogador.Vender(tDinheiroGanho);

        if (cLixoColetado >= 100)
        {
            if (estatsJogador.CarregarEstatisticas().faseAtual == fase)
                estatsJogador.PassarFase();
            
            Audio.inst.TocarAudio(somVitoria, 0.5f);
        }
        else
            Audio.inst.TocarAudio(somDerrota);

        SceneManager.LoadScene("Scenes/FimDeJogo");
    }

    public void PegarLixo(Lixo lixo)
    {
        cLixoColetado++;
        lixoColetado.SetText($"Lixo coletado: {cLixoColetado}");
        tDinheiroGanho += lixo.valor;
        dinheiroObtido.SetText($"{tDinheiroGanho:C}");
    }

    public void CoordenarAnimacaoBool(Animator animador, string nomeParametro)
    {
        foreach (AnimatorControllerParameter param in animador.parameters)
            animador.SetBool(param.name, param.name == nomeParametro);
    }
}
