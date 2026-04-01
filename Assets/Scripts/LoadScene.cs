using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField] TintScreen pantalla;
    [SerializeField] GameObject protagonista;
    [SerializeField] personaje save_posicion;

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
        if (pantalla != null)
        {
            pantalla = GetComponent<TintScreen>();
        }
        //encontrar el personaje prefab
        if (protagonista != null)
        {
            protagonista = GameObject.Find("personaje(Clone)");
        }
        //scipt dnd guardar stats prota
        if (save_posicion != null)
        {
            save_posicion = GetComponent<personaje>();
        }
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
        SceneManager.LoadScene(sceneName);
    }
    
    //private void Position()
    //{
    //    Vector3 pos = protagonista.transform.position;
    //    //la enviamos
    //    save_posicion.save_pos(pos);
    //}
}
