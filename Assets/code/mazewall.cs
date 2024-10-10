using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazewall : MonoBehaviour
{
    public List<GameObject> walls;  // �̷� �� ����Ʈ
    public GameObject wallPrefab;   // �� ������
    public GameObject item;         // ������ ������Ʈ

    private void Start()
    {
        
    }

    // �������� �Ծ��� �� ���� �� ����
    private void ChangeRandomWall()
    {
        if (walls.Count == 0) return;

        // ���� �� ����
        int randomIndex = Random.Range(0, walls.Count);
        GameObject selectedWall = walls[randomIndex];

        // ���� ����
        Destroy(selectedWall);
        walls.RemoveAt(randomIndex);

        // ���ο� ���� ��ġ�� �� ���� (�ʿ信 ���� ��ġ�� ������ �� ����)
        Vector3 newWallPosition = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)); // ��ǥ ���� ����
        GameObject newWall = Instantiate(wallPrefab, newWallPosition, Quaternion.identity);

        // ����Ʈ�� ���ο� �� �߰�
        walls.Add(newWall);
    }
}
