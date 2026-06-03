using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
public class LoadScene : MonoBehaviour
{
    private GameObject uiHub;
        [Header("Degradado pantalla")]
    private TintScreen pantalla;
        [Header("Datos prota")]
    private GameObject protagonista;
    GameObject obj_saveScript;
    private personaje save_posicion;
    private GameObject obj_input;
        [Header("Parar movimiento")]
    [SerializeField] InputHandler escenaState;
        [Header("Preparar el combate")]
    private Preload preload;
    private crear_obj destroyObjs;

    //[SerializeField] Preload preload;
    string name_anterior;
    bool onCombat;

    private void Start()
    {
        onCombat = false;

        destroyObjs = this.GetComponent<crear_obj>();
        preload = this.GetComponent<Preload>();
        pantalla = this.GetComponent<TintScreen>();
    }

    void Update()
    {

        //encontrar el personaje prefab 
        if (protagonista == null)
        {
            protagonista = GameObject.Find("personaje");
        }
        if (escenaState == null)
        {
            obj_input = GameObject.Find("personaje");
            escenaState = obj_input.GetComponent<InputHandler>();
            //save_posicion = GetComponent<personaje>();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //destroyObjs.destroyAll();
            ChangeScene("Start_MainMenu");
        }
    }

    public void ChangeScene(string sceneName) //Anar a una escena en especific
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "combat_scene")
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
            //preload.move_player();
            SceneManager.LoadScene(sceneName);
            //PUZZLES --> Mirar si hay alguno ya hecho

        }
    }
    public void EscenaAnterior()//Tornar a una escena anterior
    {
        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "combat_scene")
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
            //save_posicion.save_LastPos();
            //preload.move_player();
            //if (sceneName == "combate_pruevas"){ }
            SceneManager.LoadScene(name_anterior);
        }
    }
    public void SalirCombate()//Salimos del combate
    {
        Debug.Log("Salimos de combate");
        onCombat = false;
        //si estamos en combate eliminar esta escena
        //Sacamos la pausa del juego principal
        escenaState.ScenePause(false); //false, se mueve

        //recibir loot

        // reactiva el HUD al salir del combate
        if (uiHub != null)
            uiHub.SetActive(true);
        else
            Debug.LogWarning("uiHub es null al salir — no se pudo reactivar");

        // Unload Scene
        SceneManager.UnloadSceneAsync("combat_scene");
    }
    public void Combat(GameObject enemyName)
    {
        if (!onCombat)
        {
            onCombat = true;

            name_anterior = SceneManager.GetActiveScene().name;

            escenaState.ScenePause(true); //true, se para
            pantalla.UnTint();

            // busca y oculta el HUD ANTES de cargar el combate
            uiHub = GameObject.Find("UI_HUB");
            if (uiHub != null)
                uiHub.SetActive(false);
            else
                Debug.LogWarning("UI_HUB no encontrado — comprueba el nombre del GameObject");


            preload.CombatOpponent(enemyName); //Pasem el nom

            //save_posicion.save_LastPos();
            SceneManager.LoadScene("combat_scene", LoadSceneMode.Additive);

        }

    }
    
    public void GameOver()
    {
        pantalla.UnTint();
 
        SceneManager.LoadScene("GameOver");
    }

}
