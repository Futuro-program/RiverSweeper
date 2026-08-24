using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIFases : MonoBehaviour
{
    public static UIFases inst;
    [SerializeField] GameObject painelHUD;
    [SerializeField] GameObject menuContexto, painelOpcoes, painelSaida, painelReset, botaoPassar;
    [SerializeField] Image painelDesvanecer;
    [SerializeField] TextMeshProUGUI tLixoColetado, tDinheiroObtido, tTempoRestante, tPeixesColetados;
    [SerializeField] AudioClip somVitoria;
    EstatsJogador estatsJogador;
    AuxAnimAlpha animadorAlpha;

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
        animadorAlpha = FindObjectOfType<AuxAnimAlpha>();
        
        animadorAlpha.AnimarDesvanecer(painelDesvanecer, 1, true);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void DefLixoColetado(int lixoColetado)
    {
        tLixoColetado.SetText($"{lixoColetado}");
    }

    public void DefDinheiroGanho(float dinheiroGanho)
    {
        tDinheiroObtido.SetText($"{dinheiroGanho:C}");
    }

    public void DefTempoRestante(double tempoFim, double tempoRelativo)
    {
        tTempoRestante.SetText($"{TimeSpan.FromMinutes(tempoFim - (double)tempoRelativo / 60):mm\\:ss}");
    }

    public void DefPeixesColetados(int peixesColetados)
    {
        tPeixesColetados.SetText($"{peixesColetados}");
    }

    public void MudarCorTempo(Color corCima, Color corBaixo)
    {
        VertexGradient grad = new(corCima, corCima, corBaixo, corBaixo);
        tTempoRestante.colorGradient = grad;
    }

    public void MostrarBotaoPassar()
    {
        Audio.inst.TocarAudio(somVitoria, 0.5f);
        animadorAlpha.AnimarAparecer(botaoPassar.GetComponent<Image>(), 1);
    }

    public void Aparecer()
    {
        animadorAlpha.AnimarDesvanecer(painelDesvanecer, 1, true);
    }

    public void Desvanecer()
    {
        animadorAlpha.AnimarAparecer(painelDesvanecer, 1, true);
    }

    public void CarregarFimJogo()
    {
        SceneManager.LoadScene("Scenes/FimDeJogo");
    }

    public void Pausar(bool pause)
    {
        bool pausado = pause;
        Time.timeScale = !pausado ? 1 : 0;
        Global.inst.pausado = pausado;
        menuContexto.SetActive(pausado);
        painelHUD.SetActive(!pausado);
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene($"Scenes/Fase{Global.inst.fase}");
        Pausar(false);
    }

    public void Sair()
    {
        SceneManager.LoadScene("Scenes/TelaInicial");
    }

    public void PassarFase()
    {
        if (estatsJogador.CarregarEstatisticas().faseAtual == Global.inst.fase)
            estatsJogador.PassarFase();

        int cenaACarregar = Mathf.Clamp(estatsJogador.CarregarEstatisticas().faseAtual, 1, 2);
        SceneManager.LoadScene($"Scenes/Fase{cenaACarregar}");
    }

    public void GerenciarOpcoes(bool abrir)
    {
        painelOpcoes.SetActive(abrir);
    }

    public void GerenciarSaida(bool abrir)
    {
        painelSaida.SetActive(abrir);
    }

    public void GerenciarReset(bool abrir)
    {
        painelReset.SetActive(abrir);
    }

    public void EncarregarSeSliderSons(float valor)
    {
        Audio.inst.VolumeSons = valor;
    }

    public void EncarregarSeSliderSensibilidade(float valor)
    {
        PlayerPrefs.SetFloat("Sensibilidade", valor);
        PlayerPrefs.Save();
    }

    public void EncarregarSeSliderMusica(float valor)
    {
        Audio.inst.VolumeMusica = valor;
    }

    public void RedefinirProgresso()
    {
        estatsJogador.RedefinirProgresso();
        painelReset.SetActive(false);
    }
}
