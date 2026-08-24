using UnityEngine;
using Assets.Scripts.Estruturas;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class MovimentoLixo : MonoBehaviour
{
    public Lixo lixo;
    public float massa;
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
    [SerializeField] AudioClip somColeta;
    [SerializeField] float valor, volume;
    [SerializeField] string tipo;
    Rigidbody corpoRigido;
    AudioSource fonteAudio;
    const float ACCELGRAVIDADE = 10;
    float accel;
    bool submergido;

    // Start is called before the first frame update
    void Start()
    {
        corpoRigido = GetComponent<Rigidbody>();
        corpoRigido.velocity = new Vector3(Random.Range(-5, 5), Random.Range(-5, 5));
        fonteAudio = GetComponent<AudioSource>();

        lixo = new(valor, massa, volume, tipo);
    }

    void FixedUpdate()
    {
        if (corpoRigido.position.y < -300)
        {
            corpoRigido.MovePosition(new Vector3(corpoRigido.position.x, -300));
            corpoRigido.AddForce(2 * corpoRigido.velocity.y * Vector3.down, ForceMode.VelocityChange);
            return;
        }

        if (submergido)
        {
            float empuxo = volume;
            float peso = massa;
            float resultante = (empuxo - peso) * ACCELGRAVIDADE;
            accel = resultante / massa;
            corpoRigido.drag = 1.5f;
        }
        else
        {
            accel = -ACCELGRAVIDADE;
            corpoRigido.drag = 0.02f;
        }

        corpoRigido.AddForce(new Vector3(0, accel), ForceMode.Acceleration);
    }
    
    void OnCollisionEnter(Collision outro)
    {
        if (outro.gameObject.CompareTag("Player") && Travado)
        {
            Global.inst.PegarLixo(lixo);
            Audio.inst.TocarAudio(somColeta);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Water"))
            TocarSomSplash();
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

    void TocarSomSplash()
    {
        fonteAudio.volume = Audio.inst.VolumeSons;
        fonteAudio.Play();
    }
}
