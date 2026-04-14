using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walk : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent AI;
    public float velocidad;
    public Transform[] Objetivos; //Pts a los que se puede dirigir el NPC
    Transform Objetivo;
    private int destino = 1;
    float Distancia; //saber disstancia NPC y obj

    void Start()
    {
        Objetivo = Objetivos[0];
    }

    void Update()
    {
        //NPC detecte la distancia entre el mismo, y el objetivo
        Distancia = Vector3.Distance(transform.position, Objetivo.position);

        if (Distancia < 2)
        {
            //cuando llegue a su obj elegir otro
            Objetivo = Objetivos[destino];
            destino = destino + 1;
        }
        //Enviamos el NPC al obj
        AI.destination = Objetivo.position;
        //Velocidad a la que ira
        AI.speed = velocidad;

        if (destino == 2)
        {
            destino = 0;
        }
    }
}
