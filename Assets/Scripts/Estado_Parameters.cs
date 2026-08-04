using System.Collections;
using System.Collections.Generic;
using System.Threading;

using UnityEngine;


public class Estado_Parameters : MonoBehaviour
{
    private Parameters victima;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }


    public void EliminarEstado(Parameters target,int time, string estado)
    {
        Debug.Log("Enamorado");
        victima = target;
        StartCoroutine(WaitAndDo(time, estado));
    }
    IEnumerator WaitAndDo(int time, string estado)
    {
        yield return new WaitForSeconds(time);
        switch (estado)
        {
            case "enamorado":
                victima.enamorado = false;
                Debug.Log("Ya no esta enamorado");
                break;
            default:
                break;
        }
    }
  
}
