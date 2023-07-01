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

    public void OnPause()//点击“暂停”时执行此方法
    {
        Time.timeScale = 0;
        ingameMenu.SetActive(true);
    }

    public void OnResume()//点击“回到游戏”时执行此方法
    {
        Time.timeScale = 1f;
        ingameMenu.SetActive(false);
    }

    public void OnRestart()//点击“重新开始”时执行此方法
    {
        //Loading Scene0
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void SaveGame()//点击保存游戏使用此方法
    {
        Save();
    }
    public void LoadGame()//点击加载存档使用此方法
    {
        Load();
        SceneManager.LoadScene(5);
    }
    public void Quitgame()
    {
        Application.Quit();
    }
    private Save CreateSave()//创建一个全新存档
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
    private void Save()//调用CreateSave函数
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



    private void Load()//加载存档函数
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
                //读档获取数据给static成员
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
        if (Input.GetKeyDown(KeyCode.Escape))//按esc同样达成暂停
        {
            OnPause();
        }
    }
}