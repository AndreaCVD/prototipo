
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
//using static System.Net.Mime.MediaTypeNames;
using UnityEngine.UI;
using UnityImage = UnityEngine.UI.Image;

public class Dice : MonoBehaviour
{
    //[SerializeField] CanvasGroup canvas_dice;
    //[SerializeField] Transform dice_obj;
    private float time = 1.5f;

    [Header("Textos dado")]
    [SerializeField] TMP_Text diceText;
    [SerializeField] Animator anim_dado;

    [Header("Sprites")]
    [SerializeField] List<GameObject> dados = new List<GameObject>();

    [Header("Dados")]
    public Image image;

    [Header("Usar diferentes fotos")]
    [SerializeField] Sprite[] imagenesDados;

    GameObject dado_activar;
    GameObject dado_desactivar;

    //[SerializeField] List<Image> Fotos Dado = new List<Image>();
    //public Image imageContainer;

    //public void SetImage(int index)
    //{
    //    if (index >= 0 && index < images.Length)
    //    {
    //        imageContainer.sprite = images[index];
    //    }

    void Start()
    {
        foreach (var a in dados)
        {
            if (a.name == "d20")
                a.SetActive(true);
            else
                a.SetActive(false);
        }

    }
    public void cambiar_3d(int dado)
    {
        string aux = "d" + dado.ToString();

        foreach (var a in dados)
        {
            if (a.name == aux)
                dado_activar = a;

            else if (a.activeInHierarchy) // si esta actiu l'amaguem
                dado_desactivar = a;
        }

        StartCoroutine(ChangeModel(dado_activar, dado_desactivar, dado));
    }

    public void CambiarSprite(int dado_sprite)
    {
        // Asignar el nuevo sprite
        image.sprite = imagenesDados[dado_sprite];
    }
   // int a = diceRoller.RollDice(caras_1, tiradas_1, int caras_2, int tiradas_2);
   
    public int RollDice(int maxValue, int tiradas)
    {

        int a = 0;
        for (int aux = 0; aux < tiradas; aux++)
        {
            a += Random.Range(1, maxValue);
        }

        //StartCoroutine(ChangeText(a));


        //primero hay que ver que dado es, si el de 20, 4 o mas
        //ver que animators estan activos o no, y enviar el numero
        //Animar el dado
        foreach (var b in dados)
        {
            if (b.activeInHierarchy) // si esta actiu l'amaguem
            {
                anim_dado = b.GetComponent<Animator>();
                anim_dado.SetInteger("dado", a);
            }
        }

        return a;
    }
    IEnumerator ChangeModel(GameObject activar, GameObject desactivar, int dado)
    {
        yield return new WaitForSeconds(1);
        activar.SetActive(true);
        desactivar.SetActive(false);
    }

    void Move_dado20(int num)
    {
        anim_dado.SetInteger("dado", num);
    }
    void Move_dado12(int num)
    {

    }
}
