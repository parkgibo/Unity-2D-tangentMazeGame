using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f; // �÷��̾� �̵� �ӵ�
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer; // ��� ���� �����
    [SerializeField] private Transform boundary; // ��ġ �ʱ�ȭ�� ���� ���
    [SerializeField] private float offset = 0.3f; // ��ġ �ʱ�ȭ �� ������

    private void Update()
    {
        Move(); // �� �����Ӹ��� �̵� �Լ� ȣ��
    }

    private void Move()
    {
        float moveX = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float moveY = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;

        Vector3 newPosition = transform.localPosition + new Vector3(moveX, moveY, 0);

        // ��ġ�� ��踦 ����� �ʵ��� ����
        newPosition.x = Mathf.Clamp(newPosition.x, boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset);
        newPosition.y = Mathf.Clamp(newPosition.y, boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset);

        transform.localPosition = newPosition; // �÷��̾� ��ġ ������Ʈ
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out item item)) // �����ۿ� ����� ��
        {
            GameManager.Instance.UpdateScore(1, 0); // �÷��̾� ���� ����
            backgroundSpriteRenderer.color = Color.green; // ��� ���� ����
            ResetPosition(); // �÷��̾� ��ġ �ʱ�ȭ
            item.ResetPosition(); // ������ ��ġ �ʱ�ȭ
        }
        else if (collision.TryGetComponent(out Wall wall)) // ���� ����� ��
        {
            GameManager.Instance.UpdateScore(-1, 0); // �÷��̾� ���� ����
            backgroundSpriteRenderer.color = Color.red; // ��� ���� ����
            ResetPosition(); // �÷��̾� ��ġ �ʱ�ȭ
            item.ResetPosition(); // ������ ��ġ �ʱ�ȭ
        }
    }

    private void ResetPosition() // �÷��̾� ��ġ �ʱ�ȭ
    {
        // ��ġ �ʱ�ȭ: boundary�� �������� ����
        transform.localPosition = new Vector3(
            Random.Range(boundary.position.x - boundary.localScale.x / 2 + offset, boundary.position.x + boundary.localScale.x / 2 - offset),
            Random.Range(boundary.position.y - boundary.localScale.y / 2 + offset, boundary.position.y + boundary.localScale.y / 2 - offset),
            transform.localPosition.z // z���� �״�� ����
        );
    }
}
