using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Back : MonoBehaviour
{
    private void OnMouseDown()
    {
        if (RoomManager._this.isRoomEditing)
        {
            RoomManager._this.makeRoom(StoreManager.SelectRoom, transform.parent);

            StoreManager._this.HideGuideTextMessage();
            RoomManager._this.isRoomEditing = false;
        }
    }
}
