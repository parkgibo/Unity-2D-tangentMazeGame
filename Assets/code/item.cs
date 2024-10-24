using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private Transform boundary; // ������ ��ġ �ʱ�ȭ�� ���� ���
    [SerializeField] private float offset = 0.3f; // ��ġ �ʱ�ȭ �� ������

    // ������ ��ġ �ʱ�ȭ
    public void ResetPosition()
    {
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z // z���� �״�� ����
        );
    }
}
