using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//플레이어 데이터 클래스
[System.Serializable]
public class PlayerData
{
    //돈
    public int playerMoney;
    //최고점수
    public int highScore;
    //아이템을 샀는가? 에 관련 데이터를 관리하는 bool 배열
    public bool[] itemBuyData;
    //아이템을 사용하고 있는가? 에 관련 데이터를 관리하는 bool 배열
    public bool[] itemUseData;
}

public class GameManager : MonoSingleton<GameManager>
{
    //플레이어 데이터 할당
    public PlayerData playerData = new PlayerData();

    //필요 파일 경로 제작
    string directoryPath;
    string filePath;

    private void Awake()
    {
        //디렉토리 경로
        directoryPath = Application.persistentDataPath + "/PlayerData";
        //파일 경로
        filePath = Application.persistentDataPath + "/PlayerData/PlayerData.txt";
    }

    void Start()
    {
        //파일 로드
        LoadJson();
    }

    /// <summary>
    /// 파일 저장
    /// </summary>
    public void Save()
    {

        //만약 디렉토리 경로 내에 파일이 있을 경우
        if (File.Exists(directoryPath))
        {
            //해당 경로에 파일을 삭제
            File.Delete(filePath);
            //JsonData 라는 Json string 형식으로 playerData 를 저장
            string JsonData = JsonUtility.ToJson(playerData);
            //파일 경로에 JsonData의 모든 텍스트 파일을 작성
            File.WriteAllText(filePath, JsonData);
        }
        else
        {
            //새로운 디렉토리를 디렉토리 경로에 생성
            Directory.CreateDirectory(directoryPath);
            //JsonData라는 Json string 형식으로 PlayerData 를 저장
            string JsonData = JsonUtility.ToJson(playerData);
            //파일 경로에 JsonData의 모든 텍스트 파일을 작성
            File.WriteAllText(filePath, JsonData);
        }
    }

    /// <summary>
    /// 파일 로드
    /// </summary>
    public void LoadJson()
    {
        //filePath에 있는 모든 Text를 읽는다.
        string JsonData = File.ReadAllText(filePath);
        //읽은 테스트를 PlayerData 형태로 우리가 갖고 있는 playerData에 넣는다.
        playerData = JsonUtility.FromJson<PlayerData>(JsonData);
    }

    void Update()
    {

        Save();

    }
}
