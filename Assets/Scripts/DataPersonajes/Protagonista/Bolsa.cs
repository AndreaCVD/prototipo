using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nuevo Inventario", menuName = "Bolsa/Bag")]
public class Bolsa : ScriptableObject
{
    public string nombreObj;
    public List<string> Llave = new List<string>();
    public List<string> LlaveMaestra = new List<string>();
    public List<string> PocionVida = new List<string>();
    //public List<string> Daga = new List<string>();
    //public List<string> Espada = new List<string>();
    public List<string> PocionLava = new List<string>();
    public List<string> Monedas = new List<string>();

}