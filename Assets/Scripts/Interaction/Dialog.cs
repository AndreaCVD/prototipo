using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class Dialog : MonoBehaviour
{
    [Header("LISTA PUZZLE")]
    [SerializeField] Puzzle lista;
    [Header("PROTAGONISTA")]
    [SerializeField] Parameters prota;
    [Header("EL PREFAB")]
    [SerializeField] private cherrydev.DialogBehaviour _dialogBehaviour;
    
    private GameObject obj;
    public int vidaMax = 40;
    public int objMax = 3;

    ////La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
    //[SerializeField] private DialogNodeGraph dialogGraph;

    ////Para que el dialogo se active necesitamos esto:
    //    _dialogBehaviour.StartDialog(dialogGraph);

    public void EmpezarDialogo(DialogNodeGraph dialogo, GameObject obj)
    {
        //this.obj = obj;
        //Debug.Log(this.obj);
        //Llamar a funcion
            //BindExternalFunction(string funcName, Action function);
        _dialogBehaviour.BindExternalFunction("Destroy", DestroyObj);
        _dialogBehaviour.BindExternalFunction("EstadoEtkis", estadoEtkis);
        _dialogBehaviour.BindExternalFunction("RecuperarVida", vidaParcial);
        _dialogBehaviour.BindExternalFunction("FullVida", fullVida);
        _dialogBehaviour.BindExternalFunction("randLoot", lootRandom);
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
    public void vidaParcial()
    {
        //Tint screen, fer una transició
        //Consitucion --> 3
        if (prota.stats.values[3].value >= (vidaMax-10))
        {
            prota.stats.values[3].value += 10;
        }
        else
        {
            prota.stats.values[3].value = vidaMax;
        }
    }
    public void fullVida()
    {
        prota.stats.values[3].value = vidaMax;
    }

    public void lootRandom()
    {
        int a = Random.Range(1, objMax);
        switch (a)
        {
            case 1:
                prota.Inventario.Espada.Add("Espada_Loot");
                break;
            case 2:
                prota.Inventario.Daga.Add("Daga_Loot");
                break;
            case 3:
                prota.Inventario.PocionVida.Add("PocionVida_Loot");
                break;
            default:
                break;
        }
    }
}

