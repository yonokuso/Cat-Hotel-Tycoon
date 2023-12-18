using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager: MonoBehaviour
{
   // public GameObject StorePanel;
  //  public Image RoomImage;
    public Text message;
    public static string SelectRoom;
    public static StoreManager _this;

    public void Start()
    {
        if (_this == null) _this = this;
        HideGuideTextMessage();
    }
    public void ShowGuideTextMessage()
    {
        message.gameObject.SetActive(true);
    }
    public void HideGuideTextMessage()
    {
        message.gameObject.SetActive(false);
    }
    public void SetGuideTextMessage(string txt)
    {
        message.text = txt;
    }

    public void BuyFacility(string roomType)
    {
        SetGuideTextMessage("설치할 곳을 클릭해주세요!");
        ShowGuideTextMessage();
        RoomManager._this.isRoomEditing = true;
        SelectRoom = roomType;
    }

}
