using UnityEngine;
using UnityEngine.SceneManagement;

public class personaje : MonoBehaviour
{
    //[Header("Posicion")]
    public Vector3 posicion;
    [SerializeField] GameObject protagonista;
    //void Awake()
    public void save_pos(Vector3 nueva_pos)
    {
        posicion = nueva_pos;
    }
    void Start()
    {
        //si estamos en combate esto no aplica
        Scene escenaActual = SceneManager.GetActiveScene();

        if (escenaActual.name == "prueva_prototipo")
        {
            float x = posicion.x;
            float y = posicion.y;
            float z = posicion.z;
            protagonista.transform.position = new Vector3(x, y, z);
        }

    }


}
