using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Transform boundary;
    [SerializeField] private float offset = 0f;
    [SerializeField] private GameObject item; // 아이템 오브젝트

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + new Vector3(moveX, moveY, 0);

        newPosition.x = Mathf.Clamp(newPosition.x, boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset);
        newPosition.y = Mathf.Clamp(newPosition.y, boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset);
        transform.localPosition = newPosition;
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
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z
        );
    }

    private void ResetItemPosition() // 아이템 위치 초기화
    {
        item.transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            item.transform.localPosition.z
        );
    }
}
