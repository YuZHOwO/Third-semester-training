using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class sunxiaomaodialog : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("UI���")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject bottle;
    public Sprite headSunXiaoMao;
    bool isbottle;//�Ƿ��Ѿ�ѡ����bottle

    public List<Button> buttons = new List<Button>();//��������button

    void Start()
    {

    }
    void OnEnable()
    {
        textLabel.text = "�٣�С�һң��ҿ������µ�����ҩˮ��Ҫ������ֻҪ15���һƿ��!";
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
        //ͨ���жϵ����ť�����ֵ�����Ӧ�ķ���
        switch (buttons[a].name)
        {
            case "blueBottle1"://��������ø���̳�playerȻ���޸����Ľ������ҩˮ��
                              //bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 15)
                {
                    if (PlayerController.blueBottleNum >= 3)
                    {
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy");
                    textLabel.text = "����15��ң���������1ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 15;
                    PlayerController.blueBottleNum += 1;


                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy2");
                    textLabel.text = "����30��ң���������2ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 30;
                    PlayerController.blueBottleNum += 2;

                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy3");
                    textLabel.text = "����45��ң���������3ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 45;
                    PlayerController.blueBottleNum += 3;

                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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

