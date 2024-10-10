using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mazewall : MonoBehaviour
{
    public List<GameObject> walls;  // 미로 벽 리스트
    public GameObject wallPrefab;   // 벽 프리팹
    public GameObject item;         // 아이템 오브젝트

    private void Start()
    {
        
    }

    // 아이템을 먹었을 때 랜덤 벽 변경
    private void ChangeRandomWall()
    {
        if (walls.Count == 0) return;

        // 랜덤 벽 선택
        int randomIndex = Random.Range(0, walls.Count);
        GameObject selectedWall = walls[randomIndex];

        // 벽을 제거
        Destroy(selectedWall);
        walls.RemoveAt(randomIndex);

        // 새로운 랜덤 위치에 벽 생성 (필요에 따라 위치를 변경할 수 있음)
        Vector3 newWallPosition = new Vector3(Random.Range(-10, 10), 1, Random.Range(-10, 10)); // 좌표 조정 가능
        GameObject newWall = Instantiate(wallPrefab, newWallPosition, Quaternion.identity);

        // 리스트에 새로운 벽 추가
        walls.Add(newWall);
    }
}
