using System.Collections;
using UnityEngine;
public class NPCAction : MonoBehaviour
{
    //Determinar como va a actuar el NPC en el combate

    [SerializeField] CombatDebug combatDebug;
    [SerializeField] CommandManager commandManager;
    
    [SerializeField] Mimic_Action m;
    [SerializeField] Caballero_Action c;
    [SerializeField] Slime_Action s;
    [SerializeField] Jefe_Action j;
    [SerializeField] Libro_Lerendur l;
    [SerializeField] Viejo_Action y;

    private Parameters enemyData;
    int fuerza, intel, carisma;
    int action_enemy;
    string action;

    void Start()
    {
        //Encontrar el script y las datas
        enemyData= combatDebug.ReturnEnemy();
        //player.stats.Get(PersonajesStats.Fuerza
        //Guardar stats para realizar el analisis como atacar
        fuerza = enemyData.stats.Get(PersonajesStats.Fuerza);
        intel = enemyData.stats.Get(PersonajesStats.Inteligencia);
        carisma = enemyData.stats.Get(PersonajesStats.Carisma);
        //Podemos hacer que dependiendo del enemigo, de sus estats, estas varien
        
        action = enemyData.namePers;
    }
    public void AtaqueOportunidad()
    {
        StartCoroutine(Esperar(3f, "simple"));
    }
    public void AtaqueEnfadado()
    {
        StartCoroutine(Esperar(3f, "enfadado" ));
    }
    public void AtaqueAsustado()
    {
        StartCoroutine(Esperar(3f, "asustado"));
    }
    //var slot = root.Q<VisualElement>($"slot-{index}");
    public void DoAction()
    {
        StartCoroutine(EsperarYContinuar(3f));

        //Podemos hacer que dependiendo del enemigo, de sus estats, estas varien
        //Cualquier opcion tenemos que hablar con commandManager
        //Debug.Log("--ORDEN--");
    }
    char bestFeature()
    {
        //orden.push(fuerza);
        //int aux;
        //Bubblesort para ordenar
        //for (int i = 0; i < orden.Length; i++)
        //{
        //    for (int j = 0; j < orden.Length - 1; j++)
        //    {
        //        if (orden[j] > orden[j + 1])
        //        {
        //            aux = orden[j];
        //            orden[j] = orden[j + 1];
        //            orden[j + 1] = aux;
        //        }
        //    }
        //}
        //[Random.Range(0,5)
        int random = Random.Range(0, 2);
        if (random == 0)
        {
            return 'f';
        }
        else if (random == 1)
        {
            return 'i';
        }
        else
        {
            return 'c';
        }
        //Si Fuerza es lo mejor
        //3 posibilidades de 5 de usar esta
        //if ( fuerza > intel && fuerza > carisma && random%2 == 0)//Si Fuerza es lo mejor
        //{
        //    return 'f';
        //}
        //else if ( intel > carisma && random % 2 == 0)//Si Inteligencia es lo mejor
        //{
        //    return 'i';
        //}
        //else//Si Carisma es lo mejor
        //{
        //    return 'c';
        //}
    }
    //IEnumerator EsperarYContinuar(float segundos)
    //{
    //    yield return new WaitForSeconds(segundos);
    //    //Debug.Log("Han pasado 3 segundos.");
    //    //Debug.Log("TIEMPO ESPERA SE HA AGOTADO");

    //    char aux = bestFeature();

    //    switch (aux)
    //    {
    //        case 'f':
    //            action_enemy = 1;
    //            Invoke(action, 0f);
    //            Fuerza_Enemy();
    //            break;
    //        case 'i':
    //            action_enemy = 2;
    //            Invoke(action, 0f);
    //            Inteligencia_Enemy();
    //            break;
    //        case 'c':
    //            Carisma_Enemy();
    //            break;
    //        case 'h': //Si player huye
    //            commandManager.Fuerza(4, 1);
    //            break;
    //        default:
    //            Debug.Log("--default de npc action--");
    //            break;
    //    }
    //}
    IEnumerator EsperarYContinuar(float segundos)
    {
        yield return new WaitForSeconds(segundos);

        Invoke(action, 0f);
    }

    // -- INVOKE LLAMA A ESTAS FUNCIONES --
    void Mimic()
    {
        m.Ataque_Aleatorio();
    }
    void Caballero()
    {
        // action 1 puede ser basado en fuerza, y en el script de caballero
        // hacer un random para las opciones de fuerza
        // asi cada uno puede tener diferentes numeros de ataques, en un mismo numero de stats (fuerza, carisma, inteligencia)
        c.Ataque_Aleatorio();
    }
    void Slime()
    {
        s.Ataque_Aleatorio();
    }
    void Jefe_Libro()
    {
        j.Ataque_Aleatorio();
    }
    void Jefe_Lerendur()
    {
        l.Ataque_Aleatorio();
    }
    void Yusseif()
    {
        y.Ataque_Aleatorio();
    }
    
    // --- ATAQUES ---
    void Fuerza_Enemy()
    {
        int ca_player = commandManager.Armadura(0, 20);
        Debug.Log("Enemigo usa FUERZA");

        if (ca_player == 2) //supera armadura
        {
            commandManager.Fuerza(12, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Fuerza(12, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");
            commandManager.AutoHerirse(4, 1);
        }
        else // no supera la armadura
        {
            commandManager.NextTurn();
        }
    }
    void Inteligencia_Enemy()
    {
        int ca_player = commandManager.Armadura(0, 20);
        Debug.Log("Enemigo usa INTELIGENCIA");

        if (ca_player == 2) //supera armadura
        {
            commandManager.Inteligencia(12, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Inteligencia(12, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");

            commandManager.AutoHerirse(4, 1);
        }
        else // no supera la armadura
        {
            commandManager.NextTurn();
        }
    }
    void Carisma_Enemy()
    {
        int ca_player = commandManager.Armadura(0, 20);
        Debug.Log("Enemigo usa CARISMA");

        if (ca_player == 2) //supera armadura
        {
            commandManager.Carisma(12, 1);
        }
        else if (ca_player == 0) //CRITICO
        {
            Debug.Log("Tirada critica del enemigo");
            commandManager.Carisma(12, 2);
        }
        else if (ca_player == 1) //TIRA UN 1
        {
            Debug.Log("Tirada fatidica del enemigo");

            commandManager.AutoHerirse(4, 1);
        }
        else // no supera la armadura
        {
            commandManager.NextTurn();
        }
    }
    IEnumerator Esperar(float segundos, string ataque)
    {
        yield return new WaitForSeconds(segundos);
        // C�digo que se ejecuta despu�s del retraso
        // Enviamos d4, tirar 1 vez, acabar el combate cuando acabe de atacar
        switch(ataque)
        {
            case "simple":
                commandManager.SimpleAttack(4, 1, true);
                break;
            case "enfadado":
                commandManager.AtaqueEnfadado(8, 1);
                break;
            case "asustado":
                commandManager.AtaqueAsustado(8, 1);
                break;
            default:
                Debug.Log("No se lee bien");
                break;
        }
        

    }
}
