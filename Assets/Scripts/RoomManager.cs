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

    //상점에서 시설을 선택한 후, 객실을 클릭하면 시설이 설치가 됨. 

    public void Start()
    {
        if (Instance == null) Instance = this;
    }


    public void makeRoom(string roomType, Transform _transform)
    {
        Instance.isRoomEditing = true;
        //타입에 맞는 시설 prefab 생성
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


    //Edit모드에서 시설을 선택하면, 드래그 드롭으로 배치하고 확정버튼을 누르면 시설이 이동 배치됨.
    public void EditRoom(Room room)
    {
        room.isRoomEditing = true;
        arrangeRoom(room);

    }

    //드래그 드롭으로 배치하고 확정버튼을 누르면 시설이 이동 배치
    public void arrangeRoom(Room room)
    {
        //룸의 층과 num이 확정됨
    }


    //시설을 선택하면, 삭제하시겠습니까? 라는 팝업이 뜨고 삭제가 됨
    public void DeleteRoom(Room room)
    {
        //팝업 띄우기
        //시설 삭제하기
    }


}
