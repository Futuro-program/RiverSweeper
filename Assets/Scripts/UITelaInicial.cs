using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Estruturas;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject painelLoja, painelCreditos, painelOpcoes, painelVerifCompra;
    [SerializeField] LogicaBotao[] botoesCompra;
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
