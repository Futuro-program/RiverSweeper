using UnityEngine;

public class LancamentoAnzol : MonoBehaviour
{
    [SerializeField] MovimentoBarco barco;
    [SerializeField] LogicaAnzol anzol;
    [SerializeField] LineRenderer guia;
    [SerializeField] int forcaLancamento;
    float anguloLancamento;
    float tempoPress;
    int estado;

    // Update is called once per frame
    void Update()
    {
        bool pressBotao = Input.GetKeyDown(KeyCode.Space);

        switch (estado)
        {
            case 0: 
            {
                anzol.transform.localPosition = Vector3.left * 0.1f;
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
                anzol.transform.localPosition = Vector3.left * 0.2f;

                if (!guia.gameObject.activeSelf)
                    guia.gameObject.SetActive(true);

                float tempo = Time.time - tempoPress;
                float tempoMax = 0.5f;
                float fator = Mathf.PingPong(tempo, tempoMax);
                anguloLancamento = Mathf.LerpAngle(0, 90, fator / tempoMax);

                guia.SetPosition(1, new Vector3(
                    -Mathf.Cos(anguloLancamento * Mathf.Deg2Rad),
                    0,
                    Mathf.Sin(anguloLancamento * Mathf.Deg2Rad)
                ));

                if (pressBotao)
                {
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
                if (guia.gameObject.activeSelf)
                    guia.gameObject.SetActive(false);

                if (pressBotao)
                    estado = 3;
                
                break;
            }
            case 3:
            {
                anzol.velocidade = (
                    transform.position - anzol.transform.position
                ).normalized * 10;
                break;
            }
        }
    }

    void FixedUpdate()
    {
        if (estado == 2)
            anzol.velocidade.y -= 2;
    }

    void OnCollisionEnter(Collision outro)
    {
        if (outro.gameObject.CompareTag("Anzol") && estado == 3)
            estado = 0;
    }
}
