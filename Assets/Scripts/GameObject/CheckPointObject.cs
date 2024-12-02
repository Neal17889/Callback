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
            // ��ȡ��ɫ�Ϳ���ǽ֮������λ��
            Vector2 direction = other.transform.position - transform.position;
            LevelDefine ld;
            if (direction.y > 0)
            {
                
                Debug.Log("��ɫ���Ϸ���������ǽ");
                ld = DataManager.Instance.Levels[ID + 1];
                GameObjectManager.Instance.CameraMove(ld, true, this.isRightSide);
            }
            else if (direction.y < 0)
            {
                
                Debug.Log("��ɫ���·���������ǽ");
                ld = DataManager.Instance.Levels[ID];
                GameObjectManager.Instance.CameraMove(ld, false, this.isRightSide);
            }
        }
    }
}