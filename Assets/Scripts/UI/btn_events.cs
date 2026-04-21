using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using System.Collections.Generic;

public class btn_events : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    void Start()
    {
        root = uIDocument.rootVisualElement;
        List<VisualElement> allButtons = root.Query<VisualElement>(className: "Btn").ToList();

        foreach (VisualElement btn in allButtons)
        {
            VisualElement sign = btn.Q(className: "sign");
            VisualElement text = btn.Q(className: "text");

            // Registramos las funciones que tienes abajo
            btn.RegisterCallback<MouseEnterEvent>(evt => OnEnter(btn, sign, text));
            btn.RegisterCallback<MouseLeaveEvent>(evt => OnLeave(btn, sign, text));

            btn.RegisterCallback<ClickEvent>(evt => {
                if (btn.name == "start_btn")
                {
                    ChangeSceneUI("pruevas_prototipo");

                }
                else if(btn.name == "options_btn")
                {
                    ChangeSceneUI("pruevas_prototipo");
                }
                else if (btn.name == "exit_btn")
                {
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                }
            });
        }
    }

    private void OnEnter(VisualElement btn, VisualElement sign, VisualElement text)
    {
        btn.AddToClassList("Btn--collapsed");
        sign?.AddToClassList("sign--visible");
        text?.AddToClassList("text--hidden");
    }

    private void OnLeave(VisualElement btn, VisualElement sign, VisualElement text)
    {
        btn.RemoveFromClassList("Btn--collapsed");
        sign?.RemoveFromClassList("sign--visible");
        text?.RemoveFromClassList("text--hidden");
    }

    public void ChangeSceneUI(string sceneName)
    {
        if (pantalla != null) pantalla.UnTint();
        SceneManager.LoadScene(sceneName);
    }
}