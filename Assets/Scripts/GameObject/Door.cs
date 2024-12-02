using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
        {//��Ҳ�
            if (other.gameObject.GetComponent<PlayerControllerSystem>().canOpenDoor)
            {
                GameObjectManager.Instance.TriggerDestroyKeys();
                Destroy(this.gameObject);
            }
            else
            {
                Debug.Log("����Ҫ��������Կ�ף����ܿ�������");
            }
        }
    }
}
