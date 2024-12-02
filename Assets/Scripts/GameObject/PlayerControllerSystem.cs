using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

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




    [Header("��������")]
    public Vector2 inputDirection;
    public float speed;
    public float jumpForce;
    public float dashForce;
    public float dashTime;
    private float dashTimeCounter;
    public float dashCD;
    private float dashCDCounter;
    private float originalGravityScale;
    float faceDir;
    public float wallSpeed;
    
	public bool isPresent = false;
    [Header("�������")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    [Header("״̬")]

    public bool isDash;
    public bool dashAble;
    internal bool isDead = false;
    public bool isInvincible = false;
    internal Tilemap PastTile;
    internal Tilemap PresentTile;

    private void Awake()
    {
        faceDir = transform.localScale.x;
        originalGravityScale = rb.gravityScale;
        inputControl = new InputController();
        
        coll = GetComponent<CapsuleCollider2D>();
        physicsCheck = GetComponent<PhysicsCheck>();

        //������
        inputControl.GamePlay.Jump.started += Jump;
        inputControl.GamePlay.Dash.started += Dash;
        inputControl.GamePlay.TimeChange.started += TimeChange;
        inputControl.GamePlay.Sucide.started += Sucide;
        inputControl.GamePlay.Camera.started += CameraLock;
        inputControl.GamePlay.Invincibility.started += ChangeInvincibilityMode;

    }

    private void ChangeInvincibilityMode(InputAction.CallbackContext context)
    {
        this.ToggleInvincibility();
    }

    private void CameraLock(InputAction.CallbackContext context)
    {
        MainPlayerCamera.Instance.ToggleCameraMode();
    }

    private void Sucide(InputAction.CallbackContext context)
    {
        GameObjectManager.Instance.PlayerDie();
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
        if (!isDash)//���˱�����ʱ�����ƶ�
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
        //�����ƶ���ת
        
        if (inputDirection.x > 0)
        {
            transform.localScale = new Vector3(faceDir, transform.localScale.y, transform.localScale.z);
        }
        if (inputDirection.x < 0)
        {
            transform.localScale = new Vector3(-faceDir, transform.localScale.y, transform.localScale.z); 
        }
        
    }//����ƶ���غ���

    private void Jump(InputAction.CallbackContext obj)
    {
        if (physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            //SoundManager.Instance.PlaySound(SoundDefine.SFX_UI_Click);
        }
            

    }//��Ծ����

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
    public void horizonSpringDash(float horizonSpringDashTime)
    {
        if (dashAble)
        {
            dashGravityStop();

            isDash = true;
            dashTimeCounter = horizonSpringDashTime;
            dashCDCounter = dashCD + dashTime;

            if (!physicsCheck.isGround)
            {

            }
        }

    }


    private void dashGravityStop()//�ڳ��ʱ������ʧЧ
    {
        
        rb.gravityScale = 0f;
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
        if (physicsCheck.isRightWall || physicsCheck.isLeftWall)
        { 
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 10)
        {//����Trap
            if (this.isInvincible)
                return;
            Debug.Log("��ɫ��������");
            GameObjectManager.Instance.PlayerDie();
        }
        else if (collision.gameObject.layer == 12)
        {//����������

            GameObjectManager.Instance.UpdateRebornPoint(collision.gameObject.transform.position, collision.gameObject.GetComponent<RebornPoint>().isInPresent);
            
        }
        else if (collision.gameObject.layer == 7)
        {
            if (this.isInvincible)
                return;
            Debug.Log("Ӱ�ӻ�ɱ����");
            GameObjectManager.Instance.PlayerDie();
        }
        
    }


    private void TimeChange(InputAction.CallbackContext context)
    {
        GameObjectManager.Instance.TimeChange();
    }

    public void GoToPast()
    {
        Vector2 targetPosition = this.rb.position + new Vector2(0, coll.size.y / 2f) + new Vector2(0, Config.DistanceBetweenPastAndPresent);
        if (RayCaster.Instance.CanTeleport(targetPosition, this.PastTile))
        {
            rb.position += new Vector2(0, Config.DistanceBetweenPastAndPresent);
            MainPlayerCamera.Instance.Init(this.isPresent);

            this.isPresent = false;
        }
        else
        {
            Debug.Log("�㲻���ڸ�λ�ô���");
        }
    }

    public void BackToPresent()
    {
        Vector2 targetPosition = this.rb.position + new Vector2(0, coll.size.y / 2f) - new Vector2(0, Config.DistanceBetweenPastAndPresent);
        if (RayCaster.Instance.CanTeleport(targetPosition, this.PresentTile))
        {
            rb.position -= new Vector2(0, Config.DistanceBetweenPastAndPresent);
            MainPlayerCamera.Instance.Init(this.isPresent);
            this.isPresent = true;
        }
        else
        {
            Debug.Log("�㲻���ڸ�λ�ô���");
        }        
    }

    internal void Die()
    {
        Debug.Log("Player����");
        this.isDead = true;
        this.ani.SetTrigger("Player_Die");
    }

    internal void OnDeath()
    {
        GameObjectManager.Instance.PlayerReborn();
    }

    internal void Reborn()
    {
        this.isDead = false;

    }

    /// <summary>
    /// �л��޵�״̬
    /// </summary>
    private void ToggleInvincibility()
    {
        this.isInvincible = !this.isInvincible; 
        if (this.isInvincible)
        {
            Debug.Log("Player is now invincible!");
            
        }
        else
        {
            Debug.Log("Player is no longer invincible.");
            
        }
    }
}
	
