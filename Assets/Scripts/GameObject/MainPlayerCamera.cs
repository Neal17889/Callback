using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainPlayerCamera : MonoSingleton<MainPlayerCamera>
{
    public bool TestMode = true;
    public Camera camera;
    public Transform playerTf;

    public GameObject player;
    internal float x, y;
    internal float mapWidth, mapHeight;
    internal float smoothSpeed = 1f;

    internal bool isLerp = false;
    internal Vector2 wpScreenV2 = Vector2.zero;
    internal bool isInFinalLevel = false;
    internal bool isGameWin = false;


    public float moveDuration;
    public Vector3 finalTargetPosition;
    public float finalOrthographicSize;

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
            this.y += Config.DistanceBetweenPastAndPresent;
        else
            this.y -= Config.DistanceBetweenPastAndPresent;
    }


    private void Update()
    {

    }

    private void LateUpdate()
    {
        if (playerTf == null || isLerp)
            return;
        
            if (!isGameWin)
            {
                if (!this.isInFinalLevel)
                {
                    MoveSolution1();
                }
                else
                {
                    MoveSolution2();
                }
            }
            else
            {
                return;
            }
        
        
        
        
    }

    private void MoveSolution2()
    {
        
        float y = -this.wpScreenV2.y / 4f;
        Vector3 offset = new Vector3(0, y, -10);
        this.gameObject.transform.position = this.playerTf.transform.position + offset;
    }

    private void MoveSolution1()
    {
        if (TestMode)
        {
            this.gameObject.transform.position = playerTf.transform.position + new Vector3(0, 0, -10);
            return;
        }
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

        Vector3 startPosition = this.transform.position;

        while (elapsedTime < moveDuration)
        {
            this.transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        this.transform.position = targetPosition;
        this.isLerp = false;
    }

    internal void VictoryScreen()
    {
        StartCoroutine(CameraOfVictory());
    }

    IEnumerator CameraOfVictory()
    {
        yield return new WaitForSeconds(2f);

        this.gameObject.transform.position -= new Vector3(0, Config.DistanceBetweenPastAndPresent, 0);

        float elapsedTime = 0f;
        float moveDuration = this.moveDuration;

        Vector3 startPosition = this.transform.position;
        float startSize = Camera.main.orthographicSize;

        while (elapsedTime < moveDuration)
        {
            this.transform.position = Vector3.Lerp(startPosition, this.finalTargetPosition, elapsedTime / moveDuration);
            Camera.main.orthographicSize = Mathf.Lerp(startSize, this.finalOrthographicSize, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        this.transform.position = this.finalTargetPosition;
        Camera.main.orthographicSize = this.finalOrthographicSize;
    }

    internal void ToggleCameraMode()
    {
        this.TestMode = !this.TestMode;
    }
}
