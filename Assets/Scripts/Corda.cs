using UnityEngine;

public class Corda : MonoBehaviour
{
    [SerializeField] LineRenderer linha;
    [SerializeField] Transform anzol;

    void Update()
    {
        linha.SetPosition(2, new Vector3(anzol.position.x, 0, anzol.position.y));
    }
}
