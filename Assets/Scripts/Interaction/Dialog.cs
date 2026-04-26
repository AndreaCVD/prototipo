using System.Collections.Generic;
using UnityEngine;


    public class Dialog : MonoBehaviour
    {
        [Header("EL PREFAB")]
        [SerializeField] private cherrydev.DialogBehaviour _dialogBehaviour;
        [Header("DIALOGOS PERSONAJES")]
        [SerializeField] List<cherrydev.DialogNodeGraph> Viejo = new List<cherrydev.DialogNodeGraph>();
        [SerializeField] List<cherrydev.DialogNodeGraph> Nim = new List<cherrydev.DialogNodeGraph>();
        [SerializeField] List<cherrydev.DialogNodeGraph> Etkis = new List<cherrydev.DialogNodeGraph>();
        [SerializeField] List<cherrydev.DialogNodeGraph> Lerendur = new List<cherrydev.DialogNodeGraph>();
        [SerializeField] List<cherrydev.DialogNodeGraph> Libro = new List<cherrydev.DialogNodeGraph>();


        ////La conversa, podemos tener todas las conversas guardadas y enviar la que se necesite
        //[SerializeField] private DialogNodeGraph dialogGraph;

        ////Para que el dialogo se active necesitamos esto:
        //private void Start()
        //{
        //    _dialogBehaviour.StartDialog(dialogGraph);
        //}
        public void StartDialog(cherrydev.DialogNodeGraph dialogo)
        {
            //Le enviamos el dialogo que tiene que hacer
            _dialogBehaviour.StartDialog(dialogo);
        }
    }

