using UnityEngine;

public class SonidoInteractuable : MonoBehaviour
{
    [SerializeField] private AudioClip sonido;

    public void ReproducirSonido()
    {
        ControladorSonido.Instance.EjecutarSonido(sonido);
    }
}
