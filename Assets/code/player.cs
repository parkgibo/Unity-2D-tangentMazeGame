using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float movementSpeed = 2f;
    [SerializeField] private SpriteRenderer backgroundSpriteRenderer;
    [SerializeField] private Transform boundary;
    [SerializeField] private GameObject item; // ������ ������Ʈ
    [SerializeField] private Color defaultColor = Color.white; // ���� ����
    [SerializeField] private float colorResetTime = 0.5f; // ���� ���� �ð�
    private Bounds boundaryBounds;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public AudioSource itemsound;

    private void Start()
    {
        // boundary�� SpriteRenderer�κ��� ��Ȯ�� ��� ũ�⸦ ������
        if (boundary.TryGetComponent(out SpriteRenderer boundarySpriteRenderer))
        {
            boundaryBounds = boundarySpriteRenderer.bounds;
        }

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
        backgroundSpriteRenderer.color = defaultColor; 
    }
    private void Update()
    {
        Move();
    }
    private void Move()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            // �̵� �ӵ��� ���� �÷��̾ ���콺 �������� �̵�
            Vector3 direction = (mousePosition - transform.position).normalized;
            Vector3 newPosition = transform.position + direction * movementSpeed * Time.deltaTime;

            // ���⿡ ���� ��������Ʈ �¿� ���� ����
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true; // ������ �� ��
            }
            else if (direction.x > 0)
            {
                spriteRenderer.flipX = false; // �������� �� ��
            }

            // �÷��̾� �ִϸ��̼� ����
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);

            // �̵� ���� ���� ��ġ ����
            transform.position = newPosition;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out item itemComponent)) // �����ۿ� ����� ��
        {
            GameManager.Instance.UpdateScore(1, 0);
            ChangeBackgroundColor(Color.green);
            ResetPosition();
            ResetItemPosition(); // ������ ��ġ �ʱ�ȭ
            itempicksound();
        }
        else if (collision.TryGetComponent(out Wall wall)) // �ܺ��� ����� ��
        {
            ChangeBackgroundColor(Color.red);
            ResetPosition();
        }
        else if (collision.TryGetComponent(out mazewall mazewall)) // ���� ���� ����� ��
        {
            ChangeBackgroundColor(Color.red);
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
    private void ChangeBackgroundColor(Color color)
    {
        backgroundSpriteRenderer.color = color;
        Invoke(nameof(ResetBackgroundColor), colorResetTime); // ���� �ð� �� ���� ����
    }
    private void ResetBackgroundColor()
    {
        backgroundSpriteRenderer.color = defaultColor;
    }
    private void itempicksound()
    {
        itemsound.Play();
    }
}
