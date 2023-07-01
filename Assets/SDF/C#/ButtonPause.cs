using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using UnityEngine.SceneManagement;

public class ButtonPause : MonoBehaviour
{
    private bool loadalready = false;
    //the ButtonPauseMenu
    public GameObject ingameMenu;

    public void OnPause()//�������ͣ��ʱִ�д˷���
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnResume()//������ص���Ϸ��ʱִ�д˷���
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }

    public void OnRestart()//��������¿�ʼ��ʱִ�д˷���
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void SaveGame()//���������Ϸʹ�ô˷���
    {
        Save();
    }
    public void LoadGame()//������ش浵ʹ�ô˷���
    {
        Load();
        SceneManager.LoadScene(5);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    private Save CreateSave()//����һ��ȫ�´浵
    {
        Save save = new Save();
        save.coinNum = PlayerController.coinNum;
        save.attackNum = PlayerController.attackNum;
        save.defendNum = PlayerController.defendNum;
        save.maxHP = PlayerController.maxHP;
        save.maxEP = PlayerController.maxEP;
        save.taskNum = PlayerController.taskNum;
        save.deathNum = PlayerController.deathNum;
        save.redBottleNum = PlayerController.redBottleNum;
        save.blueBottleNum = PlayerController.blueBottleNum;
        return save;
    }
    private void Save()//����CreateSave����
    {

        Save save = CreateSave();
        string filePath = Application.dataPath + "/StreamingFile" + "/byJson.json";

        string saveJsonStr = JsonMapper.ToJson(save);
        StreamWriter sw = new StreamWriter(filePath);
        sw.Write(saveJsonStr);
        sw.Close();
        //Debug.Log("save");
        loadalready = true;
    }



    private void Load()//���ش浵����
    {
        if (loadalready)
        {
            string filePath = Application.dataPath + "/StreamingFile" + "/byJson.json";
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
                string jsonStr = sr.ReadToEnd();
                sr.Close();
                Save save = JsonMapper.ToObject<Save>(jsonStr);
                //������ȡ���ݸ�static��Ա
                PlayerController.coinNum = save.coinNum;
                PlayerController.attackNum = save.attackNum;
                PlayerController.defendNum = save.defendNum;
                PlayerController.maxHP = save.maxHP;
                PlayerController.maxEP = save.maxEP;
                PlayerController.taskNum = save.taskNum;
                PlayerController.deathNum = save.deathNum;
                PlayerController.redBottleNum = save.redBottleNum;
                PlayerController.blueBottleNum = save.blueBottleNum;
                //Debug.Log("load");
                
            }

        }
        else
        {
            Debug.Log("no save");
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))//��escͬ�������ͣ
        {
            OnPause();
        }
    }
}