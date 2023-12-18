using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager: MonoBehaviour
{
    public static string SelectRoom;
    public static StoreManager _this;
    public GameObject StoreCanvas;

    public void Start()
    {
        if (_this == null) _this = this;
    }
    public void BuyFacility(string roomType)
    {
        StoreCanvas.SetActive(false);
        GuideManager.instance.SetGuideMessage("��ġ�� ���� Ŭ�����ּ���");
        GuideManager.instance.OpenGuideBox();
        RoomManager._this.isRoomEditing = true;
        SelectRoom = roomType;
    }

}
