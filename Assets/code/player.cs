using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Transform boundary;
    [SerializeField] private GameObject item; // 아이템 오브젝트

    private Bounds boundaryBounds;

    private void Start()
    {
        // boundary의 SpriteRenderer로부터 정확한 경계 크기를 가져옴
        if (boundary.TryGetComponent(out SpriteRenderer boundarySpriteRenderer))
        {
            boundaryBounds = boundarySpriteRenderer.bounds;
        }
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // 마우스 좌표를 월드 좌표로 변환
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // 이동 속도에 따라 플레이어가 마우스 방향으로 이동
            Vector3 direction = (mousePosition - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * movementSpeed * Time.deltaTime;

            // 이동 제한 없이 위치 설정
            transform.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out item itemComponent)) // 아이템에 닿았을 때
        {
            GameManager.Instance.UpdateScore(1, 0);
            backgroundSpriteRenderer.color = Color.green;
            ResetPosition();
            ResetItemPosition(); // 아이템 위치 초기화
        }
        else if (collision.TryGetComponent(out Wall wall)) // 외벽에 닿았을 때
        {
            GameManager.Instance.UpdateScore(-1, 0);
            backgroundSpriteRenderer.color = Color.red;
            ResetPosition();
        }
        else if (collision.TryGetComponent(out mazewall mazewall)) // 내부 벽에 닿았을 때
        {
            ResetPosition();
            ResetItemPosition();
        }
    }
    private void ResetPosition() // 플레이어 위치 초기화
    {
        transform.position = new Vector3(
            Random.Range(boundaryBounds.min.x, boundaryBounds.max.x),
            Random.Range(boundaryBounds.min.y, boundaryBounds.max.y),
            transform.position.z
        );
    }
    private void ResetItemPosition() // 아이템 위치 초기화
    {
        item.transform.position = new Vector3(
            Random.Range(boundaryBounds.min.x, boundaryBounds.max.x),
            Random.Range(boundaryBounds.min.y, boundaryBounds.max.y),
            item.transform.position.z
        );
    }
}
