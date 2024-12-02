using Common.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointObject : MonoBehaviour
{
    public int ID;
    public bool isRightSide;
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
            // 获取角色和空气墙之间的相对位置
            Vector2 direction = other.transform.position - transform.position;
            LevelDefine ld;
            if (direction.y > 0)
            {
                
                Debug.Log("角色从上方碰到空气墙");
                ld = DataManager.Instance.Levels[ID + 1];
                GameObjectManager.Instance.CameraMove(ld, true, this.isRightSide);
            }
            else if (direction.y < 0)
            {
                
                Debug.Log("角色从下方碰到空气墙");
                ld = DataManager.Instance.Levels[ID];
                GameObjectManager.Instance.CameraMove(ld, false, this.isRightSide);
            }
        }
    }
}
