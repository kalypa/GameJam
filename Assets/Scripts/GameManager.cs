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
    public bool[] itemBuyData = new bool[4];
    //�������� ����ϰ� �ִ°�? �� ���� �����͸� �����ϴ� bool �迭
    public bool[] itemUseData = new bool[4];
    public bool[] itemUseData;
    //�÷��̾� ���׹̳� ��ȭ ����
    public int statusLevel;
}

public class GameManager : MonoSingleton<GameManager>
{
    //�÷��̾� ������ �Ҵ�
    public PlayerData playerData = new PlayerData();

    //�ʿ� ���� ��� ����
    string directoryPath;
    string filePath;

    private void Awake()
    {
        //���丮 ���
        directoryPath = Application.dataPath + "/PlayerData";
        //���� ���
        filePath = Application.dataPath + "/PlayerData/PlayerData.txt";
    }

    void Start()
    {
        //���� �ε�
        if (File.Exists(directoryPath))
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
            Debug.Log("�� ���̺�");
        }
        else
        {
            //���ο� ���丮�� ���丮 ��ο� ����
            Directory.CreateDirectory(directoryPath);
            //JsonData��� Json string �������� PlayerData �� ����
            string JsonData = JsonUtility.ToJson(playerData);
            //���� ��ο� JsonData�� ��� �ؽ�Ʈ ������ �ۼ�
            File.WriteAllText(filePath, JsonData);
            Debug.Log("���̺� �����");
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
        Debug.Log("�ε� �Ϸ�");
    }

    void Update()
    {

        //A�� ���� ��� ����
        if (Input.GetKeyDown(KeyCode.A))
        {
            Save();
        }
    }
}
