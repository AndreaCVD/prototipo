using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class CommandPanel : MonoBehaviour
{
    [Header("Ficha personaje")]
    [SerializeField] Parameters protagonista;
    [Header("Iconos Inventario")]
    [SerializeField] Sprite iconoLlave;
    [SerializeField] Sprite iconoLlaveMaestra;
    [SerializeField] Sprite iconoPocionVida;
    [SerializeField] Sprite iconoDaga;
    [SerializeField] Sprite iconoEspada;
    [SerializeField] Sprite iconoPocionLava;
    [SerializeField] Sprite iconoMonedas;

    [SerializeField] CommandManager commandManager;
    [SerializeField] Dice diceSprite;

    private GameObject load_script;
    private LoadScene loadScene;

    private VisualElement root;
    VisualElement options_menu;
    VisualElement fuerza_options, intel_options, caris_options, inventoryGrid, 
        tirada_armadura, tirada_final, tirada_critica, tirada_fatidica;

    //fila principal
    private Button btnFUE, btnCAR, btnINT, btnITEM;
    
    //fila ataque fuerza
    private Button btnDAGA, btnESPADA, btnBACK;
    //fila ataque intel
    private Button btnINMOV, btnESCUDO, btnBACK_intel;
    //fila ataque carisma
    private Button btnLOVE, btnINTIMIDAR, btnBACK_carisma;
    //fila ataque item
    private Button btnBACK_item, btn_slot_2;
    //boton dado tirada final
    private Button btnDADO, btnDADO_CA, btnCRITICO, btnFATIDICO;

    //fila secundaria
    private Button btnItem, btnRun;
    //contadores inventory
    private int llaves, llaveMaestra, pocionVida, pocionLava, monedas;

    private Label info_tirada, info_result;

    public string armadura, nom_ataque;
    private int stat, veces_tirada, MAX_vida;
    
    // Variables activas de combate
    private bool escudo, inLove;
    public int enamorado;
    void Start()
    {
        MAX_vida = protagonista.stats.Get(PersonajesStats.Max_Vida);
        armadura = " ";
        nom_ataque = " ";
        veces_tirada = 1;
        enamorado = 0;
        //inLove = commandManager.Return_inLove();
        inLove = commandManager.enemigo_inLove;

        llaves = 0;
        llaveMaestra = 0;
        pocionVida = 0;
        pocionLava = 0;
        monedas = 0;

        if (load_script == null)
        {
            load_script = GameObject.Find("--SceneManagement--");
            loadScene = load_script.GetComponent<LoadScene>();

        }

        var uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        //Volver a menu opciones
        btnBACK = root.Q<Button>("btn-BACK");
        btnBACK_intel = root.Q<Button>("btn-intelBACK");
        btnBACK_carisma = root.Q<Button>("btn-carisBACK");
        
        //Botones
        btnFUE = root.Q<Button>("btn-FUE");
            btnDAGA = root.Q<Button>("btn-DAGA");
            btnESPADA = root.Q<Button>("btn-ESPADA");

        //Botones inteligencia
        btnINT = root.Q<Button>("btn-INT");
            btnINMOV = root.Q<Button>("btn-INMOV");
            btnESCUDO = root.Q<Button>("btn-ESCUDO");
            
        //Botones carisma
        btnCAR = root.Q<Button>("btn-CAR");
            btnLOVE = root.Q<Button>("btn-ENAMORAR");
            btnINTIMIDAR = root.Q<Button>("btn-INTIMIDAR"); 

        //Botones Bolsa Items
        btnITEM = root.Q<Button>("btn-ITEM");
            btnBACK_item = root.Q<Button>("btn-ITEM-BACK");
            btn_slot_2 = root.Q<Button>("btn-slot-2"); //pocion vida

        btnRun = root.Q<Button>("btn-huir");
        btnDADO = root.Q<Button>("btn-DADO");
        btnCRITICO = root.Q<Button>("btn-CRITICO");
        btnFATIDICO = root.Q<Button>("btn-FATIDICO");
        btnDADO_CA = root.Q<Button>("btn-DADO-CA");
        //Visual Elements
        options_menu = root.Q<VisualElement>("option_menu");
        fuerza_options = root.Q<VisualElement>("atq-FUE");
        intel_options = root.Q<VisualElement>("atq-INTEL");
        caris_options = root.Q<VisualElement>("atq-CARISMA");
        inventoryGrid = root.Q<VisualElement>("inventory-grid");
        tirada_final = root.Q<VisualElement>("tirada-FINAL");
        tirada_critica = root.Q<VisualElement>("tirada-CRITICA");
        tirada_armadura = root.Q<VisualElement>("tirada-ARMADURA");
        tirada_fatidica = root.Q<VisualElement>("tirada-FATIDICA");
        
        //texto tirada
        info_tirada = root.Q("info-tirada").Q<Label>();
        info_tirada.text = " ";
        info_result = root.Q("resultado-info").Q<Label>();
        info_result.style.visibility = Visibility.Hidden;


        //info_tirada.label = " ";
        // eventos
        btnFUE.clicked += Menu_Fuerza;
            btnDAGA.clicked += Daga;
            btnESPADA.clicked += Espada;

        btnINT.clicked += Menu_Intel;
            btnINMOV.clicked += Inmovilizar;
            btnESCUDO.clicked += Escudo;
            //btnBACK_intel.clicked += Back_intel;

        btnCAR.clicked += Menu_Carisma;
            btnLOVE.clicked += Enamorar;
            btnINTIMIDAR.clicked += Intimidar;

        btnRun.clicked += Huir;
        btnDADO.clicked += TiradaAlEnemigo;
        btnCRITICO.clicked += TiradaAlEnemigo;
        btnDADO_CA.clicked += TiradaArmadura;
        btnFATIDICO.clicked += TiradaFatidica;

        //inventary
        btnITEM.clicked += Abrir_Inventario;
            btnBACK_item.clicked += Back_item;
            btn_slot_2.clicked += () => UsarItem(2);
        
        btnBACK.clicked += Back;
        btnBACK_intel.clicked += Back;
        btnBACK_carisma.clicked += Back;
    }

    void FixedUpdate()
    {
        //inventary
        if (!areListEqual())
        {
            SetInventario();
        }
        if (inLove && enamorado != 3)
        {
            btnLOVE.SetEnabled(false);
            inLove = commandManager.Return_inLove();
            //inLove = commandManager.enemigo_inLove;

        }
    }
    void OnDisable()
    {
        btnFUE.clicked -= Menu_Fuerza;
        btnINT.clicked -= Menu_Intel;
        btnCAR.clicked -= Menu_Carisma;
        btnITEM.clicked -= Abrir_Inventario;
        btnRun.clicked -= Huir;
    }
    // --- INFO TIRADA ---
    void Texto_Tirada()
    {
        //info_tirada.style.display = DisplayStyle.Flex;

        //info_tirada.label = " ";
        //info_tirada.value = "";
        switch (stat)
        {
            case 0: //fuerza
                string f = protagonista.stats.Get(PersonajesStats.Fuerza).ToString();
                info_tirada.text = "+" + f + "(fuerza)";
                break;
            case 1:// inteligencia
                string i = protagonista.stats.Get(PersonajesStats.Inteligencia).ToString();
                info_tirada.text = "+" + i + "(intel)";
                break;
            case 2:// carisma
                string c = protagonista.stats.Get(PersonajesStats.Carisma).ToString();
                info_tirada.text = "+" + c + "(carisma)";
                break;
            default:
                break;
        }
    }
    void Resultado_Tirada()
    {
        info_result.style.visibility = Visibility.Visible;

        switch (armadura)
        {
            case "si":
                info_result.text = "Bien hecho";
                break;
            case "critico":
                info_result.text = "Tirada critica";
                break;
            case "fatidico":
                info_result.text = "Tirada fatidica";
                break;
            case "no":
                info_result.text = "CA no superada";
                break;
            default:
                info_result.text = "???";
                break;
        }
    }
    // --- VOLVER AL MENU PRINCIPAL ---
    public void Back()
    {
        options_menu.style.display = DisplayStyle.Flex;

        if (fuerza_options.style.display == DisplayStyle.Flex)
        {
            fuerza_options.style.display = DisplayStyle.None;
        }
        if (intel_options.style.display == DisplayStyle.Flex)
        {
            intel_options.style.display = DisplayStyle.None;
        }
        if (caris_options.style.display == DisplayStyle.Flex)
        {
            caris_options.style.display = DisplayStyle.None;
        }
        if (tirada_armadura.style.display == DisplayStyle.Flex)
        {
            tirada_armadura.style.display = DisplayStyle.None;
        }
        if (tirada_final.style.display == DisplayStyle.Flex)
        {
            tirada_final.style.display = DisplayStyle.None;
        }
        if (tirada_critica.style.display == DisplayStyle.Flex)
        {
            tirada_critica.style.display = DisplayStyle.None;
        }
        if (tirada_fatidica.style.display == DisplayStyle.Flex)
        {
            tirada_fatidica.style.display = DisplayStyle.None;
        }
        Resetear_Valores();
        //fuerza_options.style.display = index == 0 ? DisplayStyle.Flex : DisplayStyle.None;
    }

    // --- ARMADURA ---
    public void Menu_TiradaArmadura()
    {
        tirada_armadura.style.display = DisplayStyle.Flex;
        fuerza_options.style.display = DisplayStyle.None;
    }
    public void TiradaArmadura()
    {
        // 0 = Nat 20
        // 1 = Nat 1
        // 2 = Tirada normal, AC superada
        // 3 = AC NO superada
        if (nom_ataque == "enamorado" || nom_ataque == "intimidar")
        {
            TiradaArmadura(0);
        }
        else
        {
            int AC_superada = commandManager.Armadura(stat, 20);
            if (AC_superada == 2)
            {
                tirada_armadura.style.display = DisplayStyle.None;
                armadura = "si";
                veces_tirada = 1;
                
                Resultado_Tirada();
                NextAction();
            }
            else if (AC_superada == 0)
            {
                tirada_armadura.style.display = DisplayStyle.None;
                armadura = "critico";
                veces_tirada = 2;

                Resultado_Tirada();
                NextAction();
            }
            else if (AC_superada == 1) //ha tirado un 1
            {
                tirada_armadura.style.display = DisplayStyle.None;
                // el jugador se hace daño a si mismo
                armadura = "fatidico";

                Resultado_Tirada();
                NextAction();
            }
            else //(AC_superada == 3)
            {
                armadura = "no";

                Resultado_Tirada();
                Back();
            }
        }
    }
    public void TiradaArmadura(int a) //Para Enamorado
    {
        // 0 = Nat 20
        // 1 = Nat 1
        // 2 = Tirada normal, AC superada
        // 3 = AC NO superada

        int AC_superada = commandManager.Armadura(stat, 20);
        if (AC_superada == 2)
        {
            tirada_armadura.style.display = DisplayStyle.None;
            armadura = "si";
            veces_tirada = 1;
        }
        else //(AC_superada == 3)
        {
            tirada_armadura.style.display = DisplayStyle.None;
            armadura = "no";
        }

        if (nom_ataque == "intimidar")
            Intimidar();
        else
            Enamorar();
    }

    // --- TIRADAS FINALES ---
    public void Menu_TiradaFinal()
    {
        tirada_final.style.display = DisplayStyle.Flex;
    }
    public void Menu_TiradaCritico()
    {
        tirada_critica.style.display = DisplayStyle.Flex;
    }
    void TiradaAlEnemigo()
    {
        if (escudo)
        {
            Escudo();
        }
        switch (nom_ataque)
        {
            case "daga":
                commandManager.Change_img("daga");
                commandManager.Fuerza(8, veces_tirada);
                break;
            case "espada":
                commandManager.Change_img("espada");
                commandManager.Fuerza(12, veces_tirada);
                break;
            default:
                Debug.Log("No se ha leido bien el nombre del ataque");
                break;
        }
        Resetear_Valores();
        //volver a menu inicial
        Back();
        diceSprite.CambiarSprite(1);
    }

    // --- TIRADA FINAL D1 AL PROPIO JUGADOR ---
    public void Menu_TiradaFatidica()
    {
        tirada_fatidica.style.display = DisplayStyle.Flex;
    }
    public void TiradaFatidica()
    {
        commandManager.AutoHerirse(4, 1);
        Resetear_Valores();
        //volver a menu inicial
        Back();
        //diceSprite.CambiarSprite(1);
    }
    
    // --- RESETEAR TIRADA ---
    void Resetear_Valores()
    {
        info_tirada.text = " ";
        StartCoroutine(ChangeText(2)); //sacaer el mensaje de "Bien hecho"

        nom_ataque = " ";
        stat = 10;
        armadura = " ";
    }
    IEnumerator ChangeText(int time)
    {
        yield return new WaitForSeconds(time);
        info_result.style.visibility = Visibility.Hidden;

    }

    // --- NEXT ACTION QUE TIENE QUE HACER EL PLAYER ---
    public void NextAction() 
    {
        //Mira la armadura y que menus abrir
        if (armadura == "no") //No super el AC
        {
            Back();
            Resetear_Valores();
        }
        else if (armadura == "critico") //El jugador tira NAT 20
        {
            fuerza_options.style.display = DisplayStyle.None;
            Menu_TiradaCritico();
        }
        else if (armadura == "fatidico") //El jugador tira NAT 1
        {
            fuerza_options.style.display = DisplayStyle.None;
            Menu_TiradaFatidica();
        }
        else //si supera el AC
        {
            fuerza_options.style.display = DisplayStyle.None;
            Menu_TiradaFinal();
        }
    }
        
    // --- BOTON FUERZA ---
    public void Menu_Fuerza()
    {
        //hacer visible los ataques de fuerza
        options_menu.style.display = DisplayStyle.None;
        fuerza_options.style.display = DisplayStyle.Flex;

        stat = 0; //Stat de fuerza = 0
        Texto_Tirada();
    }
    public void Daga()
    {
        if (armadura == " ") //No ha hecho nada aun
        {
            nom_ataque = "daga";
            Menu_TiradaArmadura();
        }
        else
        {
            NextAction();
        }   
    }
    public void Espada()
    {
        if (armadura == " ") //No ha hecho nada aun
        {
            nom_ataque = "espada";
            Menu_TiradaArmadura();
        }
        else
        {
            NextAction();
        }
    }

    //--- BOTON INTELIGENCIA ---
    public void Menu_Intel()
    {
        options_menu.style.display = DisplayStyle.None;
        intel_options.style.display = DisplayStyle.Flex;

        stat = 1; //Stat de inteligencia = 1
        Texto_Tirada();
    }
    public void Inmovilizar()
    {
        commandManager.EnemigoInmovilizado(true, 1);
        btnINMOV.SetEnabled(false); //usar solo una vez por partida

        info_result.style.visibility = Visibility.Visible;
        info_result.text = "Enemigo Inmovilizado";

        Back();
        //tirar dos veces, enemigo inmovil
    }
    public void Escudo()
    {
        //tirar sin tener que tirar d20
        // te suma temporalmente +2 CA
        if ( !escudo)
        {
            escudo = true;
            commandManager.Modificar_CA(2);
            btnESCUDO.SetEnabled(false);

            commandManager.Change_img("escudo");

            info_result.style.visibility = Visibility.Visible;
            info_result.text = "Escudo activado";

        }
        else
        {
            escudo = false;
            commandManager.Modificar_CA(-2);
            btnESCUDO.SetEnabled(true);

            commandManager.Change_img("idle_prota");

            info_result.style.visibility = Visibility.Visible;
            info_result.text = "Escudo desactivado";
        }
    }

    //Boton Carisma
    public void Menu_Carisma()
    {
        options_menu.style.display = DisplayStyle.None;
        caris_options.style.display = DisplayStyle.Flex;
        stat = 2; //Stat de carisma = 2
        Texto_Tirada();
    }
    public void Enamorar()
    {
        if (armadura == " ") //No ha hecho nada aun
        {
            nom_ataque = "enamorado";
            caris_options.style.display = DisplayStyle.None;
            Menu_TiradaArmadura();
        }
        else if (armadura == "no")
        {
            Resetear_Valores();
            Back();

            info_result.style.visibility = Visibility.Visible;
            info_result.text = "CA no superada";

            commandManager.NextTurn();
            // Fallas enamoramiento == se enfada
        }
        else //Armadura Si
        {
            //si se usa 3 veces --> enemigo estado Enamorado
            enamorado++;
            if (enamorado == 1)
                commandManager.Change_img("enamorado_1");
            if (enamorado == 2)
                commandManager.Change_img("enamorado_2");
            if (enamorado == 3 && !inLove)
            {
                //enamorado por 30 segundos
                btnLOVE.SetEnabled(false);
                StartCoroutine(Enamorado(30));
                commandManager.enemigoEnamorado();
            }

            info_result.style.visibility = Visibility.Visible;
            info_result.text = "Enemigo enamorado no te ataca";

            Resetear_Valores();
            Back();
            Debug.Log("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            commandManager.NextTurn();
        }
    }
    public void Intimidar()
    {
        // Aciertas == Menos daño al enemigo -> asustado -d4
        // Fallas == Mas daño al enemigo -> enfadadp +1d4
        if (armadura == " ") //No ha hecho nada aun
        {
            nom_ataque = "intimidar";
            caris_options.style.display = DisplayStyle.None;
            Menu_TiradaArmadura();
        }
        else if (armadura == "no")
        {
            Debug.Log("armadura no del prota");
            //enemigo enfadado
            Resetear_Valores();
            Back();
            commandManager.EstadoIntimidar("enfadado", true);
            commandManager.Change_img("enfadado");
        }
        else if (armadura == "si")
        {
            Debug.Log("armadura si del prota");

            //enemigo enfadado
            Resetear_Valores();
            Back();
            commandManager.EstadoIntimidar("asustado", true);

        }
        //dura 1 turno
    }
    // SECUNDARIAS
    //Boton Huir
    public void Huir()
    {
        //Ataque de oportunidad del enemigo de d4
        commandManager.Huir();
    }
    // --- INVENTARIO ---
    void Abrir_Inventario()
    {
        options_menu.style.display = DisplayStyle.None;
        inventoryGrid.style.display = DisplayStyle.Flex;
    }
    void Back_item()
    {
        options_menu.style.display = DisplayStyle.Flex;
        inventoryGrid.style.display = DisplayStyle.None;
    }
    void UsarItem(int slot)
    {
        //vemos que item es y lo sacamos del inventario
        switch (slot)
        {
            case 0: //llave normal
                break;
             case 1: //llave maestra
                break;
             case 2: //Pocion de vida
                Debug.Log("El jugador usa una pocion, recupera 10 de vida");
                int vida = protagonista.stats.values[3].value;
                if (vida > 0 && vida <= MAX_vida-10) //MAX vida - 10
                {
                    protagonista.stats.values[3].value += 10;
                    protagonista.Inventario.PocionVida.RemoveAt(protagonista.Inventario.PocionVida.Count - 1);
                }
                break;
             case 3: //pocion lava
                break;
             case 4: //monedas
                break;
            default:
                break;
        }
        //actualizamos el inventario
    }
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
        if (protagonista.Inventario.Monedas.Count != monedas) return false;
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
        if (protagonista.Inventario.Monedas == null) return;
        //if (protagonista.Inventario.Daga == null) return;
        //if (protagonista.Inventario.Espada == null) return;

        llaves = protagonista.Inventario.Llave.Count;
        llaveMaestra = protagonista.Inventario.LlaveMaestra.Count;
        pocionVida = protagonista.Inventario.PocionVida.Count;
        pocionLava = protagonista.Inventario.PocionLava.Count;
        //daga = protagonista.Inventario.Daga.Count;
        //espada = protagonista.Inventario.Espada.Count;
        monedas = protagonista.Inventario.Monedas.Count;

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
    // --- COROUTINES ---
    IEnumerator Enamorado(int time)
    {
        yield return new WaitForSeconds(time);
        commandManager.enemigo_inLove = false;
    }
}