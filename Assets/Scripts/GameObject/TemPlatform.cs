using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TemPlatform : MonoBehaviour
{
    public TilemapCollider2D coll;
    public TilemapRenderer tileRenderer;


    public float existTime;

    public float disappearTime;

    void Start()
    {
        
    }
    private void Awake()
    {
        coll = GetComponent <TilemapCollider2D>();
        tileRenderer = GetComponent <TilemapRenderer>();
    }

    // Update is called once per frame
    void Update()
    {


    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            Disappear();
        }
    }
    public void Disappear()
    {
        // ����Э�̣��ӳٵ��ú���
        StartCoroutine(DelayedDisappearTime());
    }

    
    private IEnumerator DelayedDisappearTime()
    {
        yield return new WaitForSeconds(existTime); // �ȴ�
        DisappearTime(); // ����Ŀ�꺯��
    }

    // Ŀ�꺯��
    private void DisappearTime()
    {
        coll.enabled=false;
        tileRenderer.enabled=false;
        
        StartCoroutine(DelayedAppearTime());
    }

    private IEnumerator DelayedAppearTime()
    {
        yield return new WaitForSeconds(disappearTime); // �ȴ�
        AppearTime();
    }

    private void AppearTime()
    {
        coll.enabled = true;
        tileRenderer.enabled=true;
    }
}
