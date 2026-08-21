using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Assets.Scripts.Estruturas;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    GameObject painelLoja, painelCreditos, painelOpcoes, painelVerifCompra, painelSelecaoFases;
    [SerializeField] LogicaBotao[] botoesCompra;
    [SerializeField] TextMeshProUGUI textoImpedimentoCompra;
    [SerializeField] Image painelHistoria;
    EstatsJogador estats;
    ItemCompra itemTentadoComprar;
    float tempoHistoria;
    int estadoHistoria = 0;

    // Start is called before the first frame update
    void Start()
    {
        estats = FindObjectOfType<EstatsJogador>();
    }

    void Update()
    {
        if (estadoHistoria == 1)
        {
            painelHistoria.enabled = true;
            painelHistoria.color = new Color(255, 255, 255, Mathf.Lerp(0, 255, Time.time - tempoHistoria));

            if (Time.time - tempoHistoria >= 1)
            {
                tempoHistoria = Time.time; 
                estadoHistoria = 2;
            }
        }
        else if (estadoHistoria == 2 && Time.time - tempoHistoria >= 4)
        {
            tempoHistoria = Time.time; 
            estadoHistoria = 3;
        }
        else if (estadoHistoria == 3)
        {
            painelHistoria.enabled = true;
            painelHistoria.color = new Color(255, 255, 255, Mathf.Lerp(0, 255, Time.time - tempoHistoria));

            if (Time.time - tempoHistoria >= 1)
                estadoHistoria = 0;
        }
    }

    public void GerenciarSelecaoFases(bool abrir)
    {
        if (estats.CarregarEstatisticas().faseAtual == 1)
            MostrarHistoria();
        
        painelSelecaoFases.SetActive(abrir);
    }

    public void Iniciar(int fase)
    {
        SceneManager.LoadScene($"Scenes/Fase{fase}");
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
                estats.Pagar(itemTentadoComprar.valor);
                estats.AdicionarVara(itemTentadoComprar.nome);
                foreach (var botao in botoesCompra)
                    botao.Verificar();
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

    void MostrarHistoria()
    {
        estadoHistoria = 1;
        tempoHistoria = Time.time;
    }
}
