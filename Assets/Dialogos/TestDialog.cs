using System.Collections.Generic;
using UnityEngine;

namespace cherrydev
{
    public class TestDialog : MonoBehaviour
    {
        [Header("EL PREFAB")]
        [SerializeField] private DialogBehaviour _dialogBehaviour;
        [Header("DIALOGOS PERSONAJES")]
        [SerializeField] List<DialogNodeGraph> Viejo = new List<DialogNodeGraph>();
        [SerializeField] List<DialogNodeGraph> Nim = new List<DialogNodeGraph>();
        [SerializeField] List<DialogNodeGraph> Etkis = new List<DialogNodeGraph>();
        [SerializeField] List<DialogNodeGraph> Lerendur = new List<DialogNodeGraph>();
        [SerializeField] List<DialogNodeGraph> Libro = new List<DialogNodeGraph>();


        //La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
        [SerializeField] private DialogNodeGraph dialogGraph;

        //Para que el dialogo se active necesitamos esto:
        private void Start()
        {
            _dialogBehaviour.StartDialog(dialogGraph);
        }
    }
}
