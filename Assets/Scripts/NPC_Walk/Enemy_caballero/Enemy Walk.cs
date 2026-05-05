using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyWalk : MonoBehaviour
{
    private LoadScene load;
    private GameObject script_load;

    public bool isHome;
    [SerializeField] Transform home;
    [SerializeField] Transform pivot_caballero;
    public float distanciaRayCast;
    public bool firstWalk;

    private float tiempoEspera = 3.5f;
    //private Coroutine miCoroutine;

    [Header("Conf Walk")]
    [SerializeField] NavMeshAgent enemy;
    [SerializeField] float velocidad;
    [SerializeField] bool persiguiendo;
    [SerializeField] float rango; //distancia en que te ve el enemigo

    [SerializeField]  float distancia; //valor que representa que tan lejos el enemigo esta del prota
    float distanciaExtra;
    [SerializeField] Transform objetivo; //lo que persigue el enemigo
   
    [Header("Animaciones")]
    Animation anim;
    string nombreAnimacionWalk;
    string nombreAnimQuieto;
    //[Header("Raycast")]
    RaycastHit hitInfo; //Informacion de cuando el raycast del personaje se encuentre con un obj
    Ray ray;
    void Start()
    {
        isHome = true;
        firstWalk = false;

        if (script_load == null)
        {
            script_load = GameObject.Find("--SceneManagement--");
            load = script_load.GetComponent<LoadScene>();
        }
    }
    private void Update()
    {
        //a la primera vez, la estatua no se mueve hasta que el prota pasa por delante
        //De dnd sale y adonde va
        if (!firstWalk && isHome)
        {
            ray = new Ray(pivot_caballero.transform.position, pivot_caballero.transform.forward);
            Invoke(nameof(Interact), 1.0f);
        }

        if (firstWalk)
        {
            //primero hacemos que se calcule la distancia entre el y el jugador
            distancia = Vector3.Distance(enemy.transform.position, objetivo.position);

            //que el eneimgo detecte el jugador a cierta distancia
            if (distancia < rango && !persiguiendo)
            {
                //if (isHome)
                //{
                //    isHome = false;
                //}
                persiguiendo = true;
                Debug.Log("persiguiendo");
            }
            else if (distancia > rango + distanciaExtra && persiguiendo)
            {
                persiguiendo = false;
                Debug.Log("no persiguiendo");

            }

            if (!persiguiendo)
            {
                //if (!isHome)
                //{
                //    //si despues de X tiempo aun no vuelve a pareer el plaer vuelve a la posicion
                //    StartCoroutine(EsperarYComprovar());
                //}

                enemy.speed = 0;
                //anim.CrossFade("Stand");
            }
            else if (persiguiendo)
            {
                //StopCoroutine(miCoroutine);
                enemy.speed = velocidad;
                ///anim.CrossFade(nombreAnimacionWalk);
                enemy.SetDestination(objetivo.position);
            }
        }
    }
    //Si hay collide vamos al combate
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player") )
        {
            //Combat(GameObject enemyName)
            load.Combat(this.gameObject);
        }
    }
            //Volver a la posicion original si no esta el prota en X tiempo
            IEnumerator EsperarYComprovar()
    {
        yield return new WaitForSeconds(tiempoEspera);
        Debug.Log("Han pasado " + tiempoEspera + " segundos.");
        if ( !persiguiendo && !isHome)
        {
            Debug.Log("???????????");
            enemy.speed = velocidad;
            enemy.SetDestination(home.transform.position);
            //transform.position = Vector3.MoveTowards(transform.position, destino, velocidadMovimiento * Time.deltaTime);

            isHome = true;
        }
    }
    public void Interact()
    {
        //detectar todos los objetos DELANTE del enemigo
        //Collider[] colliders = Physics.OverlapBox(pivot.position, interactAreaSize);
        //Distancia máxima del ray, sino con Mathf.Infinity no tiene limite
        if (Physics.Raycast(ray, out hitInfo, distanciaRayCast))
        {
            Debug.DrawRay(ray.origin, ray.direction * 1f, Color.red);
            if (hitInfo.transform.tag == "Player")
            {
                Debug.Log("El jugador ha pasado por delante");
                firstWalk = true;
            }
        }
    }
    //Dibujar el rango en unity
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(enemy.transform.position, rango); //dibujar el radio del rango (centro, rango)
    }
}
