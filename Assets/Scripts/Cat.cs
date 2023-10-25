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
        //�����ϰ� ������� �ൿ�� �����Ѵ�.
        return CatState.EnterHotel;
    }


    public void Acting(CatState state)
    {
        //state�� �´� �ൿ�� �ϰԲ� 
    }

    public void AnimActing(AnimationState state)
    {
        //�� �ൿ�� �´� �ִϸ��̼��ϰԲ�
    }


    //�ü�(���� �Ǵ� ����) ������ ������, �ü��� �̵��ϴ� �Լ�
    public void Move(Room room)
    {
        //���� ���� �ִٸ�..

        //���� ���� ���� �ʴٸ�...���������ͷ� Ÿ�� �̵��ϰԲ�
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
