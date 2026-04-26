using System;
using System.Collections.Generic;
using UnityEngine;



public class Dialog : MonoBehaviour
{
    [Header("EL PREFAB")]
    [SerializeField] private cherrydev.DialogBehaviour _dialogBehaviour;
    [Header("DIALOGOS PERSONAJES")]
    [SerializeField] List<cherrydev.DialogNodeGraph> Viejo = new List<cherrydev.DialogNodeGraph>();
    [SerializeField] List<cherrydev.DialogNodeGraph> Nim = new List<cherrydev.DialogNodeGraph>();
    [SerializeField] List<cherrydev.DialogNodeGraph> Etkis = new List<cherrydev.DialogNodeGraph>();
    [SerializeField] List<cherrydev.DialogNodeGraph> Lerendur = new List<cherrydev.DialogNodeGraph>();
    [SerializeField] List<cherrydev.DialogNodeGraph> Libro = new List<cherrydev.DialogNodeGraph>();

    private GameObject obj;

    ////La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
    //[SerializeField] private DialogNodeGraph dialogGraph;

    ////Para que el dialogo se active necesitamos esto:
    //private void Start()
    //{
    //    _dialogBehaviour.StartDialog(dialogGraph);
    //}
    public void StartDialog(cherrydev.DialogNodeGraph dialogo, GameObject obj)
    {
        this.obj = obj;
        Debug.Log(this.obj);
        //Llamar a funcion
            //BindExternalFunction(string funcName, Action function);
        _dialogBehaviour.BindExternalFunction("Destroy", DestroyObj);

         //Le enviamos el dialogo que tiene que hacer --> ESTE SIEMPRE ÚLTIMO
        _dialogBehaviour.StartDialog(dialogo);


    }
    public void DestroyObj()
    {
        Destroy(obj);
    }
    public void SetBool(string nombreVal, bool val)
    {
        _dialogBehaviour.SetVariableValue(nombreVal, val);
    }
}

