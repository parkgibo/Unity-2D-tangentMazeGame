using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public delegate void ItemCollected();
    public event ItemCollected OnItemCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 아이템 수집
            OnItemCollected?.Invoke();  // 이벤트 호출
            Destroy(gameObject);  // 아이템 제거
        }
    }
}
