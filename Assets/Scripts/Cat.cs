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
            StoreManager._this.SetGuideTextMessage("�մ��� �ӹ� ���� �����! �������� �Խ�Ʈ���� �������ּ���.");
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
                    Debug.Log("���� �����Ǿ����ϴ�.");
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
        //�� �ൿ�� �´� �ִϸ��̼��ϰԲ�
    }


    //�ü�(���� �Ǵ� ����) ������ ������, �ü��� �̵��ϴ� �Լ�
    public void Move(Room StartRoom, Room targetRoom)
    {
        var distanceY = Vector2.Distance(
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x,gameObject.transform.position.y),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x,
            GuestManager._this.HotelEnterPosition.transform.position.y + (-targetRoom.roomfloor + StartRoom.roomfloor) * 165f));

        var distanceX = Vector2.Distance(
            new Vector2(gameObject.transform.position.x, gameObject.transform.position.y),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + ( targetRoom.roomNumber - StartRoom.roomNumber) * 100f, gameObject.transform.position.y));

        //���� ���� ���� �ʴٸ�...���������ͷ� Ÿ�� �̵��ϰԲ�
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
        // ���� ��ġ���� ��ǥ ��ġ������ ���� ���� ���
        Vector2 direction = (endPosition - startPosition).normalized;

        // ��ǥ ��ġ������ �Ÿ� ���
        float distance = Vector2.Distance(startPosition, endPosition);

        // ������ �̵� ���� ���
        Vector2 movement = direction * moveSpeed * Time.fixedDeltaTime;

        // ���� ��ġ���� ��ǥ ��ġ������ �Ÿ��� �̵� �Ÿ����� Ŭ ��쿡�� �̵�
        if (distance > movement.magnitude)
        {
            // Rigidbody�� ����Ͽ� ������ ��ġ�� �̵�
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // ��ǥ ��ġ�� ������ ��� ���� �̵��� ���� ���� ��ġ�� �� ��ġ ��ȯ
            Vector2 temp = startPosition;
            startPosition = endPosition;
            endPosition = temp;
        }
    }


    //�ü��� ���ų� ���� �� �θ��� �Լ�
    public void EnterRoom(Room room)
    {
        //��� �ü��� �� ������
        //�ü��� isUsing ���� true�� �ٲٱ�
        room.isUsing = true;
    }

    public void ExitRoom(Room room)
    {
        //�ü��� isUsing ���� False�� �ٲٱ�
        room.isUsing = false;
    }

}
