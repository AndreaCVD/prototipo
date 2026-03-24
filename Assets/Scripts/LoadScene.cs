using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField] TintScreen pantalla;
    //moverse entre escenas
    public void teleport()
    {
        pantalla.Tint();
        SceneManager.LoadScene("pruevas_prototipo");
        
    }

    /*
    public void base_teleport()
    {
        SceneManager.LoadScene("name");
    }*/
}
