using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Classes;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject painelLoja;
    [SerializeField] GameObject painelCreditos;
    [SerializeField] GameObject painelOpcoes;
    [SerializeField] GameObject painelVerifCompra;
    [SerializeField] TextMeshProUGUI textoImpedimentoCompra;
    EstatsJogador estats;
    ItemCompra itemTentadoComprar;

    // Start is called before the first frame update
    void Start()
    {
        estats = FindObjectOfType<EstatsJogador>();
    }

    public void Iniciar()
    {
        SceneManager.LoadScene("Scenes/Fase1");
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
            estats.EquiparVara(item.vara.nome);
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
        PlayerPrefs.SetFloat("Sons", valor);
        PlayerPrefs.Save();
    }

    public void EncarregarSeSliderSensibilidade(float valor)
    {
        PlayerPrefs.SetFloat("Sensibilidade", valor);
        PlayerPrefs.Save();
    }

    public void EncarregarSeSliderMusica(float valor)
    {
        PlayerPrefs.SetFloat("Música", valor);
        PlayerPrefs.Save();
    }
}
