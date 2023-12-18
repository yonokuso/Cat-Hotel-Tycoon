using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sound // ����� c# Ŭ����
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // static : ���� ���� �޸𸮰� ������

    public AudioSource audioSourceBGM; // ������� ��� ������Ʈ
    public AudioSource audioSourceEFF; 
    // ȿ���� ��� ������Ʈ, ȿ������ ���� ���� ���ÿ� ����� �� �����Ƿ� ���� �̸� �迭

    public string[] playEFFName; // ������� ȿ���� �̸� �迭


    private void Awake() // �̱���
    {
        if (instance == null)
        {
            instance = this; // ����Ŵ��� �Ҵ�
            //SceneManager.sceneLoaded += OnSceneLoaded; // ���� �ٲ� ��� ����?
            
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject); // ������Ʈ ����
    }
    

    private void Start()
    {
        playEFFName = new string[0];//����
    }

    public void PlaySE(string name)
    {

    }

    public void stopAllSE()
    {

    }

    public void stopSE()
    {

    }


}
   