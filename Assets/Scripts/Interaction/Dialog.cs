using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;
using UnityEngine;
using cherrydev;

public class Dialog : MonoBehaviour
{
    LoadScene load;
    Dice dados;
    [Header("LISTA PUZZLE")]
    [SerializeField] Puzzle lista;
    [Header("PROTAGONISTA")]
    [SerializeField] Parameters prota;
    //[Header("Parar movimiento")]
    [SerializeField] InputHandler escenaState;
    [Header("EL PREFAB")]
    [SerializeField] private cherrydev.DialogBehaviour _dialogBehaviour;
    
    private GameObject obj;
    private int vidaMax = 50;
    private int objMax = 3;

    ////La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
    //[SerializeField] private DialogNodeGraph dialogGraph;

    ////Para que el dialogo se active necesitamos esto:
    //    _dialogBehaviour.StartDialog(dialogGraph);
    public void Awake()
    {
        //Setear todos las variables cuando se abre el juego por primera vez
    }
    public void Start()
    {
        dados = this.GetComponent<Dice>();

        GameObject aux = GameObject.Find("--SceneManagement--");
        load = aux.GetComponent<LoadScene>();
    }
    public void EmpezarDialogo(DialogNodeGraph dialogo, GameObject obj)
    {
        //this.obj = obj;
        //Debug.Log(this.obj);
        //Llamar a funcion
        //BindExternalFunction(string funcName, Action function);

        //Dados
        _dialogBehaviour.BindExternalFunction("dadoFuerza", dadoFuerza);
        _dialogBehaviour.BindExternalFunction("dadoIntel", dadoIntel);
        _dialogBehaviour.BindExternalFunction("dadoCarisma", dadoCarisma);

        //Funciones pa todos
        _dialogBehaviour.BindExternalFunction("ContinuarMov", continuarMov);
        _dialogBehaviour.BindExternalFunction("PararMov", pararMov);
        _dialogBehaviour.BindExternalFunction("Destroy", DestroyObj);
        _dialogBehaviour.BindExternalFunction("Combat", Combate);
        _dialogBehaviour.BindExternalFunction("PisosDesbloqueados", pisosDesbloqueados);
        //Cambio escena
        _dialogBehaviour.BindExternalFunction("irCampamento", irCampamento);
        _dialogBehaviour.BindExternalFunction("irNivel_1", irNivel_1);
        _dialogBehaviour.BindExternalFunction("irNivel_2", irNivel_2);
        _dialogBehaviour.BindExternalFunction("irNivel_3", irNivel_3);
        //Personajes
        _dialogBehaviour.BindExternalFunction("EstadoEtkis", estadoEtkis);
        _dialogBehaviour.BindExternalFunction("EstadoNim", estadoNim);

        //Prota
        _dialogBehaviour.BindExternalFunction("RecuperarVida", vidaParcial);
        _dialogBehaviour.BindExternalFunction("FullVida", fullVida);
        _dialogBehaviour.BindExternalFunction("randLoot", lootRandom);
        //Le enviamos el dialogo que tiene que hacer --> ESTE SIEMPRE ÚLTIMO
        _dialogBehaviour.StartDialog(dialogo);

    }
    //Dados
    public void dadoFuerza()
    {
        int aux = dados.RollDice(20);
        int tirada = prota.stats.Get(PersonajesStats.Fuerza) + aux;
        _dialogBehaviour.SetVariableValue("tiradaFuerza", tirada);
    }
    public void dadoIntel()
    {
        int aux = dados.RollDice(20);
        int tirada = prota.stats.Get(PersonajesStats.Inteligencia) + aux;
        _dialogBehaviour.SetVariableValue("tiradaIntel", tirada);

    }
    public void dadoCarisma()
    {
        int aux = dados.RollDice(20);
        int tirada = prota.stats.Get(PersonajesStats.Carisma) + aux;
        _dialogBehaviour.SetVariableValue("tiradaCarisma", tirada);

    }
    //Moviment
    public void continuarMov()
    {
        escenaState.ScenePause(false); //false, se mueve
    }
    public void pararMov()
    {
        escenaState.ScenePause(true); //true, no se mueve
    }
    public void DestroyObj()
    {
        Destroy(obj);
    }
    public void Combate()
    {
        Debug.Log("Inicia combate por dialogo");
        load.Combat(obj);
    }
    public void SetBool(string nombreVal, bool val)
    {
        Debug.Log(val);
        _dialogBehaviour.SetVariableValue(nombreVal, val);
    }
    public void irCampamento()
    {
        load.ChangeScene("Nivel_0");
    }
    public void irNivel_1()
    {
        load.ChangeScene("Nivel_1");
    }
    public void irNivel_2()
    {
        load.ChangeScene("Nivel_2");
    }
    public void irNivel_3()
    {
        load.ChangeScene("Nivel_3");
    }
    public void pisosDesbloqueados()
    {
        //Nivel 1
        if (lista.NivelDesbloqueado[1].acabado)
        {
            _dialogBehaviour.SetVariableValue("nivel_1", 1);
        }
        else
        {
            _dialogBehaviour.SetVariableValue("nivel_1", 0);
        }
        //Nivel 2
        if (lista.NivelDesbloqueado[2].acabado)
        {
            _dialogBehaviour.SetVariableValue("nivel_2", 1);
        }
        else
        {
            _dialogBehaviour.SetVariableValue("nivel_2", 0);
        }
        //Nivel 3
        if (lista.NivelDesbloqueado[3].acabado)
        {
            _dialogBehaviour.SetVariableValue("nivel_3", 1);
        }
        else
        {
            _dialogBehaviour.SetVariableValue("nivel_3", 0);
        }
    }
    public void estadoEtkis()
    {
        if (lista.Nivel_1[1].acabado)
        {
            //Debug.Log("Etkis es libre");
            _dialogBehaviour.SetVariableValue("etkis_libre_nivel1", 1);
        }
        else
        {
            //Debug.Log("Etkis no es libre");
            _dialogBehaviour.SetVariableValue("etkis_libre_nivel1", 0);
        }
    }
    public void estadoNim()
    {
        if (lista.Nivel_2[2].acabado)
        {
            Debug.Log("Nim es libre");
            _dialogBehaviour.SetVariableValue("nim_libre_nivel2", 1);
        }
        else
        {
            Debug.Log("Nim no es libre");
            _dialogBehaviour.SetVariableValue("nim_libre_nivel2", 0);
        }
    }
    public void vidaParcial()
    {
        //Tint screen, fer una transició
        //Consitucion --> 3
        if (prota.stats.values[3].value <= (vidaMax-10))
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
    //Cofre
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

