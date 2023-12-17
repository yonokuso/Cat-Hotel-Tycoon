using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public float likeability;
    public Animator animator;
    public CatState myState;
    public AnimationState myAnimationState;
    public float moveSpeed = 50f;
    public Text catTxt;
    
    private Rigidbody2D rb;
    private Room myRoom;
    private Room EnterHotel;
    private bool isCatDecideInHotelAction;
    private int usingNum;

    float elapsedTime = 0f;
    float waitTime = 3f;
    
    private string[] CatTxt = { "와!", "배고프다..", "기대돼~", "뭐할까?", "재미있다!", "목말라", "룰루랄라~", "멋지다!" };

    public void Start()
    {
        //animator = gameObject.GetComponent<Animator>();
        myState = CatState.EnterHotel;
        myAnimationState = AnimationState.Walk;
        rb = GetComponent<Rigidbody2D>();
        EnterHotel = GameObject.FindGameObjectWithTag("EnterHotel").GetComponent<Room>();

        isCatDecideInHotelAction = false;
        usingNum = 0;
    }

    public void Update()
    {
        Acting();
        DecideAction();
    }

    public void DecideAction()
    {
        if (myState == CatState.EnterHotel)
        {
            var distance = Vector2.Distance(gameObject.transform.position, GuestManager._this.HotelEnterPosition.transform.position);
            if (distance <= 1.5f)
            {
                myState = CatState.MoveToRoom;
            }
        }
        else if(myState == CatState.MoveToRoom && myRoom != null)
        {
            var distance = Vector2.Distance(gameObject.transform.position, 
                new Vector2 (GuestManager._this.HotelEnterPosition.transform.position.x + myRoom.roomNumber * 100f, 
                GuestManager._this.HotelEnterPosition.transform.position.y + myRoom.roomfloor * 165f));

            if (distance <= 2f)
            {
                myState = CatState.UsingRoom;
            }
        }
        else if (myState == CatState.UsingRoom)
        {
            if(usingNum >= 5)
            {
                myState = CatState.LeaveHotel;
            }
            
        }

    }

    public void UsingHotel()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= waitTime)
        {
            catTxt.text = CatTxt[Random.Range(0, CatTxt.Length)]; 
            usingNum += 1;
            elapsedTime = 0f;
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
                    catTxt.text = CatTxt[Random.Range(0, CatTxt.Length)];
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
            catTxt.text = "대기중...";
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
            UsingHotel();
        }
        else if(myState == CatState.Pay)
        {
            catTxt.text = "돈내야지!";
        }
        else if(myState == CatState.LeaveHotel)
        {
            catTxt.text = "즐거웠어!";

            var distance = Vector2.Distance(gameObject.transform.position, GuestManager._this.HotelEnterPosition.transform.position);

            if (distance <= 1.5f)
            {
                MovePos(gameObject.transform.position, GuestManager._this.CatSpawnPosition.transform.position);
                var ExitDistance = Vector2.Distance(gameObject.transform.position, GuestManager._this.CatSpawnPosition.transform.position);
                if ( ExitDistance< 1.5f)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                Move(myRoom, EnterHotel);
            }

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
            new Vector2(0,gameObject.transform.position.y),
            new Vector2(0,GuestManager._this.HotelEnterPosition.transform.position.y + ( targetRoom.roomfloor - StartRoom.roomfloor) * 165f));

        var distanceX = Vector2.Distance(
            new Vector2(gameObject.transform.position.x, 0),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + ( targetRoom.roomNumber - StartRoom.roomNumber) * 100f, 0));

        //같은 층에 있지 않다면...엘레베이터로 타고 이동하게끔
        if (targetRoom.roomfloor != StartRoom.roomfloor &&  distanceY > 2f  && StartRoom.roomfloor == 0)
        {
            Debug.Log("distance Y : " + distanceY);
            MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, gameObject.transform.position.y + ( + targetRoom.roomfloor-+ StartRoom.roomfloor) * 165));
        }
        else if (distanceX > 2f && StartRoom.roomfloor == 0)
        {
            Debug.Log("distance X : " + distanceX);
            MovePos(gameObject.transform.position, new Vector2(gameObject.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100, gameObject.transform.position.y));
        }

        if (targetRoom.roomfloor != StartRoom.roomfloor && distanceX > 2f && StartRoom.roomfloor != 0)
        {
            Debug.Log("distance X : " + distanceX);
            MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100, gameObject.transform.position.y));

        }
        else if (distanceY > 2f && StartRoom.roomfloor != 0)
        {
            Debug.Log("distance Y : " + distanceY);
            MovePos(gameObject.transform.position, new Vector2(gameObject.transform.position.x, GuestManager._this.HotelEnterPosition.transform.position.y + (+targetRoom.roomfloor - +StartRoom.roomfloor) * 165));

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
