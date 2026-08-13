using UnityEngine;
using UnityEngine.SceneManagement;

public class UIGameOver : MonoBehaviour
{
    public void Sair()
    {
        SceneManager.LoadScene("Scenes/TelaInicial");
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene("Scenes/Fase1");
    }
}
