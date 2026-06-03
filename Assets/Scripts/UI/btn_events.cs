using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class btn_events : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private VisualElement _mainPanel;
    private VisualElement _optionsPanel;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    void Start()
    {
        root = uIDocument.rootVisualElement;

        _mainPanel = root.Q<VisualElement>("Main_menu");
        _optionsPanel = root.Q<VisualElement>("Options");

        root.Q<Button>("start_btn").clicked += () => ChangeSceneUI("Nivel_0");
        root.Q<Button>("options_btn").clicked += ShowOptions;
        root.Q<Button>("exit_btn").clicked += () =>
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        };

        root.Q<Button>("btn-back").clicked += HideOptions;
        root.Q<Button>("btn-apply").clicked += OnApply;

        root.Q<Button>("tab-audio").clicked += () => SwitchTab(0);
        root.Q<Button>("tab-graphics").clicked += () => SwitchTab(1);
        root.Q<Button>("tab-controls").clicked += () => SwitchTab(2);

        root.Q<Slider>("slider-music").RegisterValueChangedCallback(e =>
            root.Q<Label>("val-music").text = Mathf.RoundToInt(e.newValue).ToString());
        root.Q<Slider>("slider-sfx").RegisterValueChangedCallback(e =>
            root.Q<Label>("val-sfx").text = Mathf.RoundToInt(e.newValue).ToString());
        root.Q<Slider>("slider-ambient").RegisterValueChangedCallback(e =>
            root.Q<Label>("val-ambient").text = Mathf.RoundToInt(e.newValue).ToString());
    }

    private void ShowOptions()
    {
        _mainPanel.style.display = DisplayStyle.None;
        _optionsPanel.style.display = DisplayStyle.Flex;
        SwitchTab(0);
    }

    private void HideOptions()
    {
        _optionsPanel.style.display = DisplayStyle.None;
        _mainPanel.style.display = DisplayStyle.Flex;
    }

    private void SwitchTab(int index)
    {
        root.Q<VisualElement>("section-audio").EnableInClassList("opt-hidden", index != 0);
        root.Q<VisualElement>("section-graphics").EnableInClassList("opt-hidden", index != 1);
        root.Q<VisualElement>("section-controls").EnableInClassList("opt-hidden", index != 2);

        root.Q<Button>("tab-audio").EnableInClassList("opt-tab-active", index == 0);
        root.Q<Button>("tab-graphics").EnableInClassList("opt-tab-active", index == 1);
        root.Q<Button>("tab-controls").EnableInClassList("opt-tab-active", index == 2);
    }

    private void OnApply()
    {
        Screen.fullScreen = root.Q<Toggle>("toggle-fullscreen").value;
        QualitySettings.vSyncCount = root.Q<Toggle>("toggle-vsync").value ? 1 : 0;
        QualitySettings.SetQualityLevel(root.Q<DropdownField>("dropdown-quality").index);
        PlayerPrefs.SetFloat("MusicVol", root.Q<Slider>("slider-music").value);
        PlayerPrefs.SetFloat("SFXVol", root.Q<Slider>("slider-sfx").value);
        PlayerPrefs.SetFloat("AmbientVol", root.Q<Slider>("slider-ambient").value);
        PlayerPrefs.Save();
    }

    public void ChangeSceneUI(string sceneName)
    {
        if (pantalla != null) pantalla.UnTint();
        SceneManager.LoadScene(sceneName);
    }
}