using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerSystem : MonoBehaviour
{
    private Rigidbody2D rb;
    public InputController inputControl;
    private CapsuleCollider2D coll;

    


    [Header("��������")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce = 20f;
    public float hurtForce;

    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]
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
        inputControl.GamePlay.Jump.started += Jump;

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
    private void FixedUpdate()
    {
        if (!isHurt & !isAttack)//���˱�����ʱ�����ƶ�
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
        //if (physicsCheck.isGround)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }//��Ծ����


    public void GetHurt(Transform attacker)//���˻��˺���
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        //�������ֵ��������뱻������֮��ķ���normalized��һ��ʹֵȡ0��1
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
