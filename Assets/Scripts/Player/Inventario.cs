using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    [SerializeField] Parameters prota;
    //Para encontrar los scripts de DialogManager
    private Dialog dialog;
    private GameObject script_dialog;
    //----------
    [SerializeField] List<int> Llaves = new List<int>();
    private GameObject posible_llave;
    //public List<int[]> x = new List<int[]>();


    private void Start()
    {
        //prota.Inventario.Clear();
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
            prota.Inventario.Llave.Add(other.gameObject.name);          
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "LlaveMaestra")
        {
            prota.Inventario.Llave.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "PocionVida")
        {
            prota.Inventario.Llave.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Daga")
        {
            prota.Inventario.Llave.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Espada")
        {
            prota.Inventario.Llave.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
    }

    public void CofreKey(GameObject cofre, cherrydev.DialogNodeGraph dialogo_obj)
    {
        //Llaves = new List<int>(prota.Inventario);

        //Llaves = prota.Inventario;
        //List<string> listaB = new List<string>(listaA);
        bool keyFound = false;
        //Leer el nombre de este cofre
        string nombre = this.name; //Prota es THIS
        //Recorremos nuestro inventario para ver si coincide alguna
        //foreach (string[] a in Llaves)
        //{
        //    if (a == cofre.name)
        //    {
        //        Debug.Log(a);
        //        Si tiene mismo nombre se abre
        //        AbrirCofre();
        //        keyFound = true;
        //        break;
        //    }
        //}
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
