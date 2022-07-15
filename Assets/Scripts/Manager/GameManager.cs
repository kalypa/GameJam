using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//�÷��̾� ������ Ŭ����
[System.Serializable]
public class PlayerData
{
    //��
    public int playerMoney;
    //�ְ�����
    public int highScore;
    //�������� ��°�? �� ���� �����͸� �����ϴ� bool �迭
    public bool[] itemBuyData;
    //�������� ����ϰ� �ִ°�? �� ���� �����͸� �����ϴ� bool �迭
    public bool[] itemUseData;
    //�÷��̾� ���׹̳� ��ȭ ����
    public int statusLevel;

    public float bgmScale;
    public float sfxScale;

    public bool firstStart;
}

public class GameManager : MonoSingleton<GameManager>
{
    //�÷��̾� ������ �Ҵ�
    public PlayerData playerData;

    //�ʿ� ���� ��� ����
    string directoryPath;
    string filePath;

    private void Awake()
    {
        //���丮 ���
        directoryPath = Application.persistentDataPath + "PlayerData";
        //����Ƽ ���丮
        //directoryPath = Application.dataPath + "/PlayerData";
        //���� ���
        filePath = Application.persistentDataPath + "PlayerData/PlayerData.txt";
        //����Ƽ ����
        //filePath = Application.dataPath + "/PlayerData/PlayerData.txt";
        //���� �ε�
        if (File.Exists(filePath))
        {
            LoadJson();
        }
    }

    /// <summary>
    /// ���� ����
    /// </summary>
    public void Save()
    {

        //���� ���丮 ��� ���� ������ ���� ���
        if (File.Exists(directoryPath))
        {
            //�ش� ��ο� ������ ����
            File.Delete(filePath);
            //JsonData ��� Json string �������� playerData �� ����
            string JsonData = JsonUtility.ToJson(playerData);
            //���� ��ο� JsonData�� ��� �ؽ�Ʈ ������ �ۼ�
            File.WriteAllText(filePath, JsonData);
        }
        else
        {
            //���ο� ���丮�� ���丮 ��ο� ����
            Directory.CreateDirectory(directoryPath);
            //JsonData��� Json string �������� PlayerData �� ����
            string JsonData = JsonUtility.ToJson(playerData);
            //���� ��ο� JsonData�� ��� �ؽ�Ʈ ������ �ۼ�
            File.WriteAllText(filePath, JsonData);
        }
    }

    /// <summary>
    /// ���� �ε�
    /// </summary>
    public void LoadJson()
    {
        //filePath�� �ִ� ��� Text�� �д´�.
        string JsonData = File.ReadAllText(filePath);
        //���� �׽�Ʈ�� PlayerData ���·� �츮�� ���� �ִ� playerData�� �ִ´�.
        playerData = JsonUtility.FromJson<PlayerData>(JsonData);
    }
    void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            Save();
        }
    }
    void OnApplicationQuit()
    {
        Save();
    }
}