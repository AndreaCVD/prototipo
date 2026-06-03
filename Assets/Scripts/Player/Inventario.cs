using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Inventario : MonoBehaviour
{
    LoadScene load;
    [SerializeField] Parameters prota;
    [SerializeField] Puzzle lista;
    //Para encontrar los scripts de DialogManager
    private Dialog dialog;
    private GameObject script_dialog;
    //----------
    [SerializeField] List<int> Llaves = new List<int>();
    private GameObject posible_llave;
    //public List<int[]> x = new List<int[]>();



    private void Start()
    {
        // Inicializa listas si son null (por cambio de escena)
        if (prota.Inventario.Llave == null) prota.Inventario.Llave = new List<string>();
        if (prota.Inventario.LlaveMaestra == null) prota.Inventario.LlaveMaestra = new List<string>();
        if (prota.Inventario.PocionVida == null) prota.Inventario.PocionVida = new List<string>();
        if (prota.Inventario.Daga == null) prota.Inventario.Daga = new List<string>();
        if (prota.Inventario.Espada == null) prota.Inventario.Espada = new List<string>();

        //prota.Inventario.Clear();
        if (script_dialog == null)
        {
            script_dialog = GameObject.Find("--DialogManager--");
            dialog = script_dialog.GetComponent<Dialog>();
        }
        if (load == null)
        {
            GameObject aux = GameObject.Find("--SceneManagement--");
            load = aux.GetComponent<LoadScene>();
        }
    }
    public void InsertKey(GameObject x)
    {
        posible_llave = x;
        Debug.Log("Cojera esa llave???");
    }
    void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.tag == "Llave")
        {
            prota.Inventario.Llave.Add(other.gameObject.name);
            //Marcar que se ha acabado el puzzle
            puzzleAcabado(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "LlaveMaestra")
        {
            prota.Inventario.LlaveMaestra.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "PocionVida")
        {
            prota.Inventario.PocionVida.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Daga")
        {
            prota.Inventario.Daga.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Espada")
        {
            prota.Inventario.Espada.Add(other.gameObject.name);
            Destroy(other.gameObject);
        }

    }
    public void restarVida()
    {

         Debug.Log("lava");
        if (prota.stats.values[3].value > 0)
        {
            prota.stats.values[3].value--;
        }
        if (prota.stats.values[3].value <= 0)
        {
            load.GameOver();
        }

    }
    public void CofreKey(GameObject cofre, cherrydev.DialogNodeGraph dialogo_obj)
    {
        bool keyFound = false;

        //hay cofres que se abren sin llave, los loot
        if (cofre.name.Contains("noKey"))
        {
            //Si llave, puede ser aleatorio o no
            AbrirCofre(cofre);
        }
        else if (!cofre.name.Contains("loot"))
        {
            //No aleatorio con llave
            //Recorremos nuestro inventario para ver si tenemos llaves
            if (prota.Inventario.Llave.Count > 0)
            {
                prota.Inventario.Llave.RemoveAt(prota.Inventario.Llave.Count - 1);
                AbrirCofre(cofre);
                keyFound = true;
            }
            if (!keyFound)
            {
                dialog.EmpezarDialogo(dialogo_obj, cofre);
            }
        }
        else
        {
            //Si contiene loot es aleatorio con llave
            AbrirCofre(cofre);
        }
    }
    public void PuertaMaestraKey(GameObject puerta, cherrydev.DialogNodeGraph dialogo_obj)
    {
        bool keyFound = false;

        //Recorremos nuestro inventario para ver si tenemos llaves
        if ( prota.Inventario.LlaveMaestra.Count > 0 )
        {
            prota.Inventario.LlaveMaestra.RemoveAt(prota.Inventario.LlaveMaestra.Count - 1);
            AbrirPuertaMaestra(puerta.name);
            keyFound = true;
        }
        if (!keyFound)
        {
            dialog.EmpezarDialogo(dialogo_obj, puerta);
        }
    }
    public void PuertaKey(GameObject puerta, cherrydev.DialogNodeGraph dialogo_obj)
    {
        bool keyFound = false;

        //Recorremos nuestro inventario para ver si tenemos llaves
        if ( prota.Inventario.Llave.Count > 0 )
        {
            prota.Inventario.Llave.RemoveAt(prota.Inventario.Llave.Count - 1);
            Destroy(puerta);
            //AbrirPuerta(puerta.name);
            keyFound = true;
        }
        if (!keyFound)
        {
            dialog.EmpezarDialogo(dialogo_obj, puerta);
        }
    }
    private void AbrirCofre(GameObject cofre)
    {
        Debug.Log("El cofre se abre");
        //Activar animacion
        //Activar UI del loot que ha salido --> brillo bolsa UI

        //Cofre B1 --> Llave Maestra
        //Cofre C2 --> Loot aleatorio
        //Cofre C5 --> Pocion Nivel_3
        //Cofre C6 --> Llave Maestra
        //Cofre D5 --> Pocion Vida y Espada de Lava
        switch (cofre.name)
        {
            case string b when b.Contains("b1"):
                prota.Inventario.LlaveMaestra.Add("llave_cofre");
                break;
            case string b when b.Contains("c2"):
                prota.Inventario.Espada.Add("espada_cofre");
                break;
            case string b when b.Contains("c5"):
                prota.Inventario.PocionLava.Add("pocion_cofre");
                break;
            case string b when b.Contains("c6"):
                prota.Inventario.LlaveMaestra.Add("llave_cofre");
                break;
            case string b when b.Contains("d3"):
                prota.Inventario.LlaveMaestra.Add("llave_cofre");
                break;
            case string b when b.Contains("d5"):
                prota.Inventario.Espada.Add("espada_cofre");
                prota.Inventario.PocionVida.Add("pocion_cofre");
                break;
            default:
                lootAleatorio();
                break;

        }
        //Abrir cofre por animacion
        Destroy(cofre);
        Debug.Log("Cofre destruido");
    }
    public void lootAleatorio()
    {
        int a = Random.Range(1, 3);
        switch (a)
        {
            case 1:
                prota.Inventario.Espada.Add("Espada_Loot");
                break;
            case 2:
                prota.Inventario.Daga.Add("Daga_Loot");
                break;
            case 3:
                prota.Inventario.PocionVida.Add("PocionVida_Loot");
                break;
            default:
                break;
        }
    }
    private void AbrirPuertaMaestra(string a)
    {
        Debug.Log("La puerta se abre");
        //Activar animacion

        switch (a)
        {
            case string b when b.Contains("A"):
                //Puerta Maestra del Nivel 0 se ha abierto
                lista.NivelDesbloqueado[0].acabado = true;
                break;
            case string b when b.Contains("B"):
                //Puerta Maestra del Nivel 1 se ha abierto
                lista.NivelDesbloqueado[1].acabado = true;
                break;
            case string b when b.Contains("C"):
                //Puerta Maestra del Nivel 2 se ha abierto
                lista.NivelDesbloqueado[2].acabado = true;
                break;
            case string b when b.Contains("E"):
                lista.NivelDesbloqueado[3].acabado = true;
                break;
            default:
                Debug.Log("No se ha leido bien la Puerta Maestra");
                break;
        }
    }
    private void puzzleAcabado(string obj)
    {
        string a1 = "a1";
        string b1 = "b1"; //Cofre
        string b2 = "b2"; //Cofre
        Scene escenaActual = SceneManager.GetActiveScene(); 
        switch( escenaActual.name )
        {
            case "Nivel_0":
                if (obj.Contains(a1))
                {
                    Debug.Log("es del escenario A");
                }
                    break;
            case "Nivel_1":
                if (obj.Contains(b1))
                {
                    lista.Nivel_1[0].acabado = true;
                }               
                else if (obj.Contains(b2))
                {
                    lista.Nivel_1[1].acabado = true;
                }
                break;
            case "Nivel_2":
                break;
            case "Nivel_3":
                break;
            default:
                Debug.Log("Error en inventario");
                break;
        }
    }

    public bool pocionLava()
    {
        if (prota.Inventario.PocionLava.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
   
    }
}
