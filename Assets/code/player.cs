using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Transform boundary;
    [SerializeField] private GameObject item; // ������ ������Ʈ

    private Bounds boundaryBounds;

    private void Start()
    {
        // boundary�� SpriteRenderer�κ��� ��Ȯ�� ��� ũ�⸦ ������
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
        // ���콺 ��ǥ�� ���� ��ǥ�� ��ȯ
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // �̵� �ӵ��� ���� �÷��̾ ���콺 �������� �̵�
            Vector3 direction = (mousePosition - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * movementSpeed * Time.deltaTime;

            // �̵� ���� ���� ��ġ ����
            transform.position = newPosition;
        }
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
        transform.position = new Vector3(
            Random.Range(boundaryBounds.min.x, boundaryBounds.max.x),
            Random.Range(boundaryBounds.min.y, boundaryBounds.max.y),
            transform.position.z
        );
    }
    private void ResetItemPosition() // ������ ��ġ �ʱ�ȭ
    {
        item.transform.position = new Vector3(
            Random.Range(boundaryBounds.min.x, boundaryBounds.max.x),
            Random.Range(boundaryBounds.min.y, boundaryBounds.max.y),
            item.transform.position.z
        );
    }
}
