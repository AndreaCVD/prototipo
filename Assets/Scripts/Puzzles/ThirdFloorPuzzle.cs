using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ThirdFloorPuzzle : MonoBehaviour
{

    [SerializeField] LoadScene load;
    [SerializeField] Puzzle lista;
    public bool jefe_Lerendur;
    public bool jefe_Libro;


    [Header("Obj a modificar al acabar puzzle")]
    [SerializeField] Animator puertaMaestra;
    private bool puertaAbierta;
    [SerializeField] Animator candado;


    [Header("Personajes a instanciar")]
    [SerializeField] List<GameObject> JefeFinal = new List<GameObject>();

    void Start()
    {
        jefe_Lerendur = lista.Jefes[0].acabado;
        jefe_Libro = lista.Jefes[1].acabado;

        if (jefe_Lerendur &&  !jefe_Libro)
        {
            Debug.Log("Libro vuelve a tener control sobre Lerendur");
            lista.Jefes[0].acabado = false;
            jefe_Lerendur = false;
        }
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
        if (!jefe_Lerendur || !jefe_Libro)
        {
            bool lerendur = load.boss_Lerendur();
            bool libro = load.boss_Libro();
            if (lerendur && !jefe_Lerendur)
            {
                jefe_Lerendur = true;
                lista.Jefes[0].acabado = true;
                InstanciarPers();
            }
            else if (libro && !jefe_Libro)
            {
                jefe_Libro = true;
                lista.Jefes[1].acabado = true;
            }
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
        if (!jefe_Lerendur /*&& !lista.NivelDesbloqueado[3].acabado*/)
        {
            Instantiate(JefeFinal[0], JefeFinal[1].transform.position, JefeFinal[1].transform.rotation);
        }
        //Quan guanyem, nomes al Llibre
        else if (!jefe_Libro /*&& !lista.NivelDesbloqueado[4].acabado*/)
        {
            Instantiate(JefeFinal[2], JefeFinal[1].transform.position, JefeFinal[1].transform.rotation);
        }
    }
}
