using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;

public class Menu_Stat : MonoBehaviour
{
    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private VisualElement _mainPanel;
    private VisualElement _optionsPanel;

    [SerializeField] LoadScene load;
    public Puzzle lista;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Protagonista")]
    [SerializeField] Parameters prota;
    public string nivel;

    void Start()
    {
        root = uIDocument.rootVisualElement;

        _mainPanel = root.Q<VisualElement>("Main_menu");
        _optionsPanel = root.Q<VisualElement>("Options");


        root.Q<Button>("fuerza_btn").clicked += Fuerza;
        root.Q<Button>("intel_btn").clicked +=  Inteligencia;
        root.Q<Button>("carisma_btn").clicked +=  Carisma;
        root.Q<Button>("vida_btn").clicked += Vida;

    }

    private void ShowOptions()
    {
        //_mainPanel.style.display = DisplayStyle.None;
        //_optionsPanel.style.display = DisplayStyle.Flex;
        //SwitchTab(0);
    }

    private void HideOptions()
    {
        _optionsPanel.style.display = DisplayStyle.None;
        _mainPanel.style.display = DisplayStyle.Flex;
    }

    private void Fuerza()
    {
        //subir +2 a fuerza
        prota.stats.values[0].value += 2;
        //cambiar escena
        ChangeScene();
    }
    private void Inteligencia()
    {
        //subir +2 a inteligencia
        prota.stats.values[1].value += 2;
        //cambiar escena
        ChangeScene();
    }
    private void Carisma()
    {
        //subir +2 a carisma
        prota.stats.values[2].value += 2;
        //cambiar escena
        ChangeScene();
    }
    private void Vida()
    {
        //subir +5 a vida y vida max
        prota.stats.values[3].value += 5;
        prota.stats.values[5].value += 5;
        //cambiar escena
        ChangeScene();
    }

    public void ChangeScene()
    {
        if (pantalla != null) pantalla.UnTint();

        if (lista.Subir_Nivel[0].acabado && !lista.Subir_Nivel[1].acabado)
            load.ChangeScene("Nivel_1");
        else if (lista.Subir_Nivel[1].acabado && !lista.Subir_Nivel[2].acabado)
            load.ChangeScene("Nivel_2");
        else if (lista.Subir_Nivel[1].acabado && lista.Subir_Nivel[2].acabado)
            load.ChangeScene("Nivel_3");


    }
}