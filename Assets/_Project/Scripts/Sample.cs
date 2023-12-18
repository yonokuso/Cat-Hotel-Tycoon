using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    private void Start() // 시작하자마자 불러오기
    {
        DataManager.Instance.LoadGameData();
    }

    private void OnApplicationQuit() // 게임종료 시 자동저장
    {
        DataManager.Instance.SaveGameData();
    }
}
