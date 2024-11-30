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


    
    protected override void OnStart()
    {        
        StartCoroutine(Init());
    }

    private bool isInitialized = false;

    IEnumerator Init()
    {
        GameObject go = Resloader.Load<GameObject>("Prefabs/Player");
        GameObject playerInstance = Instantiate(go, new Vector3(1, 3, 0), new Quaternion());
        this.Player = playerInstance.GetComponent<PlayerControllerSystem>();

        MainPlayerCamera.Instance.Init(playerInstance.transform, 0, 0, 36, 36);

        yield return new WaitForSeconds(IntervalTimes);

        go = Resloader.Load<GameObject>("Prefabs/Shadow");
        GameObject shadowInstance = Instantiate(go, new Vector3(1, 3, 0), new Quaternion());
        this.Shadow = shadowInstance.GetComponent<EntityController>();

        isInitialized = true;
    }

    public void TimeChange(bool IsPresent)
    {
        if (!isInitialized) return; // 确保初始化完成后再调用

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
        this.Player = null;
    }


}
