using UnityEngine;

public class Corda : MonoBehaviour
{
    [SerializeField] LineRenderer linha;
    [SerializeField] Transform anzol;

    void Update()
    {
        linha.SetPosition(2, anzol.localPosition);
    }
}
