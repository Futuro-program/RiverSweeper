using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Estruturas;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject painelLoja, painelCreditos, painelOpcoes, painelVerifCompra, painelSelecaoFases, painelReset;
    [SerializeField] TextMeshProUGUI textoImpedimentoCompra, textoImpedimentoFase, contadorDinheiro;
    [SerializeField] Image painelHistoria;
    [SerializeField] LogicaBotao[] botoesCompra;
    EstatsJogador estats;
    AuxAnimAlpha animadorAlpha;
    ItemCompra itemTentadoComprar;
    float tempoHistoria, tempoInicio;
    int estadoHistoria = 0, faseTentandoEntrar = 1;

    // Start is called before the first frame update
    void Start()
    {
        estats = FindObjectOfType<EstatsJogador>();
        animadorAlpha = FindObjectOfType<AuxAnimAlpha>();
    }

    void Update()
    {
        if (estadoHistoria == 1)
        {
            painelHistoria.gameObject.SetActive(true);
            painelHistoria.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, Time.time - tempoHistoria));

            if (Time.time - tempoHistoria >= 1)
            {
                tempoHistoria = Time.time; 
                estadoHistoria = 2;
            }
        }
        else if (estadoHistoria == 2 && Time.time - tempoHistoria >= 20)
        {
            tempoHistoria = Time.time; 
            estadoHistoria = 3;
        }
        else if (estadoHistoria == 3)
        {
            painelHistoria.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Time.time - tempoHistoria));

            if (Time.time - tempoHistoria >= 1)
            {
                painelHistoria.gameObject.SetActive(false);
                estadoHistoria = 0;
            }
        }

        if (Time.time - tempoInicio > 1 && tempoInicio != 0)
            IniciarDeFato();
    }

    void MostrarHistoria()
    {
        estadoHistoria = 1;
        tempoHistoria = Time.time;
    }

    void IniciarDeFato()
    {
        SceneManager.LoadScene($"Scenes/Fase{faseTentandoEntrar}");
    }

    void AtualizarContadorDinheiro()
    {
        float novoValor = estats.CarregarEstatisticas().dinheiro;
        contadorDinheiro.SetText($"{novoValor:C}");
    }

    public void PularHistoria()
    {
        estadoHistoria = 0;
        painelHistoria.gameObject.SetActive(false);
    }

    public void GerenciarSelecaoFases(bool abrir)
    {
        if (estats.CarregarEstatisticas().faseAtual == 1)
            MostrarHistoria();
        
        painelSelecaoFases.SetActive(abrir);
    }

    public void Iniciar(int fase)
    {
        if (estats.CarregarEstatisticas().faseAtual >= fase)
        {
            faseTentandoEntrar = fase;
            tempoInicio = Time.time;
            animadorAlpha.AnimarAparecer(painelHistoria, 1, true);
        }
        else
            textoImpedimentoFase.SetText("Precisa concluir a fase anterior!");
    }

    public void Sair()
    {
        Application.Quit();
    }

    public void GerenciarCreditos(bool abrir)
    {
        painelCreditos.SetActive(abrir);
    }

    public void GerenciarOpcoes(bool abrir)
    {
        painelOpcoes.SetActive(abrir);
    }

    public void GerenciarLoja(bool abrir)
    {
        painelLoja.SetActive(abrir);
        AtualizarContadorDinheiro();
    }

    public void GerenciarReset(bool abrir)
    {
        painelReset.SetActive(abrir);
    }

    public void ComprarOuEquipar(Item item)
    {
        try
        {
            if (item.vara.nome != estats.CarregarEstatisticas().varaEquipada)
                estats.EquiparVara(item.vara.nome);

            foreach (var botao in botoesCompra)
                botao.Verificar();
        }
        catch (Exception)
        {
            painelVerifCompra.SetActive(true);
            itemTentadoComprar = item.vara;
        }
    }

    public void ConfirmarCompra(bool resposta)
    {
        if (resposta)
        {
            try
            {
                estats.AdicionarVara(itemTentadoComprar.nome);
                estats.Pagar(itemTentadoComprar.valor);

                foreach (var botao in botoesCompra)
                    botao.Verificar();
                
                AtualizarContadorDinheiro();
            }
            catch (Exception e)
            {
                textoImpedimentoCompra.text = e.Message;
            }
        }

        painelVerifCompra.SetActive(false);
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
        estats.RedefinirProgresso();
        painelReset.SetActive(false);
    }
}
