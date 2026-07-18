using UnityEngine;
using UnityEngine.SceneManagement;

public class UIFases : MonoBehaviour
{
    [SerializeField] GameObject painelHUD;
    [SerializeField] GameObject menuContexto;
    [SerializeField] GameObject painelOpcoes;
    [SerializeField] GameObject painelSaida;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void GerenciarOpcoes(bool abrir)
    {
        painelOpcoes.SetActive(abrir);
    }

    public void GerenciarSaida(bool abrir)
    {
        painelSaida.SetActive(abrir);
    }
}
