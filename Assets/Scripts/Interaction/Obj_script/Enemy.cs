using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] LoadScene scene;
 

    ////Entrar en la zona
    //void OnCollisionEnter(Collision other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        //Si es el player se va al combate
    //        scene.Combat();
    //    }
    //}
}
