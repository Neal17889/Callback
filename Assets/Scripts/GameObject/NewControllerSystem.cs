using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class NewlControllerSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController inputControl;
    private CapsuleCollider2D coll;




    [Header("��������")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float hurtForce;
    public float lateTime = 2f;
    public float timeCounter;
    Vector3 diff = new Vector3(10, 10, 0);

    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]
    public bool timeLine = true;//ΪTrueʱ�����ڣ�ΪFalseʱ�ڹ�ȥ
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public int combo;

    private void Awake()
    {
        inputControl = new InputController();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        //��Ծ������

        //����������
        //inputControl.Gameplay.Attack.started += PlayerAttack;
    }


    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //CheckState();
    }
    //private void FixedUpdate()
    //{
    //    if (!isHurt & !isAttack)//���˱�����ʱ�����ƶ�
    //        Move();
    //}

    //private void LateUpdate()
    //{
    //    Character.Instance.PositionInfo.Enqueue(this.transform.position);
    //}



    //public void Move()
    //{
    //    rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
    //    //�����ƶ���ת
    //    int faceDir = (int)transform.localScale.x;
    //    if (inputDirection.x > 0)
    //    {
    //        faceDir = 1;
    //    }
    //    if (inputDirection.x < 0)
    //    {
    //        faceDir = -1;
    //    }
    //    transform.localScale = new Vector3(faceDir, 1, 1);
    //}//����ƶ���غ���

    //private void Jump(InputAction.CallbackContext obj)
    //{
    //    //if (physicsCheck.isGround)
    //    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }//��Ծ����
    //private void TimeChange(InputAction.CallbackContext obj)
    //{
    //    timeLine = !timeLine;
    //    timeCounter = lateTime;
    //    if (!timeLine)
    //    {
    //        this.transform.position = this.transform.position + diff;
    //    }
    //    else
    //    {
    //        this.transform.position = this.transform.position - diff;
    //    }


    //}


    //public void GetHurt(Transform attacker)//���˻��˺���
    //{
    //    isHurt = true;
    //    rb.velocity = Vector2.zero;
    //    Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
    //    //�������ֵ��������뱻������֮��ķ���normalized��һ��ʹֵȡ0��1
    //    rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    //}

    //public void PlayerDead()
    //{
    //    isDead = true;
    //    inputControl.GamePlay.Disable();
    //}

    //private void CheckState()
    //{
    //    coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    //}