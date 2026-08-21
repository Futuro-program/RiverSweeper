using UnityEngine;

public class GeradorLixo : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] MovimentoLixo[] prefabsLixo;
    [SerializeField] float cooldownMax;
    float cooldownLixo;

    // Start is called before the first frame update
    void Start()
    {
        cooldownLixo = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(mainCamera.position.x, 0);
        if (Time.time - cooldownLixo > cooldownMax)
        {
            cooldownLixo = Time.time;
            MovimentoLixo lixoSel = SortearLixo();

            Instantiate(
                lixoSel.gameObject, 
                transform.position + new Vector3(Random.Range(-12, 12), Random.Range(-5, 0)), 
                Quaternion.identity
            );
        }
    }

    MovimentoLixo SortearLixo()
    {
        MovimentoLixo lixoSel = Instantiate(prefabsLixo[Random.Range(0, prefabsLixo.Length)]);
        float pesoTotal = 0;

        foreach (MovimentoLixo lixo in prefabsLixo)
            pesoTotal += lixo.lixo.valor;
        
        float bobo = Random.Range(0, pesoTotal);

        foreach (MovimentoLixo lixo in prefabsLixo)
        {
            pesoTotal -= bobo;

            if (pesoTotal <= 0)
            {
                lixoSel = Instantiate(lixo);
                break;
            }
        }

        return lixoSel;
    }
}
