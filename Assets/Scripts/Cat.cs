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
    
    private string[] CatTxt = { "��!", "�������..", "����~", "���ұ�?", "����ִ�!", "�񸻶�", "������~", "������!" };

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
            catTxt.text = "�����...";
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
            catTxt.text = "��������!";
        }
        else if(myState == CatState.LeaveHotel)
        {
            catTxt.text = "��ſ���!";

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
        //�� �ൿ�� �´� �ִϸ��̼��ϰԲ�
    }


    //�ü�(���� �Ǵ� ����) ������ ������, �ü��� �̵��ϴ� �Լ�
    public void Move(Room StartRoom, Room targetRoom)
    {
        var distanceY = Vector2.Distance(
            new Vector2(0,gameObject.transform.position.y),
            new Vector2(0,GuestManager._this.HotelEnterPosition.transform.position.y + ( targetRoom.roomfloor - StartRoom.roomfloor) * 165f));

        var distanceX = Vector2.Distance(
            new Vector2(gameObject.transform.position.x, 0),
            new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + ( targetRoom.roomNumber - StartRoom.roomNumber) * 100f, 0));

        //���� ���� ���� �ʴٸ�...���������ͷ� Ÿ�� �̵��ϰԲ�
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
