using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TintScreen : MonoBehaviour
{
    //lo que haremos sera mezclar dos colores, de un lado a otro
    [SerializeField] Image screenCover;
    [SerializeField] Color untintedColor;
    [SerializeField] Color tintedColor;

    float t;
    [SerializeField] float speed;
    public void Awake()
    {

        UnTint();
    }
    [ContextMenu("Tint")]
    public void Tint()
    {
        StopAllCoroutines();
        t = 0f;
        StartCoroutine(TintScreenCoroutine());
    }

    [ContextMenu("Untint")]
    public void UnTint()
    {
        StopAllCoroutines();
        t = 0f;
        StartCoroutine(UntintScreen());
    }

    IEnumerator TintScreenCoroutine()
    {
        while( t < 1f)
        {
            t += Time.deltaTime * speed;
            t = Mathf.Clamp01(t);

            Color c = screenCover.color;
            c = Color.Lerp(untintedColor, tintedColor, t);
            //pasar de un lado a otro
            //la t es la cantidad de la mezcla de los dos
            screenCover.color = c;

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator UntintScreen()
    {
        while (t < 1f)
        {
            t += Time.deltaTime * speed;
            t = Mathf.Clamp01(t);

            Color c = screenCover.color;
            c = Color.Lerp(tintedColor, untintedColor, t);
            screenCover.color = c;

            yield return new WaitForEndOfFrame();
        }
    }
}
