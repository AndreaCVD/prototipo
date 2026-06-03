using UnityEngine;
using UnityEngine.UIElements;

public class Start_Menu : MonoBehaviour
{
    [Header("Obj escena")]
    public Puzzle lista;

    [Header("Sprites")]
    [SerializeField] Sprite tutorial_spr;
    [SerializeField] Sprite nivel_1_spr;
    [SerializeField] Sprite nivel_2_spr;
    [SerializeField] Sprite nivel_3_spr;
    [SerializeField] Sprite bloqueado_spr_1;
    [SerializeField] Sprite bloqueado_spr_2;
    [SerializeField] Sprite bloqueado_spr_3;


    //[Header("Obj escena")]
    //[SerializeField] GameObject tutorial_obj;
    //[SerializeField] GameObject nivel_1_obj;
    //[SerializeField] GameObject nivel_2_obj;
    //[SerializeField] GameObject nivel_3_obj;
    //[Header("Materiales")]
    //[SerializeField] Material tutorial_m;
    //[SerializeField] Material nivel_1_m;
    //[SerializeField] Material nivel_2_m;
    //[SerializeField] Material nivel_3_m;
    //[SerializeField] Material bloqueado_m;

    void Start ()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        root.Q<VisualElement>("tutorial").style.backgroundImage = new StyleBackground(tutorial_spr);

        root.Q<VisualElement>("nivel_1").style.backgroundImage = new StyleBackground(
            lista.NivelDesbloqueado[0].acabado ? nivel_1_spr : bloqueado_spr_1);

        root.Q<VisualElement>("nivel_2").style.backgroundImage = new StyleBackground(
            lista.NivelDesbloqueado[1].acabado ? nivel_2_spr : bloqueado_spr_2);

        root.Q<VisualElement>("nivel_3").style.backgroundImage = new StyleBackground(
            lista.NivelDesbloqueado[2].acabado ? nivel_3_spr : bloqueado_spr_3);


    //tutorial_obj.GetComponent<Renderer>().material = tutorial_m;
    //if (lista.NivelDesbloqueado[0].acabado)
    //{
    //    nivel_1_obj.GetComponent<Renderer>().material = nivel_1_m;
    //}
    //else
    //{
    //    nivel_1_obj.GetComponent<Renderer>().material = bloqueado_m;
    //}

    //if (lista.NivelDesbloqueado[1].acabado)
    //{
    //    nivel_2_obj.GetComponent<Renderer>().material = nivel_2_m;
    //}
    //else
    //{
    //    nivel_2_obj.GetComponent<Renderer>().material = bloqueado_m;
    //}

    //if (lista.NivelDesbloqueado[2].acabado)
    //{
    //    nivel_3_obj.GetComponent<Renderer>().material = nivel_3_m;
    //}
    //else
    //{
    //    nivel_3_obj.GetComponent<Renderer>().material = bloqueado_m;
    //}
    }
}
