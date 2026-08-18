using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class CambioNivel : MonoBehaviour
{
    [SerializeField] string nextScene;
    private GameObject objLoadScene;
    private LoadScene load;

    public Puzzle lista;


    private float tiempoEspera = 1.5f;

    EventTrigger trigger;
    public bool tiempo;

    private void Start()
    {
        if (objLoadScene == null)
        {
            objLoadScene = GameObject.Find("--SceneManagement--");
            load = objLoadScene.GetComponent<LoadScene>();
        }
        trigger = GetComponent<EventTrigger>();
        tiempo = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (tiempo)
        {
            tiempo = false;
        }
        //Ver si es el personaje
        if (col.CompareTag("Player"))
        {
            Debug.Log("Ha entrado el prota");
            //Si despues de X tiempo sigue estando alla hacer el cambio de escena
            StartCoroutine(EsperarYComprovar());
        }
    }
    void OnTriggerStay(Collider col)
    {
        //Comprovar si aun esta el prota y el tiempo ha pasado
        if (col.CompareTag("Player") && tiempo)
        {
            Debug.Log("El prota sigue aqui, cambio de escena");
            tiempo = false;
            
            Scene escenaActual = SceneManager.GetActiveScene();

            if (nextScene == "Nivel_1" && !lista.Subir_Nivel[0].acabado)
            {
                lista.Subir_Nivel[0].acabado = true;
                load.NombreEscenaAnterior();
                load.ChangeScene("Menu_SubirStat");
            }
            else if (nextScene == "Nivel_2" && !lista.Subir_Nivel[1].acabado)
            {
                lista.Subir_Nivel[1].acabado = true;
                load.NombreEscenaAnterior();
                load.ChangeScene("Menu_SubirStat");
            }
            else if (nextScene == "Nivel_3" && !lista.Subir_Nivel[2].acabado)
            {
                lista.Subir_Nivel[2].acabado = true;
                load.NombreEscenaAnterior();
                load.ChangeScene("Menu_SubirStat");
            }
            else
                load.ChangeScene(nextScene);

        }

    }
    void OnTriggerExit(Collider col)
    {
        CancelInvoke("EsperarYComprovar");        
        if (tiempo)
        {
            tiempo = false;
        }
    }
    IEnumerator EsperarYComprovar()
    {
        yield return new WaitForSeconds(tiempoEspera);
        // Código que se ejecuta después del retraso
        Debug.Log("Han pasado " +tiempoEspera + " segundos.");
        tiempo = true;
        //if (trigger.CompareTag("Player"))
        //{
        //    Debug.Log("El prota sigue aqui, cambio de escena");
        //    load.ChangeScene(nextScene);
        //}
    }
}