using UnityEngine;

public class GeradorPeixes : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject prefab;
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

        if (Time.time - cooldownPeixe > 7)
        {
            int quantidade = Random.Range(3, 5);
            Gerar(quantidade);
        }
    }

    void Gerar(int quant)
    {
        for (int _ = 0; _ <= quant; _++)
        {
            cooldownPeixe = Time.time;
            float posXPeixe = Random.Range(0, 1) == 1 ? 20 : -20;

            Instantiate(
                prefab, new Vector3(posXPeixe, Random.Range(-10, 0)), 
                Quaternion.identity, transform
            );
        }
    }
}
