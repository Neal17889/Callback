using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class EntityController : MonoBehaviour
{
    

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
        
        
    }

    private void Update()
    {
        Vector2 position = Character.Instance.PositionInfo.Dequeue();
        this.transform.position = position;

    }

}
