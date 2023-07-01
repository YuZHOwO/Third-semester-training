using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnegyBar : MonoBehaviour
{
    private float maxEP;
    private float currentEP;
    public Image energyPoint;
    // Start is called before the first frame update
    void Start()
    {
        maxEP = FindObjectOfType<PlayerController>().GetMaxEP();
        currentEP = FindObjectOfType<PlayerController>().EP;
    }

    // Update is called once per frame
    void Update()
    {
        currentEP = FindObjectOfType<PlayerController>().EP;
        Amout();
    }

    void Amout()
    {
        energyPoint.fillAmount = currentEP / maxEP;
    }
}
