using System.Collections;
using UnityEngine;
public class NPCAction : MonoBehaviour
{
    //Determinar como va a actuar el NPC en el combate

    [SerializeField] CombatDebug combatDebug;
    [SerializeField] CommandManager commandManager;
    private Parameters enemyData;
    int fuerza, intel, carisma;
    private int[] orden;
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

    }

    public void DoAction()
    {
        StartCoroutine(EsperarYContinuar(3f));

        
        //Podemos hacer que dependiendo del enemigo, de sus estats, estas varien
        //Cualquier opcion tenemos que hablar con commandManager
        Debug.Log("--ORDEN--");
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
    IEnumerator EsperarYContinuar(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        // Código que se ejecuta después del retraso
        Debug.Log("Han pasado 3 segundos.");
        Debug.Log("TIEMPO ESPERA SE HA AGOTADO");
        char aux = bestFeature();
        switch (aux)
        {
            case 'f':
                //Random pero que tenga mas probabilidades de Fuerza
                Debug.Log("Enemigo ha usado FUERZA");
                commandManager.Fuerza();
                break;
            case 'i':
                //Random pero que tenga mas probabilidades de Fuerza
                Debug.Log("Enemigo ha usado INTELIGENCIA");

                commandManager.Inteligencia();
                break;
            case 'c':
                //Random pero que tenga mas probabilidades de Fuerza
                Debug.Log("Enemigo ha usado CARISMA");

                commandManager.Carisma();
                break;
            default:
                Debug.Log("--default de npc action--");
                break;
        }
    }
}
