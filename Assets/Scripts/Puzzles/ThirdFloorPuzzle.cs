using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdFloorPuzzle : MonoBehaviour
{

    [SerializeField] LoadScene load;
    [SerializeField] Puzzle lista;
    public bool jefeFinal;


    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMaestra;
    private bool puertaAbierta;
    [SerializeField] Animator candado;


    [Header("Personajes a instanciar")]
    [SerializeField] List<GameObject> JefeFinal = new List<GameObject>();

    void Start()
    {
        jefeFinal = false;
        InstanciarPers();
        //finalBoss();
    }

    void Update()
    {
        //Desbloquear jefe final
        if (lista.NivelDesbloqueado[3].acabado && !puertaAbierta)
        {
            puertaAbierta = true;
            candado.SetBool("candadoOpen", true);
            puertaMaestra.SetBool("doorOpen", true);
        }
        //mirar pelea
        bool aux = load.boss();
        if (aux && !jefeFinal)
        {
            jefeFinal = true;
            lista.NivelDesbloqueado[4].acabado = true;
        }
    }


    public void revisarPuzzle()
    {
        //Vemos si hay un puzzle acabado
        //for (int i = 0; i < lista.Nivel_1.Count; i++)
        //{
        //    if (lista.Nivel_1[i].acabado)
        //    {
        //        switch (lista.Nivel_1[i].name)
        //        {
        //            case "Puzzle_B1":

        //                break;
        //            case "Puzzle_B2":

        //                break;
        //            default:
        //                break;
        //        }
        //    }
        //}

    }

    void InstanciarPers()
    {
        //Instanciar Llibre + Lerendur
        if (!load.boss() && !lista.NivelDesbloqueado[4].acabado)
        {
            Instantiate(JefeFinal[0], JefeFinal[1].transform.position, JefeFinal[1].transform.rotation);
        }
        //Quan guanyem, nomes al Llibre
    }
}
