using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class LogicaAnzol : MonoBehaviour
{
    public Vector3 velocidade;
    public float accel = -1;
    public float massa = 1;
    public bool Travado {
        get
        {
            return corpoRigido.constraints.HasFlag(RigidbodyConstraints.FreezeAll);
        }
        set
        {
            if (value)
                corpoRigido.constraints = RigidbodyConstraints.FreezeAll;
            else
                corpoRigido.constraints = RigidbodyConstraints.FreezePositionZ;
        }
    }
    [SerializeField] AudioClip somSplash;
    [SerializeField] float volume = 2;
    const float ACCELGRAVIDADE = 1;
    Rigidbody corpoRigido;
    bool submergido;
    
    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        string varaEquipada = FindObjectOfType<EstatsJogador>().CarregarEstatisticas().varaEquipada;

        massa = varaEquipada switch {
            "madeira" => 10,
            "bambu" => 7,
            "metal" => 4,
            "ferro" => 2,
            "obsidiana" => 1,
            _ => throw new System.Exception("???")
        };
    }

    void FixedUpdate()
    {
        if (transform.position.y < -300)
        {
            velocidade = Vector3.zero;
        }

        if (submergido)
        {
            float empuxo = volume;
            float peso = massa;
            float resultante = (empuxo - peso) * ACCELGRAVIDADE;
            accel = resultante / massa;
            velocidade -= velocidade * 0.2f;
        }
        else
        {
            accel = -ACCELGRAVIDADE;
        }

        Vector3 velAnterior = new(velocidade.x, velocidade.y, velocidade.z);
        velocidade.y += accel;

        corpoRigido.MovePosition(corpoRigido.position + Time.deltaTime / 2 * (velocidade + velAnterior));
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Lixo"))
        {
            MovimentoLixo movLixo = outro.GetComponent<MovimentoLixo>();
            movLixo.Travado = true;
            movLixo.transform.SetParent(transform);
            movLixo.transform.localPosition = Vector3.zero;
            massa += movLixo.massa; 
        }
        else if (outro.gameObject.CompareTag("Water") && !Travado)
            Audio.inst.TocarAudio(somSplash);
    }

    void OnTriggerStay(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
            submergido = true;
    }

    void OnTriggerExit(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
            submergido = false;
    }
}
