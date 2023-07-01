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
    private bool isfirst = true;//�����ж��Ƿ��ǵ�һ�δ����Ի�
    bool issecond = true;
    [Header("UI���")]
    public Image headImage;
    public TMP_Text textLabel;
    public GameObject bottle;
    public TMP_Text systemNote;

    [Header("�ı��ļ�")]
    public TextAsset textFile;
    public TextAsset textFile0;
    public int index;
    public float textSpeed;

    [Header("ͷ��")]
    public Sprite headWangDaMao;
    public Sprite headSunXiaoMao;
    public Sprite headXiaoHuiHui;
    bool textFinished;  //�ı��Ƿ���ʾ���
    bool isTyping;  //�Ƿ���������ʾ
    bool isbottle;//�Ƿ��Ѿ�ѡ����bottle

    List<string> textList = new List<string>();//�����ı���
    public List<Button> buttons = new List<Button>();//��������button

    void Awake()
    {

        GetTextFromFile(textFile);

    }

    void OnEnable()
    {
        //isfirst = false;
        if (isnew) isDeshui = deshuidialog.isDeshuiTalk;//���þ�̬�����жϵ�ˮ�Ƿ񽲹�������isdeshui������
        if (isDeshui && issecond)
        {
            bottle.SetActive(false);
            GetTextFromFile(textFile0);
            isfirst = true;
        }
        if (isfirst)
        {
            index = 0;  //�Ի���ÿ�����ر�Ϊ��ʾ�����öԻ�
            textFinished = true;    //�Ի���ÿ�����ر�Ϊ��ʾ״̬��Ϊ�ı��ѽ���
            StartCoroutine(setTextUI());
        }
        else//�ڶ���֮�󴥷��ճ��Ի�
        {
            headImage.sprite = headWangDaMao;
            textLabel.text = "Ҫ��������ҩˮ��С���ӡ�����ػ�30�����һƿ���߹�·����Ҫ���ѽ��";
            bottle.SetActive(true);
        }

    }

    void Update()
    {
        
        if (isfirst)
        {
            //�������F�����ҶԻ�ȫ��������رնԻ���
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
        //ͨ���жϵ����ť�����ֵ�����Ӧ�ķ���
        switch (buttons[a].name)
        {
            case "redBottle1"://��������ø���̳�playerȻ���޸����Ľ������ҩˮ��
                              //bottle.SetActive(false);
                if (isbottle) return;
                if (PlayerController.coinNum >= 30 )
                {
                    if (PlayerController.redBottleNum >= 3)
                    {
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy");
                    textLabel.text = "����30��ң���������1ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 30;
                    PlayerController.redBottleNum += 1;

                    
                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy2");
                    textLabel.text = "����60��ң���������2ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 60;
                    PlayerController.redBottleNum += 2;

                }
                else
                {
                    textLabel.text = "���Ľ�Ҳ��㣡";
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
                        textLabel.text = "������Ʒ��������";
                        isbottle = true;
                        return;
                    }
                    //Debug.Log("buy3");
                    textLabel.text = "����90��ң���������3ƿ����ҩˮ����л�ݹˣ�";
                    isbottle = true;
                    PlayerController.coinNum -= 90;
                    PlayerController.redBottleNum += 3;

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
            case "����ë":
                headImage.sprite = headWangDaMao;
                index++;
                break;
            case "��Сë":
                headImage.sprite = headSunXiaoMao;
                index++;
                break;
            case "С�һ�":
                headImage.sprite = headXiaoHuiHui;
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

// Update is called once per frame
