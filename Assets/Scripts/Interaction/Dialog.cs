using System;
using System.Collections.Generic;
using UnityEngine;

using cherrydev;

public class Dialog : MonoBehaviour
{
    [Header("LISTA PUZZLE")]
    [SerializeField] Puzzle lista;
    [Header("EL PREFAB")]
    [SerializeField] private cherrydev.DialogBehaviour _dialogBehaviour;
    
    private GameObject obj;

    ////La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
    //[SerializeField] private DialogNodeGraph dialogGraph;

    ////Para que el dialogo se active necesitamos esto:
    //private void Start()
    //{
    //    _dialogBehaviour.StartDialog(dialogGraph);
    //}
    public void EmpezarDialogo(DialogNodeGraph dialogo, GameObject obj)
    {
        this.obj = obj;
        //Debug.Log(this.obj);
        //Llamar a funcion
            //BindExternalFunction(string funcName, Action function);
        _dialogBehaviour.BindExternalFunction("Destroy", DestroyObj);
        _dialogBehaviour.BindExternalFunction("EstadoEtkis", estadoEtkis);

        //Le enviamos el dialogo que tiene que hacer --> ESTE SIEMPRE ÚLTIMO
        _dialogBehaviour.StartDialog(dialogo);


    }
    public void DestroyObj()
    {
        Destroy(obj);
    }
    public void SetBool(string nombreVal, bool val)
    {
        Debug.Log(val);
        _dialogBehaviour.SetVariableValue(nombreVal, val);
    }

    public void estadoEtkis()
    {
        if (lista.Nivel_1[1].acabado)
        {
            Debug.Log("Etkis es libre");
            _dialogBehaviour.SetVariableValue("b", 1);
        }
        else
        {
            Debug.Log("Etkis no es libre");
            _dialogBehaviour.SetVariableValue("b", 0);
        }
    }
    public void estadoCofre()
    {
        //Ver si hemos completado el puzzle del cofre
    }
}

