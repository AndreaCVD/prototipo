using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class btn_events : MonoBehaviour
{

    //UI elements
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;

    private VisualElement start_btn, options_btn;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        root = uIDocument.rootVisualElement;

        start_btn = root.Q<VisualElement>("start_btn");
        options_btn = root.Q<VisualElement>("options_btn");

        //cambiar escena de la main al gameplay
        start_btn.RegisterCallback<ClickEvent>(onClickStartGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void onClickStartGame(ClickEvent evt)
    {
        ChangeSceneUI("pruevas_prototipo");
    }

    public void ChangeSceneUI(string sceneName)
    {
        pantalla.UnTint();
        SceneManager.LoadScene(sceneName);
    }

}