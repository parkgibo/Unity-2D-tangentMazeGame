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

    // ���콺 ��ġ ���
    void CalTargetPos()
    {
        mousePos = Input.mousePosition;
        transPos = Camera.main.ScreenToWorldPoint(mousePos);
        targetPos = new Vector3(transPos.x, transPos.y, 0);
    }

    // �÷��̾� �̵� �� ȸ�� ó��
    void RotateMove()
    {
        dist = targetPos - transform.position;
        transform.position += dist * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dist.y, dist.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    // �浹 ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �����۰� �浹 �� �÷��̾� ���� ����
        if (collision.TryGetComponent(out item item))
        {
            // GameManager�� �÷��̾� ���� ���� �޼��� ȣ��
            GameManager.Instance.UpdateScore(1, 0); // playerScore ����
        }
        // mazewall�� �浹 �� ó�� (MoveToTargetAgentó�� �г�Ƽ �߰�)
        else if (collision.TryGetComponent(out mazewall mazewall))
        {
            // ���ϴ� �г�Ƽ ���� �Ǵ� ������ �߰��� �� �ֽ��ϴ�
            Debug.Log("Player hit the maze wall!");
            GameManager.Instance.UpdateScore(-1, 0); // �г�Ƽ ���� ����
        }
        // Wall�� �浹 �� ó��
        else if (collision.TryGetComponent(out Wall wall))
        {
            // ���ϴ� �г�Ƽ ���� �Ǵ� ������ �߰��� �� �ֽ��ϴ�
            Debug.Log("Player hit the wall!");
            GameManager.Instance.UpdateScore(-1, 0); // �г�Ƽ ���� ����
        }
    }
}
