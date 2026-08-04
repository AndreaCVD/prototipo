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
    private Estado_Parameters estado_Parameters;

    //Si cambiamos las stats
    private bool stat_change; //si se cambia un stat volver al anterior despues de turno
    //private int fuerzaChanged;
    //private int intelChanged;
    //private int carismaChanged;
    private int caChanged;

    private void Start()
    {
        //GameObject a = GameObject.Find("--WorldManagement--");
        //guardado = a.GetComponent<Save_Stats>();

        stat_change = false;
        //fuerzaChanged = 0;
        //intelChanged = 0;
        //carismaChanged = 0;
        caChanged = 0;

        if (objLoadScene == null)
        {
            objLoadScene = GameObject.Find("--SceneManagement--");
            load = objLoadScene.GetComponent<LoadScene>();
            preload = objLoadScene.GetComponent<Preload>();
            estado_Parameters = objLoadScene.GetComponent<Estado_Parameters>();
        }
        cambiarVida(0);
    }
    public void Init(Parameters player) //Al que le toque atacar
    {
        //inicializamos el jugador
        this.player = player;
            //GameObject modelo = Instantiate(player.modelPrefab, transform);

        imagenPers.sprite = player.art;
            //restablecer rotacion
            //player.modelPrefab.transform.localPosition = Vector3.zero;
            //player.modelPrefab.transform.localRotation = Quaternion.identity;
        //Setear vida
        int contitucion = player.stats.Get(PersonajesStats.Constitucion);
        HP = new Int2Val (contitucion, contitucion);
    }

    //Primero tiene que superar la armadura
    public int Armadura (CombatMonster target, int dice, int stat)
    {
        //Este da�o al enemigo
        int stat_damage = player.stats.values[stat].value;
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);
        //Si Dice + Fuerza no supera AC del enemigo, no se hace el ataque
        if (dice == 20) //Natrual 20, doble da�o
        {
            Debug.Log("NATURAL 20, el da�o sera doble");
            return 0;
        }
        else if (dice == 1) //da�o propio --> d4 a ti mismo
        {
            Debug.Log("NATURAL 1, da�o a uno mismo con d4");
            return 1;
        }
        else if ((stat_damage + dice) >= armadura)
        {
            Debug.Log("Has llegado al AC del enemigo");
            return 2;
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
            return 3;
        }
    }
    public bool InLove()
    {
        if (player.enamorado)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Enamorado()
    {
        player.enamorado = true;
        estado_Parameters.EliminarEstado(player, 30, "enamorado");
    }
    public void ataque_propio(CombatMonster player, int dice)
    {
        Debug.Log("El prota se ha hecho " + dice + " a si mismo");
        player.TakeDamage(dice);

    }
    public void modificar_CA(int valor)
    {
        stat_change = true;
        //caChanged = valor;
        player.stats.values[4].value += valor;

    }
    public void SimpleAttack(CombatMonster target, int dice, bool acabarCombate)
    {
        //Ataque solo de un dado
        int stat_damage = player.stats.Get(PersonajesStats.Fuerza);

        Debug.Log(stat_damage);
        target.TakeDamage(dice+ stat_damage, acabarCombate); 
    }
    //Luego volver a tirar dado
    public void Fuerza(CombatMonster target, int dice) //Enemigo
    {
        //Este da�o al enemigo
        int stat_damage = player.stats.Get(PersonajesStats.Fuerza);
        //Clase Armadura oponente
        //int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);

        //Si Dice + Fuerza no supera AC del enemigo, no se hace el ataque
        //if ( (stat_damage+ dice) >= armadura)
        //{
        if (stat_change)
        {
            restaurarStat(0); //Restauramos Fuerza
        }
        //Escibimos Debug.Log
        //Commando(stat_enemigo, armadura, dice);
        //Target recibe da�o de la fuerza

        //20 nat --> doble da�o
        //1 --> d4 a ti mismo
        Debug.Log("Da�o = " + stat_damage+" || Dice = "+dice);
        target.TakeDamage(stat_damage + dice); 
        //}
        //else
        //{
            //Debug.Log("No has llegado al AC del enemigo");
        //}
    }
    public void Inteligencia(CombatMonster target, int dice) //Enemigo
    {
        //Este da�o al enemigo
        int stat_enemigo = player.stats.Get(PersonajesStats.Inteligencia);
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);

        //Si Dice + Inteligencia no supera AC del enemigo, no se hace el ataque
        if ((stat_enemigo + dice) >= armadura)
        {
            //Escibimos Debug.Log
            Commando(stat_enemigo, armadura, dice);
            //Target recibe da�o de la fuerza
            target.TakeDamage(stat_enemigo + dice);
        }
        else
        {
            Debug.Log("No has llegado al AC del enemigo");
        }
    }
    public void Carisma(CombatMonster target, int dice) //Enemigo
    {
        //Este da�o al enemigo
        int stat_enemigo = player.stats.Get(PersonajesStats.Carisma);
        //Clase Armadura oponente
        int armadura = target.player.stats.Get(PersonajesStats.ClaseArmadura);

        //Si Dice + Carisma no supera AC del enemigo, no se hace el ataque
        if ((stat_enemigo + dice) >= armadura)
        {
            //Escibimos Debug.Log
            Commando(stat_enemigo, armadura, dice);
            //Target recibe da�o de la fuerza
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
                restaurarStat(10); //Restaurar todos los stats prota si han sido cambiados
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

                restaurarStat(10); //Restaurar todos los stats prota si han sido cambiados
                //destruir el obj del enemigo
                preload.DestroyEnemy();

                load.SalirCombate();
            }
            //guardado.alguien_eliminado(player); //enviara el personaje que se elimine
        }
        else
        {
            
            //SalirCombate();
            Debug.Log("FIN TURNO");
        }
    }
    public void TakeDamage(int damage, bool acabarCombate)
    { 
        HP.current -= damage;

        player.stats.values[3].value -= damage;

        Debug.Log(player.namePers + " ha sido atacado! : "+ "// HP RESTANTE : " + HP.current.ToString());

        if (HP.current <= 0)
        {
            HP.current = 0;
            player.stats.values[3].value = HP.max;

            //Si es el prota es GAMEOVER
            if (player.namePers == "Prota")
            {
                restaurarStat(10); //Restaurar todos los stats prota si han sido cambiados
                Debug.Log("Prota ha perdido");

                Debug.Log("GAME OVER");
                load.GameOver();
            }
            else //Si pierde el enemigo:
            {
                //Restaurar constitucino ficha enemigo
                player.stats.values[3].value = 0;

                restaurarStat(10); //Restaurar todos los stats prota si han sido cambiados
 
                preload.DestroyEnemy();

                SalirCombate();
            }
        }
        else
        {
            if (acabarCombate)
                SalirCombate();
        }
    }
    public void SalirCombate()
    {
        load.SalirCombate();

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
    void restaurarStat(int stat)
    {
        stat_change = false;
        switch (stat)
        {
            //case 0:
            //    player.stats.values[stat].value -= fuerzaChanged; //Le cambiamos la fuerza
            //    break;
            //case 1:
            //    player.stats.values[stat].value -= intelChanged; //Le cambiamos la fuerza
            //    break;
            //case 2:
            //    player.stats.values[stat].value -= carismaChanged; //Le cambiamos la fuerza
            //    break;
            case 10: //CAMBIAR TODOS LOS STATS
                //player.stats.values[0].value -= fuerzaChanged;
                //player.stats.values[1].value -= intelChanged;
                //player.stats.values[2].value -= carismaChanged;
                player.stats.values[2].value -= caChanged;
                break;
            default:
                Debug.Log("No se ha restaurado bien el stat");
                break;
        }
    }

}
