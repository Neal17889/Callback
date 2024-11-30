using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSystem : MonoBehaviour
{
    public Rigidbody2D rb;
    public InputController inputControl;
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;
    

    


    [Header("��������")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashTime;
    public float dashTimeCounter;
    private bool isPresent = true;

    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]

    public bool isDash;

    private void Awake()
    {
        
        inputControl = new InputController();
        
        coll = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //������
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Dash.started += Dash;
        inputControl.GamePlay.TimeChange.started += TimeChange;

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
        if (isDash)
        {
            dashTimeCounter-=Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isDash = false;
            }
        }
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //CheckState();
    }
    private void FixedUpdate()
    {
        if (!isDash)//���˱�����ʱ�����ƶ�
            Move();
        Character.Instance.PositionInfo.Enqueue(this.rb.velocity);
    }

    private void LateUpdate()
    {
        this.transform.position = this.rb.transform.position;
    }

    



    public void Move()
    {
        //rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
        //�����ƶ���ת
        int faceDir = (int)transform.localScale.x;
        if (inputDirection.x > 0)
        {
            faceDir = 1;
        }
        if (inputDirection.x < 0)
        {
            faceDir = -1;
        }
        transform.localScale = new Vector3(faceDir, 1, 1);
    }//����ƶ���غ���

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }//��Ծ����

    private void Dash(InputAction.CallbackContext obj)
    {
        isDash= true;
        dashTimeCounter = dashTime;
        rb.AddForce(new Vector2(inputDirection.x*dashForce,0), ForceMode2D.Impulse);
    }

    private void TimeChange(InputAction.CallbackContext context)
    {
        GameObjectManager.Instance.TimeChange(this.isPresent);
    }

    public void GoToPast()
    {
        rb.position += new Vector2(0, 100);
        this.isPresent = false;
    }

    public void BackToPresent()
    {
        rb.position -= new Vector2(0, 100);
        this.isPresent = true;
    }

    

    


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

    }
