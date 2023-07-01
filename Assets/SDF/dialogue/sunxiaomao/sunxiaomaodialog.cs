using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sunxiaomaodialog : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI组件")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject bottle;
    public Sprite headSunXiaoMao;
    bool isbottle;//是否已经选择了bottle

    public List<Button> buttons = new List<Button>();//用来储存button

    void Start()
    {

    }
    void OnEnable()
    {
        textLabel.text = "嘿！小灰灰！我开发了新的能量药水，要来点吗？只要15金币一瓶呢!";
        bottle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        headImage.sprite = headSunXiaoMao;
        AddClickEvents();
        if (isbottle)
        {
            bottle.SetActive(false);
            if (Input.GetKeyDown(KeyCode.F))
            {
                gameObject.SetActive(false);
                isbottle = false;
            }

        }
    }
    void AddClickEvents()
    {

        int x = 0;
        foreach (Button item in buttons)
        {
            int y = x;
            item.onClick.AddListener(() => ClickEvent2(y));
            x++;
        }
    }
    void ClickEvent2(int a)
    {
        //通过判断点击按钮的名字调用相应的方法
        switch (buttons[a].name)
        {
            case "blueBottle1"://这里可以让该类继承player然后修改他的金币数和药水数
                              //bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 15)
                {
                    if (PlayerController.blueBottleNum >= 3)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy");
                    textLabel.text = "收您15金币！这是您的1瓶能量药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 15;
                    PlayerController.blueBottleNum += 1;


                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbottle = true;
                }

                // if (Input.GetKeyDown(KeyCode.F)) gameObject.SetActive(false);
                break;
            case "blueBottle2":
                //  bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 30)
                {
                    if (PlayerController.blueBottleNum >= 2)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy2");
                    textLabel.text = "收您30金币！这是您的2瓶能量药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 30;
                    PlayerController.blueBottleNum += 2;

                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbottle = true;
                }

                // if (Input.GetKeyDown(KeyCode.F)) gameObject.SetActive(false);
                break;
            case "blueBottle3":
                //   bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 45)
                {
                    if (PlayerController.blueBottleNum >= 1)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy3");
                    textLabel.text = "收您45金币！这是您的3瓶能量药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 45;
                    PlayerController.blueBottleNum += 3;

                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbottle = true;
                }

                //  if (Input.GetKeyDown(KeyCode.F)) gameObject.SetActive(false);
                break;
            case "quitbutton":
                gameObject.SetActive(false);
                break;

        }
    }
}

