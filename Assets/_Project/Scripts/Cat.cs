using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat : MonoBehaviour
{
    public float likeability;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public CatState myState;
    public AnimationState myAnimationState;
    public float moveSpeed = 50f;
    public Text catTxt;
    
    private Rigidbody2D rb;
    private Room myRoom;
    private Room EnterHotel;
    private GameObject[] Restaurant;
    public int UsingHotelNum;
    private Room UsedRoom;
    private Room RestaurantRoom;
    private int UsingRest = 0;
    bool isArrive = false;
    bool isUsingRestaurant = false;
    bool movingToRestaurant = false;

    float elapsedTime = 0f;
    float waitTime = 5f;
    
    private string[] CatTxt = { "��!", "�������..", "����~", "���ұ�?", "����ִ�!", "�񸻶�", "������~", "������!" };

    public void Start()
    {
        moveSpeed = 70f;
        animator = gameObject.GetComponent<Animator>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        myState = CatState.EnterHotel;
        myAnimationState = AnimationState.Walk;
        rb = GetComponent<Rigidbody2D>();
        EnterHotel = GameObject.FindGameObjectWithTag("EnterHotel").GetComponent<Room>();
        UsingHotelNum = 0;
        isArrive = false;
    }

    public void Update()
    {
        Restaurant = GameObject.FindGameObjectsWithTag("Restaurant");
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

            //Debug.Log(distance);
            if (distance <= 2.5f)
            {
                myState = CatState.UsingRoom;
                isArrive = false;
            }
        }
        else if (myState == CatState.UsingRoom)
        {
            if(UsingHotelNum >= 10)
            {
                myState = CatState.LeaveHotel;
                isArrive = false;
            }
        }

    }

    public void UsingRestaurant()
    {
        if (Restaurant.Length > 0 && RestaurantRoom == null)
        {
            RestaurantRoom = Restaurant[Random.Range(0, Restaurant.Length)].GetComponentInParent<Room>();
        }
        if (RestaurantRoom != null && !RestaurantRoom.isUsing && UsingHotelNum < 6 && UsingHotelNum > 1 && UsingRest <= 0 && myState != CatState.LeaveHotel || movingToRestaurant)
        {

            var distance = Vector2.Distance(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + RestaurantRoom.roomNumber * 100,
                GuestManager._this.HotelEnterPosition.transform.position.y + RestaurantRoom.roomfloor * 165));

            if (distance > 2.5f)
            {
                movingToRestaurant = true;
                isUsingRestaurant = false;
                RestaurantRoom.isUsing = true;
                Move(myRoom, RestaurantRoom);

            }
            else
            {
                UsedRoom = RestaurantRoom;
                animator.SetBool("Walk", false);
                animator.SetBool("Eat", true);
                if (isUsingRestaurant == false)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 50);
                    isUsingRestaurant = true;
                    catTxt.text = "���־�!!";
                    movingToRestaurant = false;
                }
                if (isArrive) isArrive = !isArrive;
            }
        }

        if (UsedRoom != null && UsingRest >= 2)
        {
            if (isUsingRestaurant == true)
            {
                gameObject.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 50);
                isUsingRestaurant = false;
                RestaurantRoom.isUsing = false;
            }
            animator.SetBool("Walk", true);
            animator.SetBool("Eat", false);
            Move(RestaurantRoom, myRoom);
        }

    }

    public void UsingHotel()
    {
        UsingRestaurant();

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= waitTime)
        {
            if (isUsingRestaurant == true)
            {
                UsingRest += 1;
                catTxt.text = "���־�!!";
            }
            else
            {
                catTxt.text = CatTxt[Random.Range(0, CatTxt.Length)];
            }
            UsingHotelNum += 1;
            elapsedTime = 0f;
        }
    }

    public void DecideMyRoom()
    {
        if (RoomManager._this.numGuestRoom() <= 0)
        {
            GuideManager.instance.SetGuideMessage("�մ��� �ӹ� ���� �����! �������� �Խ�Ʈ���� �������ּ���.");
            GuideManager.instance.OpenGuideBox();
        }
        else
        {
            GuideManager.instance.HideGuideBox();
            foreach (var room in RoomManager._this.GuestRooms)
            {
              if ( room.GetComponentInParent<Room>().CatGuest == null)
                {
                    room.GetComponentInParent<Room>().CatGuest = gameObject;
                    myRoom = room.GetComponentInParent<Room>();
                  //  Debug.Log("���� �����Ǿ����ϴ�.");
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
            animator.SetBool("Walk", true);
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
            Debug.Log("���� ȹ���߽��ϴ�.");
        }
        else if(myState == CatState.LeaveHotel)
        {
            animator.SetBool("Eat", false);
            if(UsedRoom != null)
            {
                UsedRoom.isUsing = false;
            }

            catTxt.text = "��ſ���!";

            var distance = Vector2.Distance(gameObject.transform.position, GuestManager._this.HotelEnterPosition.transform.position);

            if (distance <= 3f)
            {
                isArrive = true;
            }

            if (isArrive)
            {
                animator.SetBool("Walk", true);
                MovePos(gameObject.transform.position, GuestManager._this.CatSpawnPosition.transform.position);
                var ExitDistance = Vector2.Distance(gameObject.transform.position, GuestManager._this.CatSpawnPosition.transform.position);
              //  Debug.Log("ExitDistance: " + ExitDistance);
                
                if ( ExitDistance < 1.5f)
                {
                    Destroy(gameObject);
                    GuestManager._this.CurrentGuestNum -= 1;
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
        //ù �������� ��
        if (StartRoom == EnterHotel)
        {
            var distanceY = Vector2.Distance(
           new Vector2(0, gameObject.transform.position.y),
           new Vector2(0, GuestManager._this.HotelEnterPosition.transform.position.y + (targetRoom.roomfloor - StartRoom.roomfloor) * 165f));

            var distanceX = Vector2.Distance(
                new Vector2(gameObject.transform.position.x, 0),
                new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100f, 0));

            //���� ���� ���� �ʴٸ�...���������ͷ� Ÿ�� �̵��ϰԲ�
            if (targetRoom.roomfloor != StartRoom.roomfloor && distanceY > 2f)
            {
                animator.SetBool("Walk", false);
                MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, GuestManager._this.HotelEnterPosition.transform.position.y + (targetRoom.roomfloor) * 165));
            }
            else if (distanceX > 2.5f)
            {
                animator.SetBool("Walk", true);
                MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber) * 100, gameObject.transform.position.y));
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else if (StartRoom.roomfloor == 0 && targetRoom == EnterHotel)
        {
                var distanceX = Vector2.Distance(
                    new Vector2(gameObject.transform.position.x, 0),
                    new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100f, 0));
                if (distanceX > 2.5f)
                {
                    animator.SetBool("Walk", true);
                    MovePos(gameObject.transform.position, new Vector2(gameObject.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100, gameObject.transform.position.y));
                }
                else
                {
                    animator.SetBool("Walk", false);
                }
            
        }
        else if (StartRoom.roomfloor != 0 && targetRoom == EnterHotel)
        {
            var distanceY = Vector2.Distance(
                new Vector2(0, gameObject.transform.position.y),
                new Vector2(0, GuestManager._this.HotelEnterPosition.transform.position.y));

            var distanceX = Vector2.Distance(
                new Vector2(gameObject.transform.position.x, 0),
                new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, 0));

            if (distanceX > 2.5f)
            {
                animator.SetBool("Walk", true);
                MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber - StartRoom.roomNumber) * 100, gameObject.transform.position.y));
            }
            else if (distanceY > 2.5f)
            {
                animator.SetBool("Walk", false);
                MovePos(gameObject.transform.position, new Vector2(gameObject.transform.position.x, GuestManager._this.HotelEnterPosition.transform.position.y + (+targetRoom.roomfloor - +StartRoom.roomfloor) * 165));
            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else if (StartRoom != EnterHotel && targetRoom != EnterHotel)
        {
            //���� ���� ���� ��
            if(StartRoom.roomfloor == targetRoom.roomfloor)
            {
                var distanceX = Vector2.Distance(
    new Vector2(gameObject.transform.position.x, 0),
    new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber) * 100f, 0));
                Debug.Log(distanceX);

                if (distanceX > 2.5f)
                {
                    animator.SetBool("Walk", true);
                    MovePos(gameObject.transform.position, 
                        new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber) * 100f , gameObject.transform.position.y));
                }
                else
                {
                    animator.SetBool("Walk", false);
                }
            }
            //�ٸ� ���� ���� ��
            else
            {
                var distanceX = Vector2.Distance(
                new Vector2(gameObject.transform.position.x, 0),
                new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, 0));

                var distanceY = Vector2.Distance(
                    new Vector2(0, gameObject.transform.position.y),
                    new Vector2(0, GuestManager._this.HotelEnterPosition.transform.position.y + (targetRoom.roomfloor) * 165));

                var distanceX2 = Vector2.Distance(
                     new Vector2(gameObject.transform.position.x, 0),
                    new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber) * 100, 0));

                if (distanceX > 2.5f && !isArrive)
                {
                    animator.SetBool("Walk", true);
                    MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x , gameObject.transform.position.y));
                }
                else
                {
                    isArrive = true;
                }

                if (distanceY > 2.5f && isArrive)
                {
                    
                    animator.SetBool("Walk", false);
                    MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x, GuestManager._this.HotelEnterPosition.transform.position.y+ targetRoom.roomfloor * 165 ));

                }
                else if (distanceX2 > 2.5f && isArrive)
                {
                    animator.SetBool("Walk", true);
                    MovePos(gameObject.transform.position, new Vector2(GuestManager._this.HotelEnterPosition.transform.position.x + (targetRoom.roomNumber) * 100, gameObject.transform.position.y));
                }
                else if (distanceX2 <= 2.5f)
                {
                    animator.SetBool("Walk", false);
                }
            }
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

        if(direction.x > 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
        // ���� ��ġ���� ��ǥ ��ġ������ �Ÿ��� �̵� �Ÿ����� Ŭ ��쿡�� �̵�
        if (distance > movement.magnitude)
        {
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            animator.SetBool("Walk", false);
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
