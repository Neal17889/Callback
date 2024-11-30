using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("״̬")]
    public bool isGround;
    [Header("������")]
    //public float checkRadius;
    public float checkBoxAngle;
    public Vector2 bottomOffset;
    public Vector3 checkCube;
    public LayerMask groundLayer;
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        isGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, (Vector2)checkCube, checkBoxAngle, groundLayer);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, checkCube);
    }
}
