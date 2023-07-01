using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class doordialog : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool isfirst = true;//�����ж��Ƿ��ǵ�һ�δ����Ի�
    [Header("UI���")]
    public Image headImage;
    public TMP_Text textLabel;
    //public GameObject box;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public int index;
    public float textSpeed;

    [Header("ͷ��")]
    public Sprite headYuzi;
    public Sprite headXiaoHuiHui;
    public Sprite nothing;
    public Sprite headWangdamao;
    public Sprite headSunxiaomao;
    public Sprite headDeshui;
    bool textFinished;  //�ı��Ƿ���ʾ���
    bool isTyping;  //�Ƿ���������ʾ
    //bool isbox;//�Ƿ��Ѿ�ѡ����box

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
            gameObject.SetActive(false);
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
            case "����":
                headImage.sprite = headYuzi;
                index++;
                break;
            case "С�һ�":
                headImage.sprite = headXiaoHuiHui;
                index++;
                break;
            case "�԰�":

                index++;
                break;
            case "����ë":
                headImage.sprite = headWangdamao;
                index++;
                break;
            case "��Сë":
                headImage.sprite = headSunxiaomao;
                index++;
                break;
            case "��ˮ":
                headImage.sprite = headDeshui;
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
