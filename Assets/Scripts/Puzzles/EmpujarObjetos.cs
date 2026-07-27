using UnityEngine;

public class EmpujarObjetos : MonoBehaviour
{
    [SerializeField] public int pesoNecesario = 5; // fuerza mínima para mover la caja

    private Inventario inventario;

    public float distanciaCasilla = 1f;   // Tamaño de la casilla (1 unidad por defecto)
    public float velocidadMovimiento = 5f; // Velocidad de desplazamiento
    private bool enMovimiento = false;
    private Vector3 destino;

    private LayerMask layerMask;
    public bool puzzleFinished;

    void Start()
    {
        GameObject aux = GameObject.Find("-- SavePuzzle --");
        //savePuzzle = aux.GetComponent<SavePuzzle>();

        inventario = GameObject.Find("Player Character").GetComponentInChildren<Inventario>();

        destino = transform.position; // Posición inicial

        puzzleFinished = false; //Leer de Preload
        
        layerMask = 1 << LayerMask.NameToLayer("FinPuzzle");
        //enviarVal(false);
    }

    void Update()
    {
        // Movimiento suave hacia el destino
        if (enMovimiento)
        {
            transform.position = Vector3.MoveTowards(transform.position, destino, velocidadMovimiento * Time.deltaTime);

            // Cuando llega al destino, detener el movimiento
            if (Vector3.Distance(transform.position, destino) < 0.001f)
            {
                transform.position = destino;
                enMovimiento = false;
            }
        }
    }
    public bool returnState()
    {
        return puzzleFinished;
    }
    public void restartState()
    {
        puzzleFinished = false;
    }
    void OnCollisionEnter(Collision col)
    {
        int fuerzaJugador = inventario.prota.stats.Get(PersonajesStats.Fuerza);

        if (fuerzaJugador < pesoNecesario)
        {
            Debug.Log("No tienes suficiente fuerza para mover esto");
            return;
        }


        // Si el jugador choca con este objeto y el puzzle no esta completado
        if (col.gameObject.CompareTag("Player") && !enMovimiento && !puzzleFinished)
        {
            // Dirección del empuje (basada en la posición del jugador)
            Vector3 direccion = (transform.position - col.transform.position).normalized;

            //o se mueve en X o en Y, no en diagonal

            // Redondear dirección a ejes principales (para moverse en grid)
            direccion = new Vector3(Mathf.Round(direccion.x), 0f, Mathf.Round(direccion.z));

            // Bloquear diagonal
            if (direccion.x != 0 && direccion.z != 0) return;
            if (direccion == Vector3.zero) return;

            // Calcular nueva posición
            Vector3 nuevaPos = transform.position + direccion * distanciaCasilla;

            //Comprovar tmb si esta el final del puzzle, que se mueva igual
            // Comprobar si hay algo en la nueva posición --> da TRUE o FALSE
            if (!Physics.CheckBox(nuevaPos, Vector3.one * 0.4f))
            {
                destino = nuevaPos;
                enMovimiento = true;
                //LayerMask layer;

            }
            else //Y si delante esta el final del puzzle
            {
                Collider[] hit = Physics.OverlapBox(nuevaPos, Vector3.one * 0.4f, Quaternion.identity, layerMask);
                //Para ver el tag:
                foreach (var colision in hit)
                {
                    if ( colision.gameObject.CompareTag("FinalPuzzle"))
                    {
                        //Debug.Log("El puzzle se ha completado");
                        destino = nuevaPos;
                        enMovimiento = true;
                        puzzleFinished = true;
                        enviarVal();
                    }

                }
                //Physics.OverlapBox --> saber que obj hay
                // centro, halfExtens, orientacion, layerMask

            }
        }
        if (col.gameObject.CompareTag("SueloPuzzle") && !enMovimiento)
        {
            //enMovimiento = true;
            puzzleFinished = true;
            Debug.Log("Suelo puzzle alcanzado");
        }

    }
    private void enviarVal()
    {
        Interactable interact = this.GetComponent<Interactable>();
        interact.PuzzleFinished(true);
    }
    //si encuentra suelo mazmorra (negro) se tiene que bloquear

}
