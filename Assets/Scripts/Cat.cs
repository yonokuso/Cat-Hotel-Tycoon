using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float likeability;
    public Animator animator;

    public CatState myState;
    public AnimationState myAnimationState;

    public float moveSpeed = 50f;
    private Rigidbody2D rb;

    private Room myRoom;
    private Room EnterHotel;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myState = CatState.EnterHotel;
        myAnimationState = AnimationState.Walk;
        rb = GetComponent<Rigidbody2D>();
        EnterHotel = GameObject.FindGameObjectWithTag("EnterHotel").GetComponent<Room>();
    }

    public void Update()
    {
        Acting();
        DecideAction();
    }

    public void DecideAction()
    {
        var distance = Vector2.Distance(gameObject.transform.position, GuestManager._this.HotelEnterPosition.transform.position);
        if (distance <= 1.5f)
        {
            myState = CatState.MoveToRoom;
        }
    }

    public void DecideMyRoom()
    {
        if (RoomManager._this.numGuestRoom() <= 0)
        {
            StoreManager._this.SetGuideTextMessage("손님이 머물 방이 없어요! 상점에서 게스트룸을 구매해주세요.");
            StoreManager._this.ShowGuideTextMessage();
        }
        else
        {
            StoreManager._this.HideGuideTextMessage();
            foreach (var room in RoomManager._this.GuestRooms)
            {
              if ( room.GetComponentInParent<Room>().CatGuest == null)
                {
                    room.GetComponentInParent<Room>().CatGuest = gameObject;
                    myRoom = room.GetComponentInParent<Room>();
                    Debug.Log("방이 배정되었습니다.");
                    break;
                }
            }
        }
    }

    public void Acting()
    {
        if (myState == CatState.EnterHotel)
        {
            MovePos(gameObject.transform.position , GuestManager._this.HotelEnterPosition.transform.position);
        }
        else if (myState == CatState.MoveToRoom)
        {
            if (myRoom == null)
            {
                DecideMyRoom();
            }
            else
            {
                Move(EnterHotel, myRoom);
            }
        }
        else if (myState == CatState.UsingRoom)
        {

        }
        else if(myState == CatState.Pay)
        {

        }
        else if(myState == CatState.LeaveHotel)
        {

        }
    }

    public void AnimActing(AnimationState state)
    {
        //각 행동에 맞는 애니메이션하게끔
    }


    //시설(객실 또는 상점) 정보를 얻으면, 시설로 이동하는 함수
    public void Move(Room StartRoom, Room targetRoom)
    {
        var distanceY = Vector2.Distance(
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x,gameObject.transform.position.y),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x,
            GuestManager._this.HotelEnterPosition.transform.position.y + (-targetRoom.roomfloor + StartRoom.roomfloor) * 165f));

        var distanceX = Vector2.Distance(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + ( targetRoom.roomNumber - StartRoom.roomNumber) * 100f, gameObject.transform.position.y));

        //같은 층에 있지 않다면...엘레베이터로 타고 이동하게끔
        if (targetRoom.roomfloor != StartRoom.roomfloor &&  distanceY > 1.5f )
        {
            MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, gameObject.transform.position.y + ( - targetRoom.roomfloor + StartRoom.roomfloor) * 165));
        }
        else if (distanceX > 1.5f)
        {
            Debug.Log(distanceX);
            MovePos(gameObject.transform.position, new Vector2(gameObject.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100, gameObject.transform.position.y));
      
        }
        
    }

    void MovePos(Vector2 startPosition, Vector2 endPosition)
    {
        // 현재 위치에서 목표 위치까지의 방향 벡터 계산
        Vector2 direction = (endPosition - startPosition).normalized;

        // 목표 위치까지의 거리 계산
        float distance = Vector2.Distance(startPosition, endPosition);

        // 보간된 이동 벡터 계산
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;

        // 현재 위치에서 목표 위치까지의 거리가 이동 거리보다 클 경우에만 이동
        if (distance > movement.magnitude)
        {
            // Rigidbody를 사용하여 보간된 위치로 이동
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // 목표 위치에 도달한 경우 다음 이동을 위해 시작 위치와 끝 위치 교환
            Vector2 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
        }
    }


    //시설에 들어가거나 나갈 때 부르는 함수
    public void EnterRoom(Room room)
    {
        //어느 시설에 들어갈 것인지
        //시설의 isUsing 값을 true로 바꾸기
        room.isUsing = true;
    }

    public void ExitRoom(Room room)
    {
        //시설의 isUsing 값을 False로 바꾸기
        room.isUsing = false;
    }

}
