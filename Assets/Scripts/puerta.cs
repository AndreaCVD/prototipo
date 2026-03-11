using System.Collections.Generic;
using UnityEngine;

public class puerta : MonoBehaviour
{
    [Header("Info Obj")]
    public GameObject obj_actual;
    [Header("Opacidad Canva")]
    public centro_interaccion centro;
    public Animation animacion;
    //[Header("Opciones Fuerza")]
    string fuerza = "1 - Romper Puerta";
    string intel = "2 - Buscar Llave";
    string carisma = "3 - Abrete Sesamo";

    bool open_door;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       open_door = false;
    }
    //Entrar en la zona
    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") && open_door == false)
        {
            Debug.Log("Player ha entrado de la Zona");
            //TEXTOS
            centro.textos(fuerza, intel, carisma);
            //OPACIDAD
            centro.opacidad(1f);
        }
    }

    void FixedUpdate ()
    {
        //Depende de lo que elija la opcion sera uno u otra
        if (Input.GetKeyDown("1"))
        {
            //Opcion fuerza
            Debug.Log("Opcion Fuerza - Destruir puerta");
            Destroy(obj_actual);
        }
        if (Input.GetKeyDown("2"))
        {
            //Opcion inteligencia
            Debug.Log("Opcion Inteligencia - Buscar llave");
            //int rotacion = Vector3 (0, 90, 0)
            obj_actual.transform.Rotate(0, 90, 0);
            obj_actual.transform.Translate(0f, 0f, -1.5f);
            //animacion.Play();
            open_door=true;
        }
        if (Input.GetKeyDown("3"))
        {
            //Opcion carisma
            Debug.Log("Opcion Carisma - Sesamo");
            Debug.Log("No ha servido de nada");
            centro.opacidad(0f);
        }
        /*
        //Depende de lo que elija la opcion sera uno u otra
            switch (Input.GetKeyDown)
        {
            //Opcion fuerza
            case 1:
                Debug.Log("Opcion Fuerza - Destruir puerta");
                Destroy(obj_actual);
                break;
            //Opcion inteligencia
            case 2:
                Debug.Log("Opcion Inteligencia - Buscar llave");
                obj_actual.transform.Rotate(0, 90, 0);
                break;
            //Opcion carisma
            case 3:
                Debug.Log("Opcion Carisma - Sesamo");
                break;
        }*/
    }
    //Salir de la zona
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player ha salido de la Zona");
            //OPACIDAD
            centro.opacidad(0f);
        }
        //Escondemos el cursor
        Cursor.lockState = CursorLockMode.Locked;

    }
}
