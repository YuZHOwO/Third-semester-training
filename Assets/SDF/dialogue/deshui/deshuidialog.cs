using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class deshuidialog : MonoBehaviour
{
    public static bool isDeshuiTalk;//������̬bool���������ṩ��Dongwangdialogsystem
    // Start is called before the first frame update
    private bool isfirst = true;//�����ж��Ƿ��ǵ�һ�δ����Ի�
    [Header("UI���")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject box;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("ͷ��")]
    public Sprite headDeshui;
    public Sprite headXiaoHuiHui;
    public Sprite nothing;
    bool textFinished;  //�ı��Ƿ���ʾ���
    bool isTyping;  //�Ƿ���������ʾ
    bool isbox;//�Ƿ��Ѿ�ѡ����box

    List<string> textList = new List<string>();//�����ı���
    public List<Button> buttons = new List<Button>();//��������button

    void Awake()
    {
        GetTextFromFile(textFile);
    }

    void OnEnable()
    {
        if (isfirst)
        {
            index = 0;  //�Ի���ÿ�����ر�Ϊ��ʾ�����öԻ�
            textFinished = true;    //�Ի���ÿ�����ر�Ϊ��ʾ״̬��Ϊ�ı��ѽ���
            StartCoroutine(setTextUI());
        }
        else//��һ��֮�󴥷��ճ��Ի�
        {
            headImage.sprite = headDeshui;
            textLabel.text = "���ѽ�������и������Ŀ�ʯ��Ҫ����ķ���ǿ��һ���50���һ�Σ�����������+3��";
            box.SetActive(true);
        }

    }

    void Update()
    {
        if (isfirst)
        {
            //�������F�����ҶԻ�ȫ��������رնԻ���
            if (Input.GetKeyDown(KeyCode.F) && index == textList.Count - 1)
            {
                gameObject.SetActive(false);
                isfirst = false;
                isDeshuiTalk = true;
                return;
            }

            //����F������ǰ���ı���ɾ�ִ��Э�̣���ǰ���ı�δ��ɾ�ֱ����ʾ��ǰ���ı�
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
            //�����ԶԻ�������޸�
            AddClickEvents();
            if (isbox)
            {
                box.SetActive(false);
                if (Input.GetKeyDown(KeyCode.F))//�ٴΰ���F���رնԻ���
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
        foreach (Button item in buttons)//����buttons����Ķ���
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
            case "strengthen"://��������ø���̳�playerȻ���޸����Ľ�����͹�����
                if (isbox) return;
                if (PlayerController.coinNum >= 50)
                {
                    //Debug.Log("buy");
                    textLabel.text = "����50��ң�����ǿ����ķ��ߣ����úã�";
                    isbox = true;
                    PlayerController.coinNum -= 50;
                    PlayerController.attackNum += 3;
                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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
        //����ı�����
        textList.Clear();

        //�и��ı��ļ�����Ȼ��һ��һ�мӵ�list������
        var lineDate = file.text.Split('\n');
        foreach (var line in lineDate)
        {
            textList.Add(line);
        }
        isfirst = true;
    }

    IEnumerator setTextUI()
    {
        textFinished = false;   //����������ʾ״̬
        textLabel.text = "";    //�����ı�����

        //�ж��ı��ļ��������
        switch (textList[index].Trim())
        {
            case "��ˮ":
                headImage.sprite = headDeshui;
                index++;
                break;
            case "С�һ�":
                headImage.sprite = headXiaoHuiHui;
                index++;
                break;
            case "�԰�":
                headImage.sprite = nothing;
                index++;
                break;
        }

        //ÿ��һ��F������һ������
        int word = 0;
        while (isTyping && word < textList[index].Length - 1)
        {
            //������ʾ
            textLabel.text += textList[index][word];
            word++;
            yield return new WaitForSeconds(textSpeed);
        }
        //������ʾ�ı�����Ϊ��������
        textLabel.text = textList[index];

        isTyping = true;
        textFinished = true;
        index++;
    }
}
