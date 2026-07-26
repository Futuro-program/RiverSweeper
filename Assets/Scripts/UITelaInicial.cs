using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject painelLoja;
    [SerializeField] GameObject painelCreditos;
    [SerializeField] GameObject painelOpcoes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void ComprarOuEquipar(string item)
    {
        
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
