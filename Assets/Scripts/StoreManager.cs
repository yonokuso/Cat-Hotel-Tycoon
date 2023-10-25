using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager: MonoBehaviour
{
    public GameObject StorePanel;
    public Image RoomImage;
    public Text message;


    public void Start()
    {

        HideStorePanel();
        HideTextMessage();

    }

    public void ShowStorePanel()
    {
        StorePanel.SetActive(true);
    }

    public void HideStorePanel()
    { 
        StorePanel.SetActive(false); 
    }

    public void ShowTextMessage()
    {
        message.gameObject.SetActive(true);
    }
    public void HideTextMessage()
    {
        message.gameObject.SetActive(false);
    }


    public void BuyFacility(string roomType)
    {
        string txt= "설치할 곳을 클릭해주세요!";
        message.text = txt;
        ShowTextMessage();
        HideStorePanel();
        RoomManager.Instance.makeRoom(roomType);
    }

}
