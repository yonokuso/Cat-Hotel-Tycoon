using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    static GameObject container;

    // ---�̱������� ����--- //
    static DataManager instance;

    public static DataManager Instance
    {
        get
        {
            if (!instance) // �̱������� ����
            {
                container = new GameObject(); // ������Ʈ �ڵ� ����
                container.name = "DataManager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container); // ���� �Ѿ�� ������Ʈ ����
            }
            return instance;
        }
    }

    // --- ���� ������ �����̸� ���� ("���ϴ� �̸�(����).json") --- //
    string GameDataFileName = "GData.json";

    // --- ����� Ŭ���� ���� --- //
    public GameData data = new GameData();


    // �ҷ�����
    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // ����� ������ �ִٸ�
        if (File.Exists(filePath))
        {
            // ����� ���� �о���� Json�� Ŭ���� �������� ��ȯ�ؼ� �Ҵ�
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
            print("�ҷ����� �Ϸ�");
        }
    }


    // �����ϱ�
    public void SaveGameData()
    {
        // Ŭ������ Json �������� ��ȯ (true : ������ ���� �ۼ�)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // �̹� ����� ������ �ִٸ� �����, ���ٸ� ���� ���� ����
        File.WriteAllText(filePath, ToJsonData);

    }
}
