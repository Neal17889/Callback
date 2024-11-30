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
    public Animator ani;

    public int keyCount = 0;
    public bool canOpenDoor
    {
        get
        {
            return this.keyCount >= 3;
        }
    }


    [Header("基本参数")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashTime;
    private float dashTimeCounter;
    public float dashCD;
    private float dashCDCounter;
    private float originalGravityScale;
    public float wallSpeed;
    
	private bool isPresent = true;
    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]

    public bool isDash;
    public bool dashAble;

    

    private void Awake()
    {
        
        inputControl = new InputController();
        
        coll = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //按键绑定
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
        
        dashCheck();
        WallCheck();
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();
        //CheckState();
    }
    private void FixedUpdate()
    {
        if (!isDash)//受伤被击退时禁用移动
            Move();
        
    }

    private void LateUpdate()
    {
        
        this.transform.position = this.rb.transform.position;
        Character.Instance.PositionInfo.Enqueue(this.transform.position);
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
        if (dashAble)
        {
            dashGravityStop();
            dashAble = false;
            isDash = true;
            dashTimeCounter = dashTime;
            dashCDCounter = dashCD+dashTime;
            
            rb.AddForce(new Vector2(inputDirection.x * dashForce, inputDirection.y*dashForce), ForceMode2D.Impulse);
            if (!physicsCheck.isGround)
            {
                dashAble = false;
            }
        }

    }


    private void dashGravityStop()//在冲刺时令重力失效
    {
        originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
    }

    private void dashCheck()
    {
        if (isDash)
        {
            dashTimeCounter -= Time.deltaTime;
            if (dashTimeCounter <= 0)
            {
                isDash = false;
                rb.gravityScale = originalGravityScale;
                rb.velocity = new Vector2(0,0);
            }
        }
        if (physicsCheck.isGround&&dashCDCounter<=0)
        {
            dashAble= true;
        }
        if(dashCDCounter > 0) 
        {
            dashCDCounter-=Time.deltaTime;
        }
    }
    private void WallCheck()
    {
        if (physicsCheck.isRightWall && inputDirection.x > 0&&rb.velocity.y<=0)
        {
            rb.velocity = new Vector2(rb.velocity.x,wallSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {//碰到Trap
            Debug.Log("角色碰到陷阱");
            GameObjectManager.Instance.PlayerDie();
        }
        else if (collision.gameObject.layer == 12)
        {//碰到重生点
            GameObjectManager.Instance.RebornPoint = collision.gameObject.transform.position;
        }
        
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

    internal void Die()
    {
        Debug.Log("Player死亡");
        this.ani.SetTrigger("Player_Die");
    }

    internal void OnDeath()
    {
        GameObjectManager.Instance.PlayerReborn();
    }

    internal void Reborn()
    {
        //this.gameObject.transform.localScale = Vector3.one;

    }
}
	
