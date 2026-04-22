using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CombatMonster : MonoBehaviour
{

    Parameters player; //Al que le toque recibir daño
    Save_Stats guardado; //Enviar las estats
    [SerializeField] Image imagenPers; 
    public Int2Val HP;
    public int damage;
    GameObject objLoadScene;
    LoadScene load;
    private void Start()
    {
        if (objLoadScene == null)
        {
            objLoadScene = GameObject.Find("--SceneManagement--");
            load = objLoadScene.GetComponent<LoadScene>();
            //save_posicion = GetComponent<personaje>();
        }
    }
    public void Init(Parameters player) //Al que le toque atacar
    {
        //inicializamos el jugador
        this.player = player;
            //colocamos copia del modelo
            //GameObject modelo = Instantiate(player.modelPrefab, transform);
        //colocamos imagen
        imagenPers.sprite = player.art;
            //restablecer rotacion
            //player.modelPrefab.transform.localPosition = Vector3.zero;
            //player.modelPrefab.transform.localRotation = Quaternion.identity;
        //Setear vida
        int contitucion = player.stats.Get(PersonajesStats.Constitucion);
        HP = new Int2Val (contitucion, contitucion);
    }

    public void Fuerza(CombatMonster target, int dice) //Enemigo
    {
        //Este daño al enemigo
        int stat_enemigo = player.stats.Get(PersonajesStats.Fuerza);
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);
        
        //Si Dice + Fuerza no supera AC del enemigo, no se hace el ataque
        if ( (stat_enemigo+ dice) >= armadura)
        {
            //Escibimos Debug.Log
            Commando(stat_enemigo, armadura, dice);
            //Target recibe daño de la fuerza
            target.TakeDamage(stat_enemigo + dice); 
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
        }
    }
    public void Inteligencia(CombatMonster target, int dice) //Enemigo
    {
        //Este daño al enemigo
        int stat_enemigo = player.stats.Get(PersonajesStats.Inteligencia);
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);

        //Si Dice + Inteligencia no supera AC del enemigo, no se hace el ataque
        if ((stat_enemigo + dice) >= armadura)
        {
            //Escibimos Debug.Log
            Commando(stat_enemigo, armadura, dice);
            //Target recibe daño de la fuerza
            target.TakeDamage(stat_enemigo + dice);
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
        }
    }
    public void Carisma(CombatMonster target, int dice) //Enemigo
    {
        //Este daño al enemigo
        int stat_enemigo = player.stats.Get(PersonajesStats.Carisma);
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);

        //Si Dice + Carisma no supera AC del enemigo, no se hace el ataque
        if ((stat_enemigo + dice) >= armadura)
        {
            //Escibimos Debug.Log
            Commando(stat_enemigo, armadura, dice);
            //Target recibe daño de la fuerza
            target.TakeDamage(stat_enemigo + dice);
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
        }
    }
    private void Commando(int stat_enemigo, int armadura, int dice)
    {
        Debug.Log("AC enemigo = " + armadura);
        Debug.Log("Stat = " + stat_enemigo + "| Dice = " + dice + "| Ataque total  = " + (stat_enemigo + dice));
    }
    public int FuerzaCurrent()
    {
        return player.stats.Get(PersonajesStats.Fuerza);
    }
    public void TakeDamage(int damage)
    {

        HP.current -= damage;
        // a -= damage;
        //enemigo.stats.values[3].value++;
        player.stats.values[3].value -= damage;

        //guardado.guardar_stats(player, damage); //guardar estats
        //Debug.Log(player.stats.values[3].value); 

        Debug.Log(player.namePers + " ha sido atacado! : "+ "// HP RESTANTE : " + HP.current.ToString());

        if (HP.current <= 0)
        {
            HP.current = 0;
            Debug.Log("Derrotado : " + player.namePers);
            //Si es el prota es GAMEOVER
            if (player.namePers == "Prota")
            {
                //load.GameOver();
                Debug.Log("GAME OVER");
                load.SalirCombate();
            }
            else //Si pierde el enemigo:
            {

                //Guardar en preload la nueva constitucion
                Debug.Log("HAS GANADO AL ENEMIGO");
                //hay que destruir el obj del enemigo

                //Hablar con SceneManager -> LoadScene volver a la pantalla anterior
                load.SalirCombate();
                
            }

            //guardado.alguien_eliminado(player); //enviara el personaje que se elimine
        }
        else
        {
            Debug.Log("FIN TURNO");
        }
    }
}
