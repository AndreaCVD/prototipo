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


    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<VisualElement>("tutorial").style.backgroundImage = new StyleBackground(tutorial_spr);

        if (lista == null) { Debug.LogError("lista es null en Start_Menu"); return; }
        if (lista.Nivel_0.Count == 0) { Debug.LogError("Nivel_0 está vacío"); return; }
        if (lista.NivelDesbloqueado.Count < 3) { Debug.LogError("NivelDesbloqueado tiene menos de 3 elementos"); return; }

        root.Q<VisualElement>("nivel_1").style.backgroundImage = new StyleBackground(
            lista.Nivel_0[0].acabado ? nivel_1_spr : bloqueado_spr_1);
        root.Q<VisualElement>("nivel_2").style.backgroundImage = new StyleBackground(
            lista.NivelDesbloqueado[1].acabado ? nivel_2_spr : bloqueado_spr_2);
        root.Q<VisualElement>("nivel_3").style.backgroundImage = new StyleBackground(
            lista.NivelDesbloqueado[2].acabado ? nivel_3_spr : bloqueado_spr_3);
    }
}
