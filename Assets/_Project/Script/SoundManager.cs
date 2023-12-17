using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]

public class Sound // 평범한 c# 클래스
{
    public string name;
    public AudioClip clip;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; // static : 게임 내내 메모리가 유지됨

    public AudioSource audioSourceBGM; // 배경음악 재생 컴포넌트
    public AudioSource audioSourceEFF; 
    // 효과음 재생 컴포넌트, 효과음은 여러 개가 동시에 재생될 수 있으므로 사운드 이름 배열

    public string[] playEFFName; // 재생중인 효과음 이름 배열


    private void Awake() // 싱글톤
    {
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
    

    private void Start()
    {
        playEFFName = new string[0];//수정
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
   