using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour
{

    private void Start() // �������ڸ��� �ҷ�����
    {
        DataManager.Instance.LoadGameData();
    }

    private void OnApplicationQuit() // �������� �� �ڵ�����
    {
        DataManager.Instance.SaveGameData();
    }
}
