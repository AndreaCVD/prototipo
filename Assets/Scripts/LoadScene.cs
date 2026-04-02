using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField] TintScreen pantalla;
    [SerializeField] GameObject protagonista;
    GameObject obj_saveScript;
    [SerializeField] personaje save_posicion;
    //[SerializeField] Preload preload;
    //moverse entre escenas
    //public void teleport()
    //{
    //    pantalla.UnTint();
    //    SceneManager.LoadScene("pruevas_prototipo");

    //}

    //public void combate()
    //{
    //    //Antes de irnos tenemos que guardar posicion personaje
    //    Position();
    //    pantalla.UnTint();
    //    SceneManager.LoadScene("combate_pruevas");
    //}

    void Update()
    {
        //pantalla tint
        if (pantalla == null)
        {
            pantalla = GetComponent<TintScreen>();
        }
        //encontrar el personaje prefab
        if (protagonista == null)
        {
            protagonista = GameObject.Find("personaje");
        }
        //scipt dnd guardar stats prota
        if (obj_saveScript == null)
        {
            obj_saveScript = GameObject.Find("--Preload--");
            save_posicion = obj_saveScript.GetComponent<personaje>();
            //save_posicion = GetComponent<personaje>();
        }
        //if (preload == null)
        //{
        //    preload = GetComponent<Preload>();
        //}
    }

    public void ChangeScene(string sceneName)
    {
        //Scene escenaActual = SceneManager.GetActiveScene();
        //if (escenaActual.name == "pruevas_prototipo")
        //{
        //    //Si estamos en mapa normal, no en combate, guardaremos la posicion
        //    save_posicion.save_pos();

        //}

        pantalla.UnTint();
        save_posicion.save_LastPos();
        //preload.move_player();
        //if (sceneName == "combate_pruevas")
        //{

        //}
        SceneManager.LoadScene(sceneName);
    }
    
    //private void Position()
    //{
    //    Vector3 pos = protagonista.transform.position;
    //    //la enviamos
    //    save_posicion.save_pos(pos);
    //}
}
