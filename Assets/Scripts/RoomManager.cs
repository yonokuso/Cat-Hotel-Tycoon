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
    public GameObject EmptyBackPrefab;

    public static RoomManager Instance;

    public bool isRoomEditing;

    //�������� �ü��� ������ ��, ������ Ŭ���ϸ� �ü��� ��ġ�� ��. 

    public void Start()
    {
        if (Instance == null) Instance = this;
    }


    public void makeRoom(string roomType, Transform _transform)
    {
        Instance.isRoomEditing = true;
        //Ÿ�Կ� �´� �ü� prefab ����
        switch (roomType) 
        {
            case "Guest Room":
                Instantiate(GuestRoomPrefab, _transform);
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
