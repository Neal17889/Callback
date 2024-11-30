using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA; // 移动的起始点
    public Vector3 pointB; // 移动的结束点
    public float speed = 2f; // 移动速度

    private void Update()
    {
        float journeyLength = Vector3.Distance(pointA, pointB); // 计算起始点与结束点的距离
        float distanceCovered = Mathf.PingPong(Time.time * speed, journeyLength); // 控制平台的来回移动
        float fractionOfJourney = distanceCovered / journeyLength;

        transform.position = Vector3.Lerp(pointA, pointB, fractionOfJourney); // 平台在两点之间插值
    }
}
