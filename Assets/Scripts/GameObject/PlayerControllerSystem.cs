using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController inputControl;
    private CapsuleCollider2D coll;

    


    [Header("基本参数")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce = 20f;
    public float hurtForce;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("状态")]
    public bool isHurt;
    public bool isDead;
    public bool isAttack;
    public int combo;

    private void Awake()
    {
        inputControl = new InputController();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();

        //跳跃按键绑定
        inputControl.GamePlay.Jump.started += Jump;

        //攻击按键绑定
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
    private void FixedUpdate()
    {
        if (!isHurt & !isAttack)//受伤被击退时禁用移动
            Move();
        Character.Instance.PositionInfo.Enqueue(this.rb.velocity);
    }

    private void LateUpdate()
    {
        this.transform.position = this.rb.transform.position;
    }



    public void Move()
    {
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
        //if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }//跳跃函数


    public void GetHurt(Transform attacker)//受伤击退函数
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        //用坐标差值求出攻击与被攻击者之间的方向，normalized归一化使值取0或1
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }

    //private void CheckState()
    //{
    //    coll.sharedMaterial = physicsCheck.isGround ? normal : wall;
    //}

}
