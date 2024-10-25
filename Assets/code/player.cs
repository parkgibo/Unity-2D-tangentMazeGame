using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Transform boundary;
    [SerializeField] private float offset = 0f;
    [SerializeField] private GameObject item; // ������ ������Ʈ

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
        if (collision.TryGetComponent(out item itemComponent)) // �����ۿ� ����� ��
        {
            GameManager.Instance.UpdateScore(1, 0);
            backgroundSpriteRenderer.color = Color.green;
            ResetPosition();
            ResetItemPosition(); // ������ ��ġ �ʱ�ȭ
        }
        else if (collision.TryGetComponent(out Wall wall)) // �ܺ��� ����� ��
        {
            GameManager.Instance.UpdateScore(-1, 0);
            backgroundSpriteRenderer.color = Color.red;
            ResetPosition();
        }
        else if (collision.TryGetComponent(out mazewall mazewall)) // ���� ���� ����� ��
        {
            ResetPosition();
            ResetItemPosition();
        }
    }

    private void ResetPosition() // �÷��̾� ��ġ �ʱ�ȭ
    {
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z
        );
    }

    private void ResetItemPosition() // ������ ��ġ �ʱ�ȭ
    {
        item.transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            item.transform.localPosition.z
        );
    }
}
