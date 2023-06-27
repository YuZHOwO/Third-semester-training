using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    // Start is called before the first frame update
    public int startCoinquantity;
    public Text CoinQuantity;

    public static int CurrentCoinQuantity;
    void Start()
    {
        CurrentCoinQuantity = startCoinquantity;
    }

    // Update is called once per frame
    void Update()
    {
        CoinQuantity.text = CurrentCoinQuantity.ToString();
    }
}
