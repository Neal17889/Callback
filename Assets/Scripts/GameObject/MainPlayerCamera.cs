using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCamera : MonoSingleton<MainPlayerCamera>
{
    public Camera camera;
    public Transform playerTf;

    public GameObject player;
    private float x, y;
    private float mapWidth, mapHeight;
    public float smoothSpeed = 1f;

    public bool isLerp = false;
    public Vector2 wpScreenV2 = Vector2.zero;
    protected override void OnStart()
    {
        this.camera = gameObject.GetComponent<Camera>();
        // 计算世界坐标与屏幕坐标的转换
        Vector2 pxV2 = Camera.main.WorldToScreenPoint(new Vector2(0, 0));
        Vector2 screenV2 = new Vector2(Screen.width + pxV2.x, Screen.height + pxV2.y);
        this.wpScreenV2 = Camera.main.ScreenToWorldPoint(screenV2);
    }

    public void Init(float x, float y, float mapWidth, float mapHeight, Transform target = null)
    {
        if (target != null) 
            playerTf = target;

        this.x = x;
        this.y = y;
        this.mapWidth = mapWidth;
        this.mapHeight = mapHeight;
    }

    public void Init(bool isPresent)
    {
        if (isPresent)
            this.y += 100;
        else
            this.y -= 100;
    }


    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (playerTf == null || isLerp)
            return;

        // 获取玩家位置
        Vector3 targetPosition = playerTf.position;

        // 通过插值平滑摄像机位置

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

        

        // 计算摄像机的新位置，限制在范围内
        Vector3 cameraPos = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);

        if (wpScreenV2.x > this.mapWidth)
        {
            cameraPos.x = this.x + this.mapWidth / 2;
        }
        else
        {
            float minX = this.x + wpScreenV2.x / 2f;
            float maxX = this.x + this.mapWidth - wpScreenV2.x / 2f;
            cameraPos.x = Mathf.Clamp(cameraPos.x, minX, maxX);
        }

        if (wpScreenV2.y > this.mapHeight)
        {
            cameraPos.y = this.y + this.mapHeight / 2;
        }
        else
        {
            float minY = this.y + wpScreenV2.y / 2f;
            float maxY = this.y + this.mapHeight - wpScreenV2.y / 2f;
            cameraPos.y = Mathf.Clamp(cameraPos.y, minY, maxY);
        }


        // 更新摄像机位置
        this.transform.position = cameraPos;
    }

    public void CameraMove(Vector3 targetPosition)
    {
        this.isLerp = true;
        StartCoroutine(CameraLerp(targetPosition));
    }

    IEnumerator CameraLerp(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveDuration = 1f;

        // 当前NPC位置设置为起始位置
        Vector3 startPosition = this.transform.position;

        // 在指定时间内平滑移动NPC
        while (elapsedTime < moveDuration)
        {
            this.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保移动结束时位置是目标位置
        this.transform.position = targetPosition;
        this.isLerp = false;
    }

}
