using System.Reflection;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class CombatMonster : MonoBehaviour
{
    //Todos los comentarios son:
        // 1 - Explicaciones
        // 2 - Mecanica cambiada/obsoleta no quitada por falta de tiempo
    Parameters player; //El que tiene el script (todos)
    Save_Stats guardado; //Enviar las estats
    [SerializeField] Image imagenPers; 
    public Int2Val HP;
    public int damage;
    GameObject objLoadScene;
    private LoadScene load;
    private Preload preload;

    //Si cambiamos las stats de Fuerza
    //private bool stat_change; //si se cambia un stat volver al anterior despues de turno
    //private int fuerzaChanged;
    //private int intelChanged;
    //private int carismaChanged;

    private void Start()
    {
        //GameObject a = GameObject.Find("--WorldManagement--");
        //guardado = a.GetComponent<Save_Stats>();

        //stat_change = false;
        //fuerzaChanged = 0;
        //intelChanged = 0;
        //carismaChanged = 0;

        if (objLoadScene == null)
        {
            objLoadScene = GameObject.Find("--SceneManagement--");
            load = objLoadScene.GetComponent<LoadScene>();
            preload = objLoadScene.GetComponent<Preload>();
        }
        cambiarVida(0);
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
        if ( (stat_enemigo+ 20) >= armadura)
        {
            //if (stat_change)
            //{
            //    restaurarStat(0); //Restauramos Fuerza
            //}
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
        //Debug.Log("AC enemigo = " + armadura);
        //Debug.Log("Stat = " + stat_enemigo + "| Dice = " + dice + "| Ataque total  = " + (stat_enemigo + dice));
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
            player.stats.values[3].value = HP.max;

            //Si es el prota es GAMEOVER
            if (player.namePers == "Prota")
            {
                Debug.Log("Prota ha perdido");
                //load.GameOver();
                Debug.Log("GAME OVER");
                load.GameOver();
            }
            else //Si pierde el enemigo:
            {
                //Player == enemigo
                //Restaurar constitucino ficha enemigo
                player.stats.values[3].value = 0;

                //restaurarStat(10); //Restaurar todos los stats prota si han sido cambiados
                //destruir el obj del enemigo
                preload.DestroyEnemy();
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

    public void cambiarFuerza(int damage)
    {
        //Subir Fuerza
        //stat_change = true;
        //fuerzaChanged = damage;
        //player.stats.values[0].value += damage; //Le cambiamos la fuerza
    }
    public void cambiarVida(int vida)
    {
        //player.stats.values[3].value += vida; //Le sumamos la vida
    }
    //void restaurarStat(int stat)
    //{
    //    stat_change = false;
    //    switch(stat)
    //    { 
    //        case 0:
    //            player.stats.values[stat].value -= fuerzaChanged; //Le cambiamos la fuerza
    //            break;
    //        case 1:
    //            player.stats.values[stat].value -= intelChanged; //Le cambiamos la fuerza
    //            break;
    //        case 2:
    //            player.stats.values[stat].value -= carismaChanged; //Le cambiamos la fuerza
    //            break;
    //        case 10: //CAMBIAR TODOS LOS STATS
    //            player.stats.values[0].value -= fuerzaChanged;
    //            player.stats.values[1].value -= intelChanged;
    //            player.stats.values[2].value -= carismaChanged; 
    //            break;
    //        default:
    //            Debug.Log("No se ha restaurado bien el stat");
    //            break;
    //    }
    //}

}
