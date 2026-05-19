using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    //Para encontrar los scripts de DialogManager
    private Dialog dialog;
    private GameObject script_dialog;
    //----------
    [SerializeField] List<string> Llaves = new List<string>();
    private GameObject posible_llave;

    private void Start()
    {
        if (script_dialog == null)
        {
            script_dialog = GameObject.Find("--DialogManager--");
            dialog = script_dialog.GetComponent<Dialog>();
        }
    }
    public void InsertKey(GameObject x)
    {
        posible_llave = x;
        Debug.Log("Cojera esa llave???");
    }
    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Llave")
        {
            //posible_llave = other.gameObject;
            Llaves.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    public void CofreKey(GameObject cofre, cherrydev.DialogNodeGraph dialogo_obj)
    {
        bool keyFound = false;
        //Leer el nombre de este cofre
        string nombre = this.name; //Prota es THIS
        //Recorremos nuestro inventario para ver si coincide alguna
        foreach(string a in Llaves)
        {
            if ( a == cofre.name)
            {
                Debug.Log(a);
                //Si tiene mismo nombre se abre
                AbrirCofre();
                keyFound = true;
                break;
            }
        }
        //Si el cofre comparte numero codigo con la llave --> se abre
        if (!keyFound)
        {
            dialog.EmpezarDialogo(dialogo_obj, cofre);
        }
    }
    private void AbrirCofre()
    {
        Debug.Log("El cofre se abre");

    }
}
