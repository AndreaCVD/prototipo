using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class btn_events : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private VisualElement _mainPanel;
    private VisualElement _optionsPanel;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;

    void Start()
    {
        root = uIDocument.rootVisualElement;

        _mainPanel = root.Q<VisualElement>("Main_menu");
        _optionsPanel = root.Q<VisualElement>("Options");

        // Cargar valores guardados
        root.Q<Slider>("slider-music").value = PlayerPrefs.GetFloat("MusicVol", 80f);
        root.Q<Slider>("slider-sfx").value = PlayerPrefs.GetFloat("SFXVol", 100f);
        root.Q<Slider>("slider-ambient").value = PlayerPrefs.GetFloat("AmbientVol", 60f);


        root.Q<Button>("start_btn").clicked += () => ChangeSceneUI("Nivel_0");
        root.Q<Button>("start_menu_btn").clicked += () => ChangeSceneUI("Start_MainMenu");
        root.Q<Button>("resume_btn").clicked += () => ChangeSceneUI("resumeGame");
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
        root.Q<Button>("tab-controls").clicked += () => SwitchTab(1);
        root.Q<Button>("tab-credits").clicked += () => SwitchTab(2);

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
        root.Q<VisualElement>("section-audio").style.display = index == 0 ? DisplayStyle.Flex : DisplayStyle.None;
        root.Q<VisualElement>("section-controls").style.display = index == 1 ? DisplayStyle.Flex : DisplayStyle.None;
        root.Q<VisualElement>("section-credits").style.display = index == 2 ? DisplayStyle.Flex : DisplayStyle.None;

        root.Q<Button>("tab-audio").EnableInClassList("opt-tab-active", index == 0);
        root.Q<Button>("tab-controls").EnableInClassList("opt-tab-active", index == 1);
        root.Q<Button>("tab-credits").EnableInClassList("opt-tab-active", index == 2);
    }

    private void OnApply()
    {
        float music = root.Q<Slider>("slider-music").value;
        float sfx = root.Q<Slider>("slider-sfx").value;
        float ambient = root.Q<Slider>("slider-ambient").value;

        AudioManager.instance.SetVolume("MusicVol", music);
        AudioManager.instance.SetVolume("SFXVol", sfx);
        AudioManager.instance.SetVolume("AmbientVol", ambient);

        PlayerPrefs.SetFloat("MusicVol", music);
        PlayerPrefs.SetFloat("SFXVol", sfx);
        PlayerPrefs.SetFloat("AmbientVol", ambient);
        PlayerPrefs.Save();
    }

    public void ChangeSceneUI(string sceneName)
    {
        if (pantalla != null) pantalla.UnTint();
        if (sceneName == "resumeGame")
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync("Pause_Menu");
        }
        else
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}