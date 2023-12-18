using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sound // ����� c# Ŭ����
{
    public string name; // �� �̸�
    public AudioClip clip; // ��
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // static : ���� ���� �޸𸮰� ������


    private void Awake() // �̱���
    {
        #region singleton
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
    #endregion singleton


    public Sound[] bgmSounds; // bgm ����� Ŭ��
    public Sound[] effSounds; // ȿ���� ����� Ŭ��
    public AudioSource audioSourceBGM; // ������� ��� ������Ʈ
    public AudioSource[] audioSourceEFF;
    // ȿ���� ��� ������Ʈ, ȿ������ ���� ���� ���ÿ� ����� �� �����Ƿ� ���� �̸� �迭

    public string[] playEFFName; // ������� ȿ���� �̸� �迭


    private void Start()
    {
        playEFFName = new string[audioSourceEFF.Length];
    }

    public void PlaySE(string name) // ȿ���� ��� �Լ�
    {
        for (int i = 0; i < effSounds.Length; i++)
        {
            if(name == effSounds[i].name)
            {
                for (int j= 0; j < audioSourceEFF.Length; j++)
                {
                    if (!audioSourceEFF[j].isPlaying)
                    {
                        audioSourceEFF[j].clip = effSounds[i].clip;
                        audioSourceEFF[j].Play();
                        Debug.Log("���� �÷���");
                        playEFFName[j] = effSounds[i].name;
                        return;
                    }
                }
                Debug.Log("��� AudioSource�� ��� ���Դϴ�.");
                return;
            }
        }
        Debug.Log(name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void PlayBGM(string name) // bgm ��� �Լ�
    {
        for (int i=0; i< bgmSounds.Length; i++)
        {
            if(name == bgmSounds[i].name)
            {
                audioSourceBGM.clip = bgmSounds[i].clip;
                audioSourceBGM.Play();
                return;
            }
        }
        Debug.Log(name + "���尡 SoundManager�� ��ϵ��� �ʾҽ��ϴ�.");
    }

    public void StopAllSE() // ��� ȿ���� ��� ����
    {
        for (int i=0; i< audioSourceEFF.Length; i++)
        {
            audioSourceEFF[i].Stop();
        }
    }

    public void StopSE(string name) // Ư�� ȿ������ ��� ����
    {
        for (int i=0; i < audioSourceEFF.Length; i++)
        {
            if (playEFFName[i] == name)
            {
                audioSourceEFF[i].Stop();
                break;
            }
        }
        Debug.Log("��� ����" + name + "���尡 �����ϴ�.");
    }


}
   