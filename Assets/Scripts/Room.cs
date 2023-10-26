using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour, IPointerClickHandler
{
    public int roomfloor;
    public int roomNumber;
    public bool isUsing = false;
    public bool isRoomEditing = false;
    public string roomType;
    protected GameObject room;
    private Image imageComponent;


    public void Start()
    {
        imageComponent = GetComponent<Image>();
        room = gameObject;
        int index = transform.parent.GetSiblingIndex();
        roomfloor = index;
        index = transform.GetSiblingIndex();
        roomNumber = index;
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
       // Debug.Log("클릭됨");
        if (RoomManager.Instance.isRoomEditing)
        {
          //  Debug.Log("편집중!");
            gameObject.GetComponent<Image>().enabled = false;
            RoomManager.Instance.makeRoom(StoreManager.SelectRoom, transform);

            RoomManager.Instance.isRoomEditing = false;
        }
    }
}
