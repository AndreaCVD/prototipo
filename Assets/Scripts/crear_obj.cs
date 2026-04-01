using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crear_obj : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject preloadScript;
    [SerializeField] Preload preload;
    
    void Awake()
    {
        if( !GameObject.Find("--Preload--") && !GameObject.Find("--Preload--(Clone)"))
        {
            Instantiate(preloadScript);
        }
        if ( !GameObject.Find("Player Character"))
        {
            Vector3 position = new Vector3 (0f, 0f, 0f);
            var rotation = Quaternion.Euler(0f, 0f, 0f);
            Instantiate(player, position, rotation);
        }
        
    }
}
