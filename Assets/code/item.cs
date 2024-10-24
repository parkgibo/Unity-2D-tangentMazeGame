using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    [SerializeField] private Transform boundary; // 아이템 위치 초기화를 위한 경계
    [SerializeField] private float offset = 0.3f; // 위치 초기화 시 오프셋

    // 아이템 위치 초기화
    public void ResetPosition()
    {
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z // z축은 그대로 유지
        );
    }
}
