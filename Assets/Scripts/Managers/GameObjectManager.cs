using Common.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class GameObjectManager : MonoSingleton<GameObjectManager>
{
    private PlayerControllerSystem Player;
    private EntityController Shadow;
    public float IntervalTimes = 2f;

    public Tilemap PastTile;
    public Tilemap PresentTile;

    internal event Action DestroyKeys;
    
    public Vector3 rebornPoint = new Vector3(1, 3, 0);
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

    public Vector3 RebornPointPosition = new Vector3(1, 3, 0);
    public bool isRebornPointInPresent = false;

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
        this.Player.PastTile = this.PastTile;
        this.Player.PresentTile = this.PresentTile;

        MainPlayerCamera.Instance.Init(DataManager.Instance.Levels[1].X, DataManager.Instance.Levels[1].Y + Config.DistanceBetweenPastAndPresent, DataManager.Instance.Levels[1].MapWidth, DataManager.Instance.Levels[1].MapHeight, playerInstance.transform);

        yield return new WaitForSeconds(IntervalTimes);

        go = Resloader.Load<GameObject>("Prefabs/Shadow");
        GameObject shadowInstance = Instantiate(go, this.RebornPoint, new Quaternion());
        this.Shadow = shadowInstance.GetComponent<EntityController>();

        isInitialized = true;   
    }

    public void TimeChange()
    {
        if (!isInitialized) return; // 确保初始化完成后再调用

        bool IsPresent = this.Player.isPresent;
        if (IsPresent)
        {
            this.Player.GoToPast();
            
        }
        else
        {
            this.Player.BackToPresent();
            
        }
    }

    internal void PlayerDie()
    {
        this.Player.Die();
        
    }

    public void PlayerReborn()
    {
        this.Player.Reborn();
        this.Player.gameObject.transform.position = this.RebornPointPosition;
        if (this.Player.isPresent != this.isRebornPointInPresent)
        {
            MainPlayerCamera.Instance.Init(this.Player.isPresent);
        }
        
        this.Player.isPresent = this.isRebornPointInPresent;
        
        MainPlayerCamera.Instance.isInFinalLevel = false;
    }

    internal void CameraMove(LevelDefine ld, bool isUpToDown, bool isRightSide)
    {
        float x = ld.X; float y = ld.Y;
        float mapWidth = ld.MapWidth;
        float mapHeight = ld.MapHeight;
        Vector3 targetPosition;
        if (isUpToDown)
        {
            if (isRightSide)
            {
                targetPosition = new Vector3(x + mapWidth - MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + mapHeight - MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
            }
            else
            {
                targetPosition = new Vector3(x + MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + mapHeight - MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
            }
        }
        else
        {
            if (isRightSide)
            {
                targetPosition = new Vector3(x + mapWidth - MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
            }
            else
            {
                targetPosition = new Vector3(x + MainPlayerCamera.Instance.wpScreenV2.x / 2f, y + MainPlayerCamera.Instance.wpScreenV2.y / 2f, -10);
            }
        }
        MainPlayerCamera.Instance.Init(x, y, mapWidth, mapHeight);
        MainPlayerCamera.Instance.CameraMove(targetPosition);
    }

    internal void TriggerDestroyKeys()
    {
        DestroyKeys?.Invoke();
        this.Player.keyCount = 0;
    }

    internal void UpdateRebornPoint(Vector3 position, bool isInPresent)
    {
        this.RebornPointPosition = position;
        this.isRebornPointInPresent = isInPresent;
    }
}
