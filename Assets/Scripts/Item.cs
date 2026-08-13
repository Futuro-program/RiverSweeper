using Assets.Scripts.Estruturas;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] string nome;
    [SerializeField] float custo;
    public ItemCompra vara;

    // Start is called before the first frame update
    void Start()
    {
        vara = new(nome, custo);
    }
}
