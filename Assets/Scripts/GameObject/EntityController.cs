using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {

    }



    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 v = Character.Instance.PositionInfo.Dequeue();
        this.rb.velocity = v;
 
    }

    private void LateUpdate()
    {
        this.transform.position = this.rb.transform.position;
    }

    public void GoToPast()
    {
        rb.position += new Vector2(0, 100);
    }

    public void BackToPresent()
    {
        rb.position -= new Vector2(0, 100);
    }
}
