
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class Dice : MonoBehaviour
{
    //[SerializeField] CanvasGroup canvas_dice;
    //[SerializeField] Transform dice_obj;
    private float time = 1.5f;

    [Header("Textos dado")]
    [SerializeField] TMP_Text diceText;

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

    private Vector3 dice01 = new Vector3(292.55f, 176.27f, 91.85f);
    private Vector3 dice02 = new Vector3(6.10f, 305.98f, 232.03f);
    private Vector3 dice03 = new Vector3(349.85f, 198.20f, 119.52f);
    private Vector3 dice04 = new Vector3(9.33f, 19.66f, 239.92f);
    private Vector3 dice05 = new Vector3(9.33f, 19.66f, 239.92f);
    private Vector3 dice06 = new Vector3(300.61f, 92.40f, 288.16f);
    private Vector3 dice07 = new Vector3(355.73f, 127.12f, 129.28f);
    private Vector3 dice08 = new Vector3(293.78f, 179.11f, 271.41f);
    private Vector3 dice09 = new Vector3(59.41f, 277.29f, 75.90f);
    private Vector3 dice10 = new Vector3(354.75f, 234.63f, 229.93f);
    private Vector3 dice11 = new Vector3(4.50f, 303.99f, 49.65f);
    private Vector3 dice12 = new Vector3(299.85f, 273.21f, 245.92f);
    private Vector3 dice13 = new Vector3(69.39f, 0.80f, 88.42f);
    private Vector3 dice14 = new Vector3(7.88f, 56.96f, 315.08f);
    private Vector3 dice15 = new Vector3(60.05f, 93.32f, 112.84f);
    private Vector3 dice16 = new Vector3(353.66f, 124.91f, 311.30f);
    private Vector3 dice17 = new Vector3(346.42f, 162.25f, 63.72f);
    private Vector3 dice18 = new Vector3(10.76f, 342.68f, 297.57f);
    private Vector3 dice19 = new Vector3(350.84f, 234.66f, 47.33f);
    private Vector3 dice20 = new Vector3(75.52f, 355.54f, 266.67f);

    public int RollDice(int maxValue)
    {
        int a = Random.Range(1, maxValue);
        Debug.Log("Dice=" + a);
        StartCoroutine(ChangeText(a));
        //MoveDice(a);
        return a;
        
    }
    IEnumerator ChangeText(int a)
    {
        diceText.text = a.ToString();
        yield return new WaitForSeconds(time);
        diceText.text = "?";

    }
    //private void MoveDice(int num)
    //{
    //    switch(num)
    //    {
    //        case 1:
    //            dice_obj.Rotate(dice01 * Time.deltaTime * time);
    //            break;
    //        case 2:
    //            dice_obj.Rotate(dice02 * Time.deltaTime * time);
    //            break;
    //        case 3:
    //            dice_obj.Rotate(dice03 * Time.deltaTime * time);
    //            break;
    //        case 4:
    //            dice_obj.Rotate(dice04 * Time.deltaTime * time);
    //            break;
    //        case 5:
    //            dice_obj.Rotate(dice05 * Time.deltaTime * time);
    //            break;
    //        case 6:
    //            dice_obj.Rotate(dice06 * Time.deltaTime * time);
    //            break;
    //        case 7:
    //            dice_obj.Rotate(dice07 * Time.deltaTime * time);
    //            break;
    //        case 8:
    //            dice_obj.Rotate(dice08 * Time.deltaTime * time);
    //            break;
    //        case 9:
    //            dice_obj.Rotate(dice09 * Time.deltaTime * time);
    //            break;
    //        case 10:
    //            dice_obj.Rotate(dice10 * Time.deltaTime * time);
    //            break;
    //        case 11:
    //            dice_obj.Rotate(dice11 * Time.deltaTime * time);
    //            break;
    //        case 12:
    //            dice_obj.Rotate(dice12 * Time.deltaTime * time);
    //            break;
    //        case 13:
    //            dice_obj.Rotate(dice13 * Time.deltaTime * time);
    //            break;
    //        case 14:
    //            dice_obj.Rotate(dice14 * Time.deltaTime * time);
    //            break;
    //        case 15:
    //            dice_obj.Rotate(dice15 * Time.deltaTime * time);
    //            break;
    //        case 16:
    //            dice_obj.Rotate(dice16 * Time.deltaTime * time);
    //            break;
    //        case 17:
    //            dice_obj.Rotate(dice17 * Time.deltaTime * time);
    //            break;
    //        case 18:
    //            dice_obj.Rotate(dice18 * Time.deltaTime * time);
    //            break;
    //        case 19:
    //            dice_obj.Rotate(dice19 * Time.deltaTime * time);
    //            break;
    //        case 20:
    //            dice_obj.Rotate(dice20 * Time.deltaTime * time);
    //            break;
    //        default:
    //            break;
    //    }
        //transform.Rotate(Vector3.right * Time.deltaTime);
        //transform.Translate(movement * Time.deltaTime * time); //Moure objecte
    //}
}
