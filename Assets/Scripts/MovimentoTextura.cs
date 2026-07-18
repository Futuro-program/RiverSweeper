using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MovimentoTextura : MonoBehaviour
{
    [SerializeField] float velocTextura = 3.5f;
    MeshRenderer textura;

    // Start is called before the first frame update
    void Start()
    {
        textura = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        textura.material.mainTextureOffset += velocTextura * Time.deltaTime * new Vector2(1, 1);
    }
}
