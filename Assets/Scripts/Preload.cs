using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Cargar todas las cosas antes de la escena normal, no combate
//esto no se destruira entre escenas
public class Preload : MonoBehaviour
{
    //Para encontrar los scripts de DialogManager
    [SerializeField] Dialog dialog;
    private GameObject script_dialog;
    //Listas
    [SerializeField] List<bool> Puzzles_Pruevas = new List<bool>();
    [SerializeField] List<GameObject> Puertas = new List<GameObject>();

    //guardar las variables para no perderlas
    private GameObject preloadObj;
    //public Vector3 posicion;
    private GameObject PrefabProta;
    //hacer que el personaje no se destruya
    private GameObject protagonista;
    //para recibir la posicion anterior
    personaje vectorPosicion;
    //Para Combat Debug
    [SerializeField] string NameOpponent;
    public GameObject enemigo;

    void Awake()
    {       
        //preloadObj = GameObject.Find("--Preload--");
        //PrefabProta = GameObject.Find("personaje");
        //vectorPosicion = GetComponent<personaje>();
        //protagonista = GameObject.Find("Player Character");

        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "pruevas_prototipo")
        {
            //Guardamos posicion y la ponemos en el personaje
           // PrefabProta.transform.position = vectorPosicion.load_LastPos();
        }
        //NameOpponent = "nada";
    }

    public void CombatOpponent(GameObject enemyName)
    {
        enemigo = enemyName;
        NameOpponent = enemyName.name;
        Debug.Log(NameOpponent);

    }
    public string nameOpponent()
    {
        return NameOpponent;
    }
    public void DestroyEnemy()
    {
        if (enemigo != null)
        {   
            Destroy(enemigo);
        }
        else
        {
            Debug.Log("No hay enemigo a destruir");
        }
    }
    //puzzles que tienen que ser false:
    //void Start()
    //{
    //    dialog.SetBool("puzzleDone", false);
    //}


    //public void move_player()
    //{
    //    PrefabProta.transform.position = vectorPosicion.load_pos();
    //}
    void Start()
    {
        //if puzzle esta hecho --> colocar la puerta apartada para que se pueda passar
        // else, que la puerta bloquee el paso

        //encontrar la puerta por el nombre, foreach()
    }

}
