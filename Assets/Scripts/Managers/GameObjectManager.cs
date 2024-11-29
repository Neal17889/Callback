using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectManager : MonoSingleton<GameObjectManager>
{
    private GameObject Player;
    private GameObject Shadow;
    public float IntervalTimes = 2f;
    protected override void OnStart()
    {        
        StartCoroutine(Init());
    }

    IEnumerator Init()
    {
        this.Player = Resloader.Load<GameObject>("Prefabs/Player");        
        Instantiate(this.Player);
        yield return new WaitForSeconds(IntervalTimes);
        this.Shadow = Resloader.Load<GameObject>("Prefabs/Shadow");
        Instantiate(this.Shadow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
