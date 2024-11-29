using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    public int speed = 5;
    public Character character;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
        
    }

    private void LateUpdate()
    {
        this.transform.position = this.rb.transform.position;
        this.character.PositionInfo.Enqueue(this.transform.position);
    }
}
