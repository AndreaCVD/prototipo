using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Cargar todas las cosas antes de la escena normal, no combate
//esto no se destruira entre escenas
public class Preload : MonoBehaviour
{
    //Para encontrar los scripts de DialogManager
    //[SerializeField] Dialog dialog;
    //private GameObject script_dialog;
    [SerializeField] List<bool> Puzzles_Pruevas = new List<bool>();

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


    void Awake()
    {       
        preloadObj = GameObject.Find("--Preload--");
        PrefabProta = GameObject.Find("personaje");
        vectorPosicion = GetComponent<personaje>();
        protagonista = GameObject.Find("Player Character");

        Scene escenaActual = SceneManager.GetActiveScene();
        if (escenaActual.name == "pruevas_prototipo")
        {
            //Guardamos posicion y la ponemos en el personaje
           // PrefabProta.transform.position = vectorPosicion.load_LastPos();
        }
        //NameOpponent = "nada";
    }

    public void CombatOpponent(string enemyName)
    {
        
        NameOpponent = enemyName;
        Debug.Log(NameOpponent);
        //switch (enemyName)
        //{
        //    case "Cubo":
        //        NameOpponent = name;
        //        break;
        //    default:
        //        Debug.Log("");
        //        break;
        //} 
    }
    public string nameOpponent()
    {
        return NameOpponent;
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
