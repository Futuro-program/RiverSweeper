using UnityEngine;

public class LancamentoAnzol : MonoBehaviour
{
    [SerializeField] MovimentoBarco barco;
    [SerializeField] LogicaAnzol anzol;
    [SerializeField] LineRenderer guia;
    [SerializeField] AudioClip somPuxada, somLancamento;
    float anguloLancamento;
    float tempoPress;
    int forcaLancamento;
    int forcaPuxada;
    int estado;

    void Start()
    {
        string varaEquipada = FindObjectOfType<EstatsJogador>().CarregarEstatisticas().varaEquipada;

        forcaLancamento = varaEquipada switch {
            "madeira" => 7,
            "bambu" => 12,
            "metal" => 20,
            "ferro" => 30,
            "obsidiana" => 40,
            _ => throw new System.Exception("???")
        };
        forcaPuxada = varaEquipada switch {
            "madeira" => 2,
            "bambu" => 5,
            "metal" => 10,
            "ferro" => 20,
            "obsidiana" => 30,
            _ => throw new System.Exception("???")
        };
    }

    // Update is called once per frame
    void Update()
    {
        bool pressBotao = Input.GetKeyDown(KeyCode.Space);

        switch (estado)
        {
            case 0: 
            {
                anzol.travado = true;
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
                anzol.travado = false;

                if (guia.gameObject.activeSelf)
                    guia.gameObject.SetActive(false);

                if (pressBotao)
                    estado = 3;
                
                break;
            }
            case 3:
            {
                Audio.inst.TocarAudioLoop(somPuxada);
                anzol.velocidade = (
                    transform.position - anzol.transform.position
                ).normalized * forcaPuxada / anzol.massa;
                anzol.accel = 1;
                break;
            }
        }
    }

    void OnTriggerEnter(Collider outro)
    {
        if (outro.gameObject.CompareTag("Anzol") && estado == 3)
        {
            Audio.inst.PararAudioLoop();
            estado = 0;
        }
    }
}
