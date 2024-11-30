using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController inputControl;
    private CapsuleCollider2D coll;
    private PhysicsCheck physicsCheck;

    


    [Header("基本参数")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashTime;
    public float dashTimeCounter;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]

    public bool isDash;

    private void Awake()
    {
        inputControl = new InputController();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //按键绑定
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Dash.started += Dash;

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
        if (!isDash)//受伤被击退时禁用移动
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
        //反向移动翻转
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
    }//玩家移动相关函数

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }//跳跃函数

    private void Dash(InputAction.CallbackContext obj)
    {
        isDash= true;
        dashTimeCounter = dashTime;
        rb.AddForce(new Vector2(inputDirection.x*dashForce,0), ForceMode2D.Impulse); 
    }




        //public void GetHurt(Transform attacker)//受伤击退函数
        //{
        //    isHurt = true;
        //    rb.velocity = Vector2.zero;
        //    Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        //    //用坐标差值求出攻击与被攻击者之间的方向，normalized归一化使值取0或1
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
