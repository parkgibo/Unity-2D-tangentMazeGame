using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    private Animator animator;
    private float speed = 4f;
    Vector3 mousePos, transPos, targetPos, dist;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
            CalTargetPos();
        RotateMove();
    }

    // 마우스 위치 계산
    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }

    // 플레이어 이동 및 회전 처리
    void RotateMove()
    {
        dist = targetPos - transform.position;
        transform.position += dist * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // 충돌 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 아이템과 충돌 시 플레이어 점수 증가
        if (collision.TryGetComponent(out item item))
        {
            // GameManager의 플레이어 점수 증가 메서드 호출
            GameManager.Instance.UpdateScore(1, 0); // playerScore 증가
        }
        // mazewall과 충돌 시 처리 (MoveToTargetAgent처럼 패널티 추가)
        else if (collision.TryGetComponent(out mazewall mazewall))
        {
            // 원하는 패널티 로직 또는 반응을 추가할 수 있습니다
            Debug.Log("Player hit the maze wall!");
            GameManager.Instance.UpdateScore(-1, 0); // 패널티 적용 예시
        }
        // Wall과 충돌 시 처리
        else if (collision.TryGetComponent(out Wall wall))
        {
            // 원하는 패널티 로직 또는 반응을 추가할 수 있습니다
            Debug.Log("Player hit the wall!");
            GameManager.Instance.UpdateScore(-1, 0); // 패널티 적용 예시
        }
    }
}
