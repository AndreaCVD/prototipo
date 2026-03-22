using UnityEngine;

public class EmpujarObjetos : MonoBehaviour
{
    public float distanciaCasilla = 1f;   // Tamaño de la casilla (1 unidad por defecto)
    public float velocidadMovimiento = 5f; // Velocidad de desplazamiento
    private bool enMovimiento = false;
    private Vector3 destino;

    void Start()
    {
        destino = transform.position; // Posición inicial
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

    void OnCollisionEnter(Collision col)
    {
        // Si el jugador choca con este objeto
        if (col.gameObject.CompareTag("Player") && !enMovimiento)
        {
            // Dirección del empuje (basada en la posición del jugador)
            Vector3 direccion = (transform.position - col.transform.position).normalized;

            //o se mueve en X o en Y, no en diagonal
            

            // Redondear dirección a ejes principales (para moverse en grid)
            direccion = new Vector3( Mathf.Round(direccion.x), 0f, Mathf.Round(direccion.z)
            );

            // Calcular nueva posición
            Vector3 nuevaPos = transform.position + direccion * distanciaCasilla;

            // Comprobar si hay algo en la nueva posición
            if (!Physics.CheckBox(nuevaPos, Vector3.one * 0.4f))
            {
                destino = nuevaPos;
                enMovimiento = true;
            }
        }
    }
}
