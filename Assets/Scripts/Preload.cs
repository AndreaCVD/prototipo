using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Cargar todas las cosas antes de la escena normal, no combate
//esto no se destruira entre escenas
public class Preload : MonoBehaviour
{
    //guardar las variables para no perderlas
    private GameObject preloadObj;
    //public Vector3 posicion;
    private GameObject PrefabProta;
    //hacer que el personaje no se destruya
    private GameObject protagonista;
    //para recibir la posicion anterior
    personaje vectorPosicion;

    void Awake()
    {       
        preloadObj = GameObject.Find("--Preload--");
        PrefabProta = GameObject.Find("personaje");
        vectorPosicion = GetComponent<personaje>();
        protagonista = GameObject.Find("Player Character");

        //Scene escenaActual = SceneManager.GetActiveScene();
        //if (escenaActual.name == "pruevas_prototipo")
        //{
        //    //Guardamos posicion y la ponemos en el personaje
        //    PrefabProta.transform.position = vectorPosicion.load_pos();
        //}
    }
    //void Update()
    //{
    //    //pantalla tint
    //    if (preloadObj == null)
    //    {
    //        preloadObj = GameObject.Find("Player Character");
    //    }
    //    //encontrar el personaje prefab
    //    if (protagonista == null)
    //    {
    //        protagonista = GameObject.Find("personaje");
    //    }
  
    //}
     
    //void Start()
    //{
    //    //Para que no se destruya
    //    DontDestroyOnLoad(preloadObj);
    //    DontDestroyOnLoad(protagonista);
    //}
}
