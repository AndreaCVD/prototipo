using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crear_obj : MonoBehaviour
{
    //[SerializeField] GameObject world;
    [SerializeField] GameObject player;
    [SerializeField] GameObject preloadScript;
    [SerializeField] GameObject screenTint;
    [SerializeField] GameObject dialog;
    //[SerializeField] Preload preload;

    private GameObject gameManager;

    void Awake()
    {
        //Busco el objeto GameManager en la escena y lo asocio a la variable
        if (gameManager = GameObject.Find("--SceneManagement--"))
        {
            //Le indico que no se destruya entre escenas
            DontDestroyOnLoad(gameManager);
        }
        //if (!GameObject.Find("--WorldManagement--") && !GameObject.Find("--WorldManagement--(Clone)"))
        //{
        //    Debug.Log("a");
        //    GameObject nuevoObj = Instantiate(world);
        //    nuevoObj.name = "--WorldManagement--";
        //    DontDestroyOnLoad(nuevoObj);
        //}
        //else
        //{
        //    GameObject x = GameObject.Find("--WorldManagement--");
        //    x.name = "--WorldManagement--";
        //    DontDestroyOnLoad(x);
        //}

        if (!GameObject.Find("ScreenTint") && !GameObject.Find("ScreenTint(Clone)"))
        {
            Debug.Log("a");
            GameObject nuevoObj = Instantiate(screenTint);
            nuevoObj.name = "ScreenTint";
            DontDestroyOnLoad(nuevoObj);
        }
        else
        {
            GameObject tint = GameObject.Find("ScreenTint");
            tint.name = "ScreenTint";
            DontDestroyOnLoad(tint);
        }
        //if (!GameObject.Find("--DialogManager--") && !GameObject.Find("--DialogManager--(Clone)"))
        //{
        //    GameObject nuevoObj = Instantiate(dialog);
        //    nuevoObj.name = "--DialogManager--";
        //    DontDestroyOnLoad(nuevoObj);
        //}
        //else
        if ( GameObject.Find("--DialogManager--"))
        {
            dialog.name = "--DialogManager--";
            DontDestroyOnLoad(dialog);
        }
            
            
    }

    public void destroyAll()
    {
        //GameObject preloadObj = GameObject.Find("--Preload--");
        //Destroy(preloadObj);

        GameObject sceneMObj = GameObject.Find("--SceneManagement--");
        Destroy(sceneMObj);

        GameObject dialogObj = GameObject.Find("--DialogManager--");
        Destroy(dialogObj);
    }
}
