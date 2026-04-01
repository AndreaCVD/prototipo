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

    public void combate()
    {
        pantalla.Tint();
        SceneManager.LoadScene("combate_pruevas");
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

    }
    /*
    public void base_teleport()
    {
        SceneManager.LoadScene("name");
    }*/
}
