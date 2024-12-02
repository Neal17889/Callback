using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            MainPlayerCamera.Instance.isInFinalLevel = true;
        }
    }
}
