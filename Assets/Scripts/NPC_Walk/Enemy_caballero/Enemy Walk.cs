using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    public NavMeshAgent enemy;
    public float velocidad;
    public bool persiguiendo;
    public float rango; //distancia en que te ve el enemigo

    public float distancia; //valor que representa que tan lejos el enemigo esta del prota
    float distanciaExtra;
    public Transform objetivo; //lo que persigue el enemigo
   
    [Header("Animaciones")]
    public Animation anim;
    public string nombreAnimacionWalk;
    public string nombreAnimQuieto;

    private void Update()
    {
        //primero hacemos que se calcule la distancia entre el y el jugador
        distancia = Vector3.Distance(enemy.transform.position, objetivo.position);

        //que el eneimgo detecte el jugador a cierta distancia
        if(distancia < rango)
        {
            persiguiendo = true;
            Debug.Log("persiguiendo");
        }
        else if(distancia > rango + distanciaExtra)
        {
            persiguiendo = false;
            Debug.Log("no persiguiendo");

        }

        if (!persiguiendo)
        {
            enemy.speed = 0;
            //anim.CrossFade("Stand");
        }
        else if (persiguiendo)
        {
            enemy.speed = velocidad;
            ///anim.CrossFade(nombreAnimacionWalk);
            enemy.SetDestination(objetivo.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.transform.position, rango); //dibujar el radio del rango (centro, rango)
    }
}
