using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public int roomfloor;
    public int roomNumber;
    public bool isUsing = false;
    public bool isRoomEditing = false;
    public string roomType;
    public GameObject CatGuest;
    protected GameObject room;
    


    public void Start()
    {
        room = gameObject;
        int index = transform.parent.GetSiblingIndex();
        roomfloor = index;
        index = transform.GetSiblingIndex();
        roomNumber = index;
    }

}
