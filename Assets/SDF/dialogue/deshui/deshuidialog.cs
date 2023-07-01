using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class deshuidialog : MonoBehaviour
{
    public static bool isDeshuiTalk;//建立静态bool变量用来提供给Dongwangdialogsystem
    // Start is called before the first frame update
    private bool isfirst = true;//用来判断是否是第一次触发对话
    [Header("UI组件")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject box;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite headDeshui;
    public Sprite headXiaoHuiHui;
    public Sprite nothing;
    bool textFinished;  //文本是否显示完毕
    bool isTyping;  //是否在逐字显示
    bool isbox;//是否已经选择了box

    List<string> textList = new List<string>();//储存文本用
    public List<Button> buttons = new List<Button>();//用来储存button

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    void OnEnable()
    {
        if (isfirst)
        {
            index = 0;  //对话框每次隐藏变为显示就重置对话
            textFinished = true;    //对话框每次隐藏变为显示状态变为文本已结束
            StartCoroutine(setTextUI());
        }
        else//第一次之后触发日常对话
        {
            headImage.sprite = headDeshui;
            textLabel.text = "你好呀！我这有刚送来的矿石。要给你的防具强化一下嘛？50金币一次，防御力可以+3！";
            box.SetActive(true);
        }

    }

    void Update()
    {
        if (isfirst)
        {
            //如果按下F键并且对话全部结束后关闭对话框
            if (Input.GetKeyDown(KeyCode.F) && index == textList.Count - 1)
            {
                gameObject.SetActive(false);
                isfirst = false;
                isDeshuiTalk = true;
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
            if (isbox)
            {
                box.SetActive(false);
                if (Input.GetKeyDown(KeyCode.F))//再次按下F键关闭对话框
                {
                    gameObject.SetActive(false);
                    isbox = false;
                }

            }
        }
    }
    void AddClickEvents()
    {

        int x = 0;
        foreach (Button item in buttons)//遍历buttons里面的对象
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
            case "strengthen"://这里可以让该类继承player然后修改他的金币数和攻击力
                if (isbox) return;
                if (PlayerController.coinNum >= 50)
                {
                    //Debug.Log("buy");
                    textLabel.text = "收您50金币！这是强化后的防具，请拿好！";
                    isbox = true;
                    PlayerController.coinNum -= 50;
                    PlayerController.attackNum += 3;
                }
                else
                {
                    textLabel.text = "您的金币不足！";
                    isbox = true;
                }
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
            case "德水":
                headImage.sprite = headDeshui;
                index++;
                break;
            case "小灰灰":
                headImage.sprite = headXiaoHuiHui;
                index++;
                break;
            case "旁白":
                headImage.sprite = nothing;
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
