using RPG.Combat;
using RPG.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    EnemyHealthDisplay EnemyHealthDisplay;


    private void Awake()
    {
        EnemyHealthDisplay = FindObjectOfType<EnemyHealthDisplay>();
    }

    private void OnEnable()
    { 
            EnemyHealthDisplay.UpdateHealthDisplayUI();

    }
}
