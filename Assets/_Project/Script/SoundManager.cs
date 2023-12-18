using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sound // 평범한 c# 클래스
{
    public string name; // 곡 이름
    public AudioClip clip; // 곡
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance; // static : 게임 내내 메모리가 유지됨


    private void Awake() // 싱글톤
    {
        #region singleton
        if (instance == null)
        {
            instance = this; // 사운드매니저 할당
            //SceneManager.sceneLoaded += OnSceneLoaded; // 씬이 바뀌어도 브금 지속?
            
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject); // 오브젝트 유지
    }
    #endregion singleton


    public Sound[] bgmSounds; // bgm 오디오 클립
    public Sound[] effSounds; // 효과음 오디오 클립
    public AudioSource audioSourceBGM; // 배경음악 재생 컴포넌트
    public AudioSource[] audioSourceEFF;
    // 효과음 재생 컴포넌트, 효과음은 여러 개가 동시에 재생될 수 있으므로 사운드 이름 배열

    public string[] playEFFName; // 재생중인 효과음 이름 배열


    private void Start()
    {
        playEFFName = new string[audioSourceEFF.Length];
    }

    public void PlaySE(string name) // 효과음 재생 함수
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
                        Debug.Log("음악 플레이");
                        playEFFName[j] = effSounds[i].name;
                        return;
                    }
                }
                Debug.Log("모든 AudioSource가 사용 중입니다.");
                return;
            }
        }
        Debug.Log(name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void PlayBGM(string name) // bgm 재생 함수
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
        Debug.Log(name + "사운드가 SoundManager에 등록되지 않았습니다.");
    }

    public void StopAllSE() // 모든 효과음 재생 멈춤
    {
        for (int i=0; i< audioSourceEFF.Length; i++)
        {
            audioSourceEFF[i].Stop();
        }
    }

    public void StopSE(string name) // 특정 효과음만 재생 멈춤
    {
        for (int i=0; i < audioSourceEFF.Length; i++)
        {
            if (playEFFName[i] == name)
            {
                audioSourceEFF[i].Stop();
                break;
            }
        }
        Debug.Log("재생 중인" + name + "사운드가 없습니다.");
    }


}
   