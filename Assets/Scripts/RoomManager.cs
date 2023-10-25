using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class RoomManager : MonoBehaviour
{
    public GameObject GuestRoomPrefab;
    public GameObject RestaurantPrefab;
    public GameObject EmptyBackPrefab;

    public static RoomManager Instance { get { return Instance; } }


    public bool isRoomEditing;
    public GameObject roomEditing;

    //�������� �ü��� ������ ��, ������ Ŭ���ϸ� �ü��� ��ġ�� ��. 

    public void makeRoom(string roomType)
    {
        //Ÿ�Կ� �´� �ü� prefab ����
        switch (roomType) 
        {
            case "Guest Room":
                roomEditing = GuestRoomPrefab;
                break;
            case "Restaurant":
                roomEditing = RestaurantPrefab;
                break;
            default: break;
        }
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
