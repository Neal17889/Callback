using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Vector3 pointA; // �ƶ�����ʼ��
    public Vector3 pointB; // �ƶ��Ľ�����
    public float speed = 2f; // �ƶ��ٶ�

    private void Update()
    {
        float journeyLength = Vector3.Distance(pointA, pointB); // ������ʼ���������ľ���
        float distanceCovered = Mathf.PingPong(Time.time * speed, journeyLength); // ����ƽ̨�������ƶ�
        float fractionOfJourney = distanceCovered / journeyLength;

        transform.position = Vector3.Lerp(pointA, pointB, fractionOfJourney); // ƽ̨������֮���ֵ
    }
}
