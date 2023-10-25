using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    public float likeability;
    public float speed;
    public Animator animator;

    public CatState myState;
    public AnimationState myAnimationState;

    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        myState = CatState.EnterHotel;
        myAnimationState = AnimationState.Walk;
    }

    public CatState DecideAction()
    {
        //랜덤하게 고양이의 행동을 결정한다.
        return CatState.EnterHotel;
    }


    public void Acting(CatState state)
    {
        //state에 맞는 행동을 하게끔 
    }

    public void AnimActing(AnimationState state)
    {
        //각 행동에 맞는 애니메이션하게끔
    }


    //시설(객실 또는 상점) 정보를 얻으면, 시설로 이동하는 함수
    public void Move(Room room)
    {
        //같은 층에 있다면..

        //같은 층에 있지 않다면...엘레베이터로 타고 이동하게끔
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
