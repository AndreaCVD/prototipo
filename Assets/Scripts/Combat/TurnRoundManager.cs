using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnRoundManager : MonoBehaviour
{
    //decir a quien le toca

    //variable de combat monster
    public CombatMonster current;
    void Update()
    {
        Debug.Log(current.name);
    }
}
