using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Menu_Command : MonoBehaviour
{
    //Canvas
    [SerializeField] CanvasGroup canvas_acciones;

    private void Start()
    {
        //canvas_acciones = GetComponent<CanvasGroup>();
        //Seteamos  opacidad
        //opacidad(0f);
    }
    //Aqui se reciben las nuevas opacidades
    public void opacidad(float nueva_opacidad)
    {
        // 0 == no se ve
        // 1 == se ve
        canvas_acciones.alpha = Mathf.Lerp(0f, nueva_opacidad, 5f);
    }
}