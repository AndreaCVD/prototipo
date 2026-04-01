using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    [SerializeField] TintScreen pantalla;
    [SerializeField] GameObject protagonista;
    [SerializeField] personaje save_posicion;

    //moverse entre escenas
    public void teleport()
    {
        pantalla.UnTint();
        SceneManager.LoadScene("pruevas_prototipo");
        
    }

    public void combate()
    {
        //Antes de irnos tenemos que guardar posicion personaje
        Position();
        pantalla.UnTint();
        SceneManager.LoadScene("combate_pruevas");
    }

    public void ChangeScene(string sceneName)
    {
        pantalla.UnTint();
        SceneManager.LoadScene(sceneName);
    }
    
    private void Position()
    {
        Vector3 pos = protagonista.transform.position;
        //la enviamos
        save_posicion.save_pos(pos);
    }
}
