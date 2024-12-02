using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour

{
    public float springForce;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Rigidbody2D rb=collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(transform.up * springForce, ForceMode2D.Impulse);
            }
        }

    }
}
