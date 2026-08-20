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

        for (int _ = 0; _ <= peixeSel.peixe.tamGrupo; _++)
        {
            cooldownPeixe = Time.time;
            float posXPeixe = Random.Range(0, 1) == 1 ? 20 : -20;

            Instantiate(
                peixeSel, 
                transform.position + new Vector3(posXPeixe, Random.Range(-10, 0)), 
                Quaternion.identity
            );
        }
    }

    MovimentoPeixes SortearPeixe()
    {
        MovimentoPeixes peixeSel = null;
        float pesoTotal = 0;
        foreach (MovimentoPeixes peixe in prefabsPeixes)
            pesoTotal += peixe.peixe.valor;
        
        float bobo = Random.Range(0, pesoTotal);

        foreach (MovimentoPeixes peixe in prefabsPeixes)
        {
            pesoTotal -= bobo;

            if (pesoTotal <= 0)
            {
                peixeSel = peixe;
                break;
            }
        }

        peixeSel = peixeSel == null ? prefabsPeixes[0] : peixeSel;

        return peixeSel;
    }
}
