using UnityEngine;
using UnityEngine.SceneManagement;

public class DestruirAoReiniciar : MonoBehaviour
{
    public int faseNativa;
    bool destrua;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += AoCarregarCena;
    }

    void Start()
    {
        destrua = true;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= AoCarregarCena;
    }

    void AoCarregarCena(Scene cenaNova, LoadSceneMode modo) {
        if (!destrua)
            return;
        
        if (0 == cenaNova.buildIndex || cenaNova.buildIndex == faseNativa)
            Destroy(gameObject);
    }
}
