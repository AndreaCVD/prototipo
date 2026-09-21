using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Audio;
using Cursor = UnityEngine.Cursor;

public class Tienda : MonoBehaviour
{
    [Header("Precios")]
    [SerializeField] int cost_vida;
    [SerializeField] int cost_lava;
    [SerializeField] int cost_cuero;
    [SerializeField] int cost_malla;

    private InputHandler escenaState;

    [SerializeField] UIDocument uIDocument;
    private VisualElement root;
    private VisualElement _mainPanel;
    private VisualElement _optionsPanel;
    private Label dinero_actual, coste_compra;

    private Button btn_cura, btn_escudo, btn_cuero, btn_malla, btn_cura_mayor, btn_lava;

    [SerializeField] LoadScene load;
    public Puzzle lista;
    
    private int llaves, llaveMaestra, pocionVida, pocionLava, monedas;
    [Header("Iconos Inventario")]
    [SerializeField] Sprite iconoLlave;
    [SerializeField] Sprite iconoLlaveMaestra;
    [SerializeField] Sprite iconoPocionVida;
    [SerializeField] Sprite iconoDaga;
    [SerializeField] Sprite iconoEspada;
    [SerializeField] Sprite iconoPocionLava;
    [SerializeField] Sprite iconoMonedas;
    [SerializeField] Sprite iconoCuero;
    [SerializeField] Sprite iconoMalla;

    [Header("Degradado pantalla")]
    [SerializeField] TintScreen pantalla;

    [Header("Audio")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Protagonista")]
    [SerializeField] Parameters protagonista;
    public string nivel;

    void Start()
    {
        escenaState = GameObject.Find("personaje").GetComponent<InputHandler>();
        Update_Money();
    }
    void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;

        root = uIDocument.rootVisualElement;

        _mainPanel = root.Q<VisualElement>("Main_menu");
        _optionsPanel = root.Q<VisualElement>("Options");

        dinero_actual = root.Q<Label>("dinero-actual");
        coste_compra = root.Q<Label>("coste-compra");

        root.Q<Button>("Exit").clicked += Exit;

        btn_cura = root.Q<Button>("btn-curacion");
        btn_cuero = root.Q<Button>("btn-cuero"); 
        btn_malla = root.Q<Button>("btn-malla"); 
        btn_lava = root.Q<Button>("btn-pocion-lava");

        btn_cura.clicked += Pocion_Cura;
        btn_lava.clicked += Pocion_Lava;

        //SetInventario();
    }
    void Pocion_Cura()
    {
        //numero de monedas que tenemos
        monedas = protagonista.Inventario.Monedas;
        // si llega, restamos monedas y sumamos uno al inventario
        if (monedas >= cost_vida)
        {
            protagonista.Inventario.Monedas -= cost_vida;
            
            protagonista.Inventario.PocionVida.Add("pocion_tienda");
            Update_Money();
        }
        //si no llega no puede comprar
        else
        {
            Debug.Log("Monedas insuficientes");
        }
    }
    void Pocion_Lava()
    {
        //numero de monedas que tenemos
        monedas = protagonista.Inventario.Monedas;
        // si llega, restamos monedas y sumamos uno al inventario
        if (monedas >= cost_lava)
        {
            protagonista.Inventario.Monedas -= cost_lava;
            
            protagonista.Inventario.PocionLava.Add("pocion_tienda");
            Update_Money();
        }
        //si no llega no puede comprar
        else
        {
            Debug.Log("Monedas insuficientes");
        }
    }
    void Exit()
    {
        //volver a poder moverse
        escenaState.ScenePause(false);

        //mouse
        Cursor.lockState = CursorLockMode.Locked;

        SceneManager.UnloadSceneAsync("Tienda_Yusseif");
    }
    void Update_Money()
    {
        dinero_actual.text = protagonista.Inventario.Monedas.ToString();
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

    // --- INVENTARIO ---
    bool areListEqual()
    {
        // Null check del inventario completo
        if (protagonista == null || protagonista.Inventario == null) return false;

        // Null check de cada lista antes de llamar .Count()
        if (protagonista.Inventario.Llave == null) return false;
        if (protagonista.Inventario.LlaveMaestra == null) return false;
        if (protagonista.Inventario.PocionVida == null) return false;
        //if (protagonista.Inventario.Daga == null) return false;
        if (protagonista.Inventario.PocionLava == null) return false;
        //if (protagonista.Inventario.Espada == null) return false;

        if (protagonista.Inventario.Llave.Count != llaves) return false;
        if (protagonista.Inventario.LlaveMaestra.Count != llaveMaestra) return false;
        if (protagonista.Inventario.PocionVida.Count != pocionVida) return false;
        if (protagonista.Inventario.PocionLava.Count != pocionLava) return false;
        if (protagonista.Inventario.Monedas != monedas) return false;
        //if (protagonista.Inventario.Daga.Count != daga) return false;
        //if (protagonista.Inventario.Espada.Count != espada) return false;

        return true;
    }
    void SetInventario()
    {
        // Null check antes de acceder a las listas
        if (protagonista == null || protagonista.Inventario == null) return;
        if (protagonista.Inventario.Llave == null) return;
        if (protagonista.Inventario.LlaveMaestra == null) return;
        if (protagonista.Inventario.PocionVida == null) return;
        if (protagonista.Inventario.PocionLava == null) return;
        if (protagonista.Inventario.Monedas == 0) return;
        //if (protagonista.Inventario.Daga == null) return;
        //if (protagonista.Inventario.Espada == null) return;

        llaves = protagonista.Inventario.Llave.Count;
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count;
        pocionVida = protagonista.Inventario.PocionVida.Count;
        pocionLava = protagonista.Inventario.PocionLava.Count;
        //daga = protagonista.Inventario.Daga.Count;
        //espada = protagonista.Inventario.Espada.Count;
        monedas = protagonista.Inventario.Monedas;

        SetSlot(0, llaves > 0 ? iconoLlave : null, llaves);
        SetSlot(1, llaveMaestra > 0 ? iconoLlaveMaestra : null, llaveMaestra);
        SetSlot(2, pocionVida > 0 ? iconoPocionVida : null, pocionVida);
        //SetSlot(3, daga > 0 ? iconoDaga : null, daga);
        //SetSlot(4, espada > 0 ? iconoEspada : null, espada);
        //SetSlot(5, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(3, pocionLava > 0 ? iconoPocionLava : null, pocionLava);
        SetSlot(4, monedas > 0 ? iconoMonedas : null, monedas);
    }
    void SetSlot(int index, Sprite icono, int cantidad)
    {
        //poner el icono y numero
        var slotIcon = root.Q<VisualElement>($"slot-{index}-icon");
        var slotBadge = root.Q<Label>($"slot-{index}-badge");
        var slot = root.Q<VisualElement>($"slot-{index}");

        if (icono != null)
        {
            //si antes estaba vacio
            bool esNuevo = !slot.ClassListContains("inv-slot-active");
            slotIcon.style.backgroundImage = new StyleBackground(icono);
            slot.AddToClassList("inv-slot--active");

            //if (esNuevo) MostrarNotificacion(icono);

        }
        else
        {
            slotIcon.style.backgroundImage = StyleKeyword.None;
            slot.RemoveFromClassList("inv-slot--active");
        }

        slotBadge.text = cantidad.ToString();
        slotBadge.style.display = cantidad > 0
            ? DisplayStyle.Flex
            : DisplayStyle.None;
    }

}