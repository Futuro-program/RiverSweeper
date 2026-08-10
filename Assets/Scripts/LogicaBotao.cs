using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LogicaBotao : MonoBehaviour
{
    [SerializeField] Sprite imgEquipar, imgEquipado;
    [SerializeField] string varaSel;
    Image imagem;
    EstatsJogador estatsJogador;

    // Start is called before the first frame update
    void Start()
    {
        imagem = GetComponent<Image>();
        estatsJogador = FindObjectOfType<EstatsJogador>();

        Verificar();
    }

    // Update is called once per frame
    public void Verificar()
    {
        if (estatsJogador.CarregarEstatisticas().varaEquipada == varaSel)
            imagem.sprite = imgEquipado;
        else if (estatsJogador.CarregarEstatisticas().varasCompradas.Contains(varaSel))
            imagem.sprite = imgEquipar;
    }
}
