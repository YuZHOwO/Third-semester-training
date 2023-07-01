using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class doordialog : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isfirst = true;//用来判断是否是第一次触发对话
    [Header("UI组件")]
    public Image headImage;
    public TMP_Text textLabel;
    //public GameObject box;

    [Header("文本文件")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("头像")]
    public Sprite headYuzi;
    public Sprite headXiaoHuiHui;
    public Sprite nothing;
    public Sprite headWangdamao;
    public Sprite headSunxiaomao;
    public Sprite headDeshui;
    bool textFinished;  //文本是否显示完毕
    bool isTyping;  //是否在逐字显示
    //bool isbox;//是否已经选择了box

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
            gameObject.SetActive(false);
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
            case "喻籽":
                headImage.sprite = headYuzi;
                index++;
                break;
            case "小灰灰":
                headImage.sprite = headXiaoHuiHui;
                index++;
                break;
            case "旁白":

                index++;
                break;
            case "王大毛":
                headImage.sprite = headWangdamao;
                index++;
                break;
            case "孙小毛":
                headImage.sprite = headSunxiaomao;
                index++;
                break;
            case "德水":
                headImage.sprite = headDeshui;
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
