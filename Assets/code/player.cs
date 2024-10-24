using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // 플레이어 이동 속도
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer; // 배경 색상 변경용
    [SerializeField] private Transform boundary; // 위치 초기화를 위한 경계
    [SerializeField] private float offset = 0.3f; // 위치 초기화 시 오프셋

    private void Update()
    {
        Move(); // 매 프레임마다 이동 함수 호출
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + new Vector3(moveX, moveY, 0);

        // 위치가 경계를 벗어나지 않도록 제한
        newPosition.x = Mathf.Clamp(newPosition.x, boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset);
        newPosition.y = Mathf.Clamp(newPosition.y, boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset);

        transform.localPosition = newPosition; // 플레이어 위치 업데이트
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out item item)) // 아이템에 닿았을 때
        {
            GameManager.Instance.UpdateScore(1, 0); // 플레이어 점수 증가
            backgroundSpriteRenderer.color = Color.green; // 배경 색상 변경
            ResetPosition(); // 플레이어 위치 초기화
            item.ResetPosition(); // 아이템 위치 초기화
        }
        else if (collision.TryGetComponent(out Wall wall)) // 벽에 닿았을 때
        {
            GameManager.Instance.UpdateScore(-1, 0); // 플레이어 점수 감소
            backgroundSpriteRenderer.color = Color.red; // 배경 색상 변경
            ResetPosition(); // 플레이어 위치 초기화
            item.ResetPosition(); // 아이템 위치 초기화
        }
    }

    private void ResetPosition() // 플레이어 위치 초기화
    {
        // 위치 초기화: boundary를 기준으로 설정
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z // z축은 그대로 유지
        );
    }
}
