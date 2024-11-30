using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerCamera : MonoSingleton<MainPlayerCamera>
{
    public Camera camera;
    public Transform viewPoint;

    public GameObject player;

    protected override void OnStart()
    {
        this.camera = gameObject.GetComponent<Camera>();
    }


    private void Update()
    {

    }

    private void LateUpdate()
    {
        
        if (player == null)
            return;

        this.transform.position = player.transform.position + new Vector3(0,0,-10);
        
    }
}
