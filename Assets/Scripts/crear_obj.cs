using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crear_obj : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject preloadScript;
    [SerializeField] GameObject screenTint;
    //[SerializeField] Preload preload;

    void Awake()
    {
        if( !GameObject.Find("--Preload--") && !GameObject.Find("--Preload--(Clone)"))
        {
            GameObject nuevoObj = Instantiate(preloadScript);
            nuevoObj.name = "--Preload--";
            DontDestroyOnLoad(nuevoObj);
        }
        //if ( !GameObject.Find("Player Character") && !GameObject.Find("Player Character(Clone)"))
        //{
        //    //Vector3 position = new Vector3 (0f, 0f, 0f);
        //    //var rotation = Quaternion.Euler(0f, 0f, 0f);
        //    GameObject nuevoObj = Instantiate(player);
        //    nuevoObj.name = "Player Character";
        //    DontDestroyOnLoad(nuevoObj);

        //}
        if (!GameObject.Find("ScreenTint") && !GameObject.Find("ScreenTint(Clone)"))
        {
            GameObject nuevoObj = Instantiate(screenTint);
            nuevoObj.name = "ScreenTint";
            DontDestroyOnLoad(nuevoObj);
        }
    }

    public void destroyAll()
    {
        GameObject preloadObj = GameObject.Find("--Preload--");
        Destroy(preloadObj);

        //GameObject playerObj = GameObject.Find("Player Character");
        //Destroy(playerObj);

        GameObject sceneMObj = GameObject.Find("--SceneManagement--");
        Destroy(sceneMObj);

    }
}
