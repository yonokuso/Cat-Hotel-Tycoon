using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---싱글톤으로 선언--- //
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (!instance) // 싱글톤으로 관리
            {
                container = new GameObject(); // 오브젝트 자동 생성
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container); // 씬이 넘어가도 오브젝트 유지
            }
            return instance;
        }
    }

    // --- 게임 데이터 파일이름 설정 ("원하는 이름(영문).json") --- //
    string GameDataFileName = "GData.json";

    // --- 저장용 클래스 변수 --- //
    public GameData data = new GameData();


    // 불러오기
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 저장된 게임이 있다면
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
            print("불러오기 완료");
        }
    }


    // 저장하기
    public void SaveGameData()
    {
        // 클래스를 Json 형식으로 전환 (true : 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

    }
}
