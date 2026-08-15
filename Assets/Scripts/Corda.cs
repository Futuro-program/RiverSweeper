using UnityEngine;

public class Corda : MonoBehaviour
{
    [SerializeField] LineRenderer linha;
    [SerializeField] Transform orig, ponta, anzol;

    void Update()
    {
        linha.SetPositions(new Vector3[]{
            new(orig.position.x, orig.position.y),
            new(ponta.position.x, ponta.position.y), 
            new(anzol.position.x, anzol.position.y)
        });
    }
}
