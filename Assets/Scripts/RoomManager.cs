using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject GuestRoomPrefab;
    public GameObject RestaurantPrefab;
    public GameObject StorePrefab;

    public static RoomManager _this;

    public bool isRoomEditing;

    public List<GameObject> GuestRooms;

    //�������� �ü��� ������ ��, ������ Ŭ���ϸ� �ü��� ��ġ�� ��. 

    public void Start()
    {
        if (_this == null) _this = this;
    }


    public void makeRoom(string roomType, Transform _transform)
    {
        _this.isRoomEditing = true;
        //Ÿ�Կ� �´� �ü� prefab ����
        switch (roomType) 
        {
            case "Guest Room":
                GameObject guestroom =  Instantiate(GuestRoomPrefab, _transform);
                GuestRooms.Add(guestroom);
                break;
            case "Restaurant":
                Instantiate(RestaurantPrefab, _transform);
                break;
            case "Convenience Store":
                    Instantiate(StorePrefab, _transform);
                break;
            default: break;
        }
    }

    public int numGuestRoom()
    {
        return GuestRooms.Count;
    }


    //Edit��忡�� �ü��� �����ϸ�, �巡�� ������� ��ġ�ϰ� Ȯ����ư�� ������ �ü��� �̵� ��ġ��.
    public void EditRoom(Room room)
    {
        room.isRoomEditing = true;
        arrangeRoom(room);

    }

    //�巡�� ������� ��ġ�ϰ� Ȯ����ư�� ������ �ü��� �̵� ��ġ
    public void arrangeRoom(Room room)
    {
        //���� ���� num�� Ȯ����
    }


    //�ü��� �����ϸ�, �����Ͻðڽ��ϱ�? ��� �˾��� �߰� ������ ��
    public void DeleteRoom(Room room)
    {
        //�˾� ����
        //�ü� �����ϱ�
    }


}
