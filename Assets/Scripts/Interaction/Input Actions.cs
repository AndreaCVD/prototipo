using UnityEngine;
using UnityEngine.InputSystem;

public class InputActions : MonoBehaviour
{


    void Start()
    {    
        // Crear un mapa de acción en código
        var gameplayMap = new InputActionMap("Gameplay");
        // Añadir acciones al mapa
        var jumpAction = gameplayMap.AddAction("Jump");
        var fireAction = gameplayMap.AddAction("Fire");
    }
    // Habilitar todo el mapa (activa Jump y Fire)
    //gameplayMap.Enable();   
}
