using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // ��ȡ��ɫ�Ϳ���ǽ֮������λ��
            Vector2 direction = other.transform.position - transform.position;

            if (direction.x > 0)
            {
                
                Debug.Log("��ɫ���ұ���������ǽ");

            }
            else if (direction.x < 0)
            {
                
                Debug.Log("��ɫ�������������ǽ");
                
            }
        }
    }
}
