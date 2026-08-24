using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] Image painelDesvanecer;
    [SerializeField] AudioClip somDerrota;
    AuxAnimAlpha animadorAlpha;
    float tempoSaida, tempoReinicio;

    void Start()
    {
        animadorAlpha = FindObjectOfType<AuxAnimAlpha>();
        animadorAlpha.AnimarDesvanecer(painelDesvanecer, 1, true);
        Audio.inst.TocarAudio(somDerrota);
    }

    void Update()
    {
        if (Time.time - tempoSaida > 1 && tempoSaida != 0)
            SairDeFato();
        
        if (Time.time - tempoReinicio > 1 && tempoReinicio != 0)
            ReiniciarDeFato();
    }

    void SairDeFato()
    {
        SceneManager.LoadScene("Scenes/TelaInicial");
    }

    void ReiniciarDeFato()
    {
        SceneManager.LoadScene($"Scenes/Fase{Global.inst.fase}");
    }
    
    public void Sair()
    {
        tempoSaida = Time.time;
        animadorAlpha.AnimarAparecer(painelDesvanecer, 1, true);
    }

    public void Reiniciar()
    {
        tempoReinicio = Time.time;
        animadorAlpha.AnimarAparecer(painelDesvanecer, 1, true);
    }
}
