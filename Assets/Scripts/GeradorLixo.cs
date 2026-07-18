using UnityEngine;

public class GeradorLixo : MonoBehaviour
{
    [SerializeField] Transform mainCamera;
    [SerializeField] GameObject prefab;
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
        if (Time.time - cooldownLixo > 10)
        {
            cooldownLixo = Time.time;
            Instantiate(
                prefab, new Vector3(Random.Range(-12, 12), Random.Range(-10, 0)), 
                Quaternion.identity, transform
            );
        }
    }
}
