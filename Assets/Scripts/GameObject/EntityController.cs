using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        this.rb = this.gameObject.AddComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        Vector2 position = Character.Instance.PositionInfo.Dequeue();
        this.rb.velocity = position;
    }

    private void LateUpdate()
    {
        this.transform.position = this.rb.transform.position;
    }
}
