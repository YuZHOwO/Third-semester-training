using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private float maxHP;
    private float currentHP;

    public Image healthPoint;
    // Start is called before the first frame update
    void Start()
    {
        maxHP= FindObjectOfType<PlayerController>().GetMaxHP();
        currentHP= FindObjectOfType<PlayerController>().HP;
    }

    // Update is called once per frame
    void Update()
    {
        currentHP = FindObjectOfType<PlayerController>().HP;
        Amout();
    }

    void Amout()
    {
        healthPoint.fillAmount = currentHP/maxHP;
    }
}
