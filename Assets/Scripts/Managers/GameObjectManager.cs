using Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectManager : MonoSingleton<GameObjectManager>
{
    private PlayerControllerSystem Player;
    private EntityController Shadow;
    public float IntervalTimes = 2f;

    private Vector3 rebornPoint = new Vector3(1, 3, 0);
    public Vector3 RebornPoint
    {
        get
        {
            return this.rebornPoint;
        }
        set
        {
            this.rebornPoint = value;
        }
    }

    protected override void OnStart()
    {
        
        StartCoroutine(Init());
    }

    private bool isInitialized = false;

    IEnumerator Init()
    {
        GameObject go = Resloader.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = Instantiate(go, this.RebornPoint, new Quaternion());
        this.Player = playerInstance.GetComponent<PlayerControllerSystem>();

        MainPlayerCamera.Instance.Init(0, 0, 36, 36, playerInstance.transform);

        yield return new WaitForSeconds(IntervalTimes);

        go = Resloader.Load<GameObject>("Prefabs/Shadow");
        GameObject shadowInstance = Instantiate(go, this.RebornPoint, new Quaternion());
        this.Shadow = shadowInstance.GetComponent<EntityController>();

        isInitialized = true;
    }

    public void TimeChange(bool IsPresent)
    {
        if (!isInitialized) return; // 确保初始化完成后再调用

        if (IsPresent)
        {
            this.Player.GoToPast();
            MainPlayerCamera.Instance.Init(IsPresent);
        }
        else
        {
            this.Player.BackToPresent();
            MainPlayerCamera.Instance.Init(IsPresent);
        }
    }

    internal void PlayerDie()
    {
        this.Player.Die();
    }

    public void PlayerReborn()
    {
        //this.Player.Reborn();
        this.Player.gameObject.transform.position = this.RebornPoint;
    }

    internal void CameraMove(LevelDefine ld, bool isLeftToRight)
    {
        float x = ld.X; float y = ld.Y;
        float mapWidth = ld.MapWidth;
        float mapHeight = ld.MapHeight;
        Vector3 targetPosition;
        if (isLeftToRight)
        {
            targetPosition = new Vector3(x + MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
        }
        else
        {
            targetPosition = new Vector3(x + mapWidth - MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
        }
        MainPlayerCamera.Instance.Init(x, y, mapWidth, mapHeight);
        MainPlayerCamera.Instance.CameraMove(targetPosition);
    }
}
