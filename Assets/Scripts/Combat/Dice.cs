
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
    [SerializeField] Animator dado;

    [Header("Sprites")]
    public Image image;

    [Header("Usar diferentes fotos")]
    [SerializeField] Sprite[] imagenesDados;
    //[SerializeField] List<Image> Fotos Dado = new List<Image>();
    //public Image imageContainer;

    //public void SetImage(int index)
    //{
    //    if (index >= 0 && index < images.Length)
    //    {
    //        imageContainer.sprite = images[index];
    //    }

    
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
        

            Move_dado20(a);

        //primero hay que ver que dado es, si el de 20, 4 o mas
        //ver que animators estan activos o no, y enviar el numero

        return a;
    }
    IEnumerator ChangeText(int a)
    {
        //diceText.text = a.ToString();
        yield return new WaitForSeconds(time);
        diceText.text = "?";
    }

    void Move_dado20(int num)
    {
        dado.SetInteger("dado", num);
    }
    void Move_dado12(int num)
    {

    }
}
