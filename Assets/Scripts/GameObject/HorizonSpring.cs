using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizonSpring : MonoBehaviour

{
    public float horizonSpringForce;
    public bool horizonSpringDirection;
    public float horizonSpringDashTime;

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
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            PlayerControllerSystem pc=collision.GetComponent<PlayerControllerSystem>();
            if (rb != null)
            {
                pc.horizonSpringDash(horizonSpringDashTime);
                if (horizonSpringDirection == true)
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector3(horizonSpringForce,0,0), ForceMode2D.Impulse);
                }
                if (horizonSpringDirection == false)
                {
                    rb.velocity = Vector3.zero;
                    rb.AddForce(new Vector3(-horizonSpringForce, 0, 0), ForceMode2D.Impulse);
                }

            }
        }

    }
}

