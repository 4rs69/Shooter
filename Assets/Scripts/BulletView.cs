using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class BulletView : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _numberBulletsUGUI;
    
    
    public void SetBullets(int numberBullets)
    {
        _numberBulletsUGUI.text = numberBullets.ToString();
    }
}
