using UnityEngine;

public class GeradorLixo : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject[] prefabsLixo;
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
        if (Time.time - cooldownLixo > 5)
        {
            cooldownLixo = Time.time;
            Instantiate(
                prefabsLixo[Random.Range(0, prefabsLixo.Length)], 
                transform.position + new Vector3(Random.Range(-12, 12), Random.Range(-10, 0)), 
                Quaternion.identity
            );
        }
    }
}
