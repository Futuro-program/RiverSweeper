using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LancamentoAnzol : MonoBehaviour
{
    [SerializeField] MovimentoBarco barco;
    [SerializeField] LogicaAnzol anzol;
    [SerializeField] LineRenderer guia;
    [SerializeField] Material[] materiais;
    [SerializeField] AudioClip somPuxada, somLancamento;
    [SerializeField] int forcaPuxada = 10;
    MeshRenderer renderMalha;
    float anguloLancamento;
    float tempoPress;
    int forcaLancamento;
    int estado;

    void Start()
    {
        renderMalha = GetComponent<MeshRenderer>();

        string varaEquipada = FindObjectOfType<EstatsJogador>().CarregarEstatisticas().varaEquipada;

        int idxMaterial;

        switch (varaEquipada)
        {
            case "madeira":
            {
                idxMaterial = 0;
                forcaLancamento = 15;
                break;
            }
            case "bambu":
            {
                idxMaterial = 1;
                forcaLancamento = 23;
                break;
            }
            case "metal":
            {
                idxMaterial = 2;
                forcaLancamento = 32;
                break;
            }
            case "ferro":
            {
                idxMaterial = 3;
                forcaLancamento = 43;
                break;
            }
            case "obsidiana":
            {
                idxMaterial = 4;
                forcaLancamento = 54;
                break;
            }
            default:
                throw new System.Exception("???");
        }
        
        renderMalha.material = materiais[idxMaterial];
    }

    // Update is called once per frame
    void Update()
    {
        bool pressBotao = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        switch (estado)
        {
            case 0: 
            {
                anzol.Travado = true;
                anzol.transform.position = barco.transform.position + new Vector3(barco.lado * 0.5f, 1);
                anzol.velocidade = Vector3.zero;

                if (pressBotao)
                {
                    estado = 1;
                    tempoPress = Time.time;
                }
                
                break;
            }
            case 1:
            {
                anzol.transform.position = barco.transform.position + new Vector3(barco.lado, 1);

                if (!guia.gameObject.activeSelf)
                    guia.gameObject.SetActive(true);

                float tempo = Time.time - tempoPress;
                float tempoMax = 0.5f;
                float fator = Mathf.PingPong(tempo, tempoMax);
                anguloLancamento = Mathf.LerpAngle(0, 90, fator / tempoMax);

                guia.SetPosition(1, new Vector3(
                    0,
                    Mathf.Cos(anguloLancamento * Mathf.Deg2Rad) * -barco.lado,
                    Mathf.Sin(anguloLancamento * Mathf.Deg2Rad)
                ));

                if (pressBotao)
                {
                    Audio.inst.TocarAudio(somLancamento);
                    anzol.velocidade = new Vector3(
                        Mathf.Cos(anguloLancamento * Mathf.Deg2Rad) * forcaLancamento * barco.lado, 
                        Mathf.Sin(anguloLancamento * Mathf.Deg2Rad) * forcaLancamento
                    );
                    
                    estado = 2;
                }
                
                break;
            }
            case 2:
            {
                anzol.Travado = false;

                if (guia.gameObject.activeSelf)
                    guia.gameObject.SetActive(false);

                if (pressBotao)
                    estado = 3;
                
                break;
            }
            case 3:
            {
                Audio.inst.TocarAudioLoop(somPuxada);
                anzol.velocidade += (
                    barco.transform.position - anzol.transform.position
                ).normalized * forcaPuxada / anzol.massa;
                break;
            }
        }
    }

    void OnTriggerStay(Collider outro)
    {
        if (outro.gameObject.CompareTag("Anzol") && estado == 3)
        {
            Audio.inst.PararAudioLoop();
            estado = 0;
        }
    }
}
