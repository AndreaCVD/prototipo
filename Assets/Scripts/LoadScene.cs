using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class LoadScene : MonoBehaviour
{
    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;
    [Header("Datos prota")]
    [SerializeField] GameObject protagonista;
    GameObject obj_saveScript;
    [SerializeField] personaje save_posicion;
    GameObject obj_input;
    [Header("Parar movimiento")]
    [SerializeField] InputHandler escenaState;
    [Header("Preparar el combate")]
    [SerializeField] Preload preload;

    //[SerializeField] Preload preload;
    string name_anterior;
    bool onCombat;

    private void Start()
    {
        onCombat = false;
    }

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
        if (escenaState == null)
        {
            obj_input = GameObject.Find("personaje");
            escenaState = obj_input.GetComponent<InputHandler>();
            //save_posicion = GetComponent<personaje>();
        }
        //if (preload == null)
        //{
        //    preload = GetComponent<Preload>();
        //}
    }

    public void ChangeScene(string sceneName) //Anar a una escena en especific
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "combate_pruevas")
        {
            //si estamos en combate eliminar esta escena
            //Sacamos la pausa del juego principal
            escenaState.ScenePause(false); //false, se mueve
            // Unload Scene
            SceneManager.UnloadSceneAsync(escenaActual);
            onCombat = false;
        }
        else
        {
            pantalla.UnTint();
            save_posicion.save_LastPos();
            //preload.move_player();
            //if (sceneName == "combate_pruevas"){ }
           SceneManager.LoadScene(sceneName);
        }
    }
    public void EscenaAnterior()//Tornar a una escena anterior
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "combate_pruevas")
        {
            onCombat = false;
            //si estamos en combate eliminar esta escena
            //Sacamos la pausa del juego principal
            escenaState.ScenePause(false); //false, se mueve
            // Unload Scene
            SceneManager.UnloadSceneAsync(escenaActual);

        }
        else
        {
            pantalla.UnTint();
            save_posicion.save_LastPos();
            //preload.move_player();
            //if (sceneName == "combate_pruevas"){ }
            SceneManager.LoadScene(name_anterior);
        }
    }
    public void SalirCombate()//Salimos del combate
    {
        Scene escenaActual = SceneManager.GetActiveScene();

        onCombat = false;
        //si estamos en combate eliminar esta escena
        //Sacamos la pausa del juego principal
        escenaState.ScenePause(false); //false, se mueve
        // Unload Scene
        SceneManager.UnloadSceneAsync(escenaActual);

    }
    public void Combat(/*GameObject enemyName*/)
    {
        if (!onCombat)
        {
            onCombat = true;

            name_anterior = SceneManager.GetActiveScene().name;

            escenaState.ScenePause(true); //true, se para
            pantalla.UnTint();

            //preload.CombatOpponent(/*enemyName.name*/); //Pasem el nom

            //save_posicion.save_LastPos();
            SceneManager.LoadScene("combate_pruevas", LoadSceneMode.Additive);
        }

    }
    
    public void GameOver()
    {
        pantalla.UnTint();
        save_posicion.save_LastPos();
        //preload.move_player();
        //if (sceneName == "combate_pruevas"){ }
        SceneManager.LoadScene("GameOver");
    }
    //private void Position()
    //{
    //    Vector3 pos = protagonista.transform.position;
    //    //la enviamos
    //    save_posicion.save_pos(pos);
    //}
}
