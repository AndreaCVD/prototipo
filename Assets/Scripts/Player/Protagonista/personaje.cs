using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class personaje : MonoBehaviour
{
    private GameObject statsPersonaje;
    //[Header("Posicion")]
    public Vector3 posicion;
    public GameObject PrefabProta;
    
    void Awake()
    {
         //statsPersonaje = GameObject.Find("--Preload--");
        PrefabProta = GameObject.Find("personaje");
    }

    void Update()
    {
        if (PrefabProta != null)
        {
            PrefabProta = GameObject.Find("personaje");
        }
        Scene escenaActual = SceneManager.GetActiveScene();
        //Debug.Log(escenaActual.name);
        if (escenaActual.name == "pruevas_prototipo")
        {
            save_pos();
        }
    }
    public void save_pos()
    {
        posicion = PrefabProta.transform.position;
    }

    public Vector3 load_pos()
    {
        return posicion;
    }


    //void Start()
    //{
    //    //si estamos en combate esto no aplica
    //    Scene escenaActual = SceneManager.GetActiveScene();

    //    if (escenaActual.name == "prueva_prototipo")
    //    {
    //        float x = posicion.x;
    //        float y = posicion.y;
    //        float z = posicion.z;
    //        protagonista.transform.position = new Vector3(x, y, z);
    //    }

    //}


}
