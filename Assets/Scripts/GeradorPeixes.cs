using UnityEngine;

public class GeradorPeixes : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] MovimentoPeixes[] prefabsPeixes;
    [SerializeField] float cooldownMax;
    float cooldownPeixe;

    // Start is called before the first frame update
    void Start()
    {
        cooldownPeixe = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new(mainCamera.position.x, 0);

        if (Time.time - cooldownPeixe > cooldownMax)
            Gerar();
    }

    void Gerar()
    {
        MovimentoPeixes peixeSel = SortearPeixe();
        float posXPeixe = Random.Range(0, 1) == 1 ? 20 : -20;

        for (int i = 0; i <= peixeSel.peixe.tamGrupo; i++)
        {
            cooldownPeixe = Time.time;
            Vector3 posPeixeEsp = new(
                posXPeixe + i * (-i + peixeSel.peixe.tamGrupo - 1),
                peixeSel.peixe.tamGrupo > 1 ? -i * 5 / (peixeSel.peixe.tamGrupo - 1) : 2.5f
            );
            Instantiate(
                peixeSel, 
                transform.position + posPeixeEsp, 
                Quaternion.identity
            );
        }
    }

    MovimentoPeixes SortearPeixe()
    {
        MovimentoPeixes peixeSel = Instantiate(prefabsPeixes[Random.Range(0, prefabsPeixes.Length)]);
        float pesoTotal = 0;

        foreach (MovimentoPeixes peixe in prefabsPeixes)
            pesoTotal += peixe.peixe.valor;
        
        float bobo = Random.Range(0, pesoTotal);

        foreach (MovimentoPeixes peixe in prefabsPeixes)
        {
            pesoTotal -= bobo;

            if (pesoTotal <= 0)
            {
                peixeSel = Instantiate(peixe);
                break;
            }
        }

        return peixeSel;
    }
}
