using UnityEngine;

public class Start_Menu : MonoBehaviour
{
    [Header("Obj escena")]
    public Puzzle lista;
    [Header("Obj escena")]
    [SerializeField] GameObject tutorial_obj;
    [SerializeField] GameObject nivel_1_obj;
    [SerializeField] GameObject nivel_2_obj;
    [SerializeField] GameObject nivel_3_obj;
    [Header("Materiales")]
    [SerializeField] Material tutorial_m;
    [SerializeField] Material nivel_1_m;
    [SerializeField] Material nivel_2_m;
    [SerializeField] Material nivel_3_m;
    [SerializeField] Material bloqueado_m;

    void Start ()
    {
        tutorial_obj.GetComponent<Renderer>().material = tutorial_m;
        if (lista.NivelDesbloqueado[0].acabado)
        {
            nivel_1_obj.GetComponent<Renderer>().material = nivel_1_m;
        }
        else
        {
            nivel_1_obj.GetComponent<Renderer>().material = bloqueado_m;
        }

        if (lista.NivelDesbloqueado[1].acabado)
        {
            nivel_2_obj.GetComponent<Renderer>().material = nivel_2_m;
        }
        else
        {
            nivel_2_obj.GetComponent<Renderer>().material = bloqueado_m;
        }

        if (lista.NivelDesbloqueado[2].acabado)
        {
            nivel_3_obj.GetComponent<Renderer>().material = nivel_3_m;
        }
        else
        {
            nivel_3_obj.GetComponent<Renderer>().material = bloqueado_m;
        }
    }
}
