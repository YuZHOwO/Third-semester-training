using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class dongwangdialogsystem : MonoBehaviour
{
    
    // Start is called before the first frame update
    private bool isDeshui;
    private bool isnew;
    private bool isfirst = true;//用来判断是否是第一次触发对话
    bool issecond = true;
    [Header("UI组件")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject bottle;
    public TMP_Text systemNote;

    [Header("文本文件")]
    public TextAsset textFile;
    public TextAsset textFile0;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite headWangDaMao;
    public Sprite headSunXiaoMao;
    public Sprite headXiaoHuiHui;
    bool textFinished;  //文本是否显示完毕
    bool isTyping;  //是否在逐字显示
    bool isbottle;//是否已经选择了bottle

    List<string> textList = new List<string>();//储存文本用
    public List<Button> buttons = new List<Button>();//用来储存button

    void Awake()
    {

        GetTextFromFile(textFile);

    }

    void OnEnable()
    {
        //isfirst = false;
        if (isnew) isDeshui = deshuidialog.isDeshuiTalk;//运用静态变量判断德水是否讲过话，用isdeshui来接收
        if (isDeshui && issecond)
        {
            bottle.SetActive(false);
            GetTextFromFile(textFile0);
            isfirst = true;
        }
        if (isfirst)
        {
            index = 0;  //对话框每次隐藏变为显示就重置对话
            textFinished = true;    //对话框每次隐藏变为显示状态变为文本已结束
            StartCoroutine(setTextUI());
        }
        else//第二次之后触发日常对话
        {
            headImage.sprite = headWangDaMao;
            textLabel.text = "要来点生命药水吗？小伙子。最近特惠30个金币一瓶！走过路过不要错过呀！";
            bottle.SetActive(true);
        }

    }

    void Update()
    {
        
        if (isfirst)
        {
            //如果按下F键并且对话全部结束后关闭对话框
            if (Input.GetKeyDown(KeyCode.F) && index == textList.Count && !isDeshui)
            {
                Debug.Log("firstused");
                gameObject.SetActive(false);
                isfirst = false;
                isnew = true;

                return;
            }
            if (Input.GetKeyDown(KeyCode.F) && index == textList.Count)
            {

                gameObject.SetActive(false);
                isfirst = false;
                isnew = false;
                issecond = false;
               //Debug.Log("normal");
                return;
            }

            //按下F键，当前行文本完成就执行协程，当前行文本未完成就直接显示当前行文本
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (textFinished)
                {
                    StartCoroutine(setTextUI());
                }
                else if (!textFinished)
                {
                    isTyping = false;
                }
            }
        }
        else
        {
            //后续对对话框进行修改
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
            case "redBottle1"://这里可以让该类继承player然后修改他的金币数和药水数
                              //bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 30 )
                {
                    if (PlayerController.redBottleNum >= 3)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy");
                    textLabel.text = "收您30金币！这是您的1瓶生命药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 30;
                    PlayerController.redBottleNum += 1;

                    
                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbottle = true;
                }

                // if (Input.GetKeyDown(KeyCode.F)) gameObject.SetActive(false);
                break;
            case "redBottle2":
                //  bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 60)
                {
                    if (PlayerController.redBottleNum >= 2)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy2");
                    textLabel.text = "收您60金币！这是您的2瓶生命药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 60;
                    PlayerController.redBottleNum += 2;

                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbottle = true;
                }

                // if (Input.GetKeyDown(KeyCode.F)) gameObject.SetActive(false);
                break;
            case "redBottle3":
                //   bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 90)
                {
                    if (PlayerController.redBottleNum >= 1)
                    {
                        textLabel.text = "您的物品栏已满！";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy3");
                    textLabel.text = "收您90金币！这是您的3瓶生命药水！感谢惠顾！";
                    isbottle = true;
                    PlayerController.coinNum -= 90;
                    PlayerController.redBottleNum += 3;

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
    void GetTextFromFile(TextAsset file)
    {
        //清空文本内容
        textList.Clear();

        //切割文本文件内容然后一行一行加到list集合中
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
        isfirst = true;
    }

    IEnumerator setTextUI()
    {
        textFinished = false;   //进入文字显示状态
        textLabel.text = "";    //重置文本内容

        //判断文本文件里的内容
        switch (textList[index].Trim())
        {
            case "王大毛":
                headImage.sprite = headWangDaMao;
                index++;
                break;
            case "孙小毛":
                headImage.sprite = headSunXiaoMao;
                index++;
                break;
            case "小灰灰":
                headImage.sprite = headXiaoHuiHui;
                index++;
                break;
        }

        //每按一次F键播放一行文字
        int word = 0;
        while (isTyping && word < textList[index].Length - 1)
        {
            //逐字显示
            textLabel.text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }
        //快速显示文本内容为本行内容
        textLabel.text = textList[index];

        isTyping = true;
        textFinished = true;
        index++;
    }
}

// Update is called once per frame
