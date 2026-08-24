using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AuxAnimAlpha : MonoBehaviour
{
    IEnumerator AnimaDesvanecer(Image imagem, float tMax, bool tudoPreto = false)
    {
        float t = 0;
        imagem.gameObject.SetActive(true);
        Color cor = tudoPreto ? new Color(0, 0, 0) : imagem.color;

        while (t < tMax)
        {
            t += Time.deltaTime;
            cor.a = Mathf.Lerp(tMax, 0, t / tMax);
            imagem.color = cor;

            yield return null;
        }

        cor.a = 0;
        imagem.color = cor;
        imagem.gameObject.SetActive(false);
    }

    IEnumerator AnimaAparecer(Image imagem, float tMax, bool tudoPreto = false)
    {
        float t = 0;
        imagem.gameObject.SetActive(true);
        Color cor = tudoPreto ? new Color(0, 0, 0) : imagem.color;

        while (t < tMax)
        {
            t += Time.deltaTime;
            cor.a = Mathf.Lerp(0, tMax, t / tMax);
            imagem.color = cor;

            yield return null;
        }

        cor.a = 1;
        imagem.color = cor;
    }

    public void AnimarDesvanecer(Image imagem, float duracao, bool tudoPreto = false)
    {
        StartCoroutine(AnimaDesvanecer(imagem, duracao, tudoPreto));
    }

    public void AnimarAparecer(Image imagem, float duracao, bool tudoPreto = false)
    {
        StartCoroutine(AnimaAparecer(imagem, duracao, tudoPreto));
    }
}
