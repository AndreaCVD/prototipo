using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crear_obj : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject preloadScript;
    [SerializeField] GameObject screenTint;
    [SerializeField] GameObject dialog;
    [SerializeField] GameObject saveInfo;
    [SerializeField] GameObject UIdocument;
    //[SerializeField] Preload preload;

    private GameObject gameManager;

    void Awake()
    {
        //Busco el objeto GameManager en la escena y lo asocio a la variable
        gameManager = GameObject.Find("--SceneManagement--");
        //Le indico que no se destruya entre escenas
        DontDestroyOnLoad(gameManager);

        if (!GameObject.Find("-- Save Info --") && !GameObject.Find("-- Save Info --(Clone)"))
        {
            GameObject nuevoObj = Instantiate(saveInfo);
            nuevoObj.name = "-- Save Info --";
            DontDestroyOnLoad(nuevoObj);
        }
        else
        {
            GameObject x = GameObject.Find("-- Save Info --");
            x.name = "-- Save Info --";
            DontDestroyOnLoad(x);
        }
        //if (!GameObject.Find("UI") && !GameObject.Find("UI(Clone)"))
        //{
        //    GameObject nuevoObj = Instantiate(UIdocument);
        //    nuevoObj.name = "UI";
        //    DontDestroyOnLoad(nuevoObj);
        //}
        //else
        //{
        //    GameObject x = GameObject.Find("UI");
        //    x.name = "UI";
        //    DontDestroyOnLoad(x);
        //}
        if (!GameObject.Find("ScreenTint") && !GameObject.Find("ScreenTint(Clone)"))
        {
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
        if (!GameObject.Find("--DialogManager--") && !GameObject.Find("--DialogManager--(Clone)"))
        {
            GameObject nuevoObj = Instantiate(dialog);
            nuevoObj.name = "--DialogManager--";
            DontDestroyOnLoad(nuevoObj);
        }
        else
        {
            GameObject dialog = GameObject.Find("--DialogManager--");
            dialog.name = "--DialogManager--";
            DontDestroyOnLoad(dialog);
        }
    }

    public void destroyAll()
    {
        //GameObject preloadObj = GameObject.Find("--Preload--");
        //Destroy(preloadObj);

        //GameObject playerObj = GameObject.Find("Player Character");
        //Destroy(playerObj);

        GameObject sceneMObj = GameObject.Find("--SceneManagement--");
        Destroy(sceneMObj);

        GameObject dialogObj = GameObject.Find("--DialogManager--");
        Destroy(dialogObj);
    }
}
