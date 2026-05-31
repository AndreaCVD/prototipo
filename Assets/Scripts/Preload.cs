using System.Collections;
using System;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using System.Collections.Generic.Dictionary;

public class ListaPuzzles
{
    public string name;
    public bool acabado;
}
//Cargar todas las cosas antes de la escena normal, no combate
public class Preload : MonoBehaviour
{
    private SavePuzzle savePuzzle;
    
    private GameObject script_dialog;
    private Dialog dialog;
    
    [SerializeField] List<ListaPuzzles> ListaPuzzles = new List<ListaPuzzles>();   

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

    public void puzzleTrue(string namePuzzle)
    {

        foreach (var obj in ListaPuzzles)
        {
            if (obj.name.Equals(namePuzzle)) //que funcion mas divertida me acaba de aparecer con el tab jahsjsahjashjash
            {
                obj.acabado = true;
            }
        }


    }
    //void boolTrue(string namePuzzle)
    //{
    //    Puzzles_Pruevas[namePuzzle] = true;
    //}
}
