using UnityEngine;

public class Cofre : MonoBehaviour
{
    [SerializeField] private AudioClip sonidoAbrir;
    [SerializeField] private AudioClip sonidoCerrar;

    private bool abierto = false;

    public void Interactuar()
    {
        abierto = !abierto;

        if (abierto)
        {
            ControladorSonido.Instance.EjecutarSonido(sonidoAbrir);

            // Animación de abrir
        }
        else
        {
            ControladorSonido.Instance.EjecutarSonido(sonidoCerrar);

            // Animación de cerrar
        }
    }
}