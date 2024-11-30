using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("×´Ì¬")]
    public bool isGround;
    public bool isLeftWall;
    public bool isRightWall;
    [Header("¼ì²â²ÎÊý")]
    //public float checkRadius;
    public float checkBoxAngle;
    public Vector2 bottomOffset;
    public Vector3 checkCube;
    public Vector2 leftWallBottomOffset;
    public Vector3 leftWallCheckCube;
    public Vector2 rightWallBottomOffset;
    public Vector3 rightWallCheckCube;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    private void Update()
    {
        Check();
    }
    private void Check()
    {
        isGround = Physics2D.OverlapBox((Vector2)transform.position + bottomOffset, (Vector2)checkCube, checkBoxAngle, groundLayer);
        isLeftWall = Physics2D.OverlapBox((Vector2)transform.position + leftWallBottomOffset, (Vector2)leftWallCheckCube, checkBoxAngle, wallLayer) ;
        isRightWall = Physics2D.OverlapBox((Vector2)transform.position + rightWallBottomOffset, (Vector2)rightWallCheckCube, checkBoxAngle, wallLayer);
    }

    private void OnDrawGizmosSelected()
    {
        //Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset,checkRadius);
        Gizmos.DrawWireCube((Vector2)transform.position + bottomOffset, checkCube);
        Gizmos.DrawWireCube((Vector2)transform.position + leftWallBottomOffset, leftWallCheckCube);
        Gizmos.DrawWireCube((Vector2)transform.position + rightWallBottomOffset, rightWallCheckCube);
    }
}
 