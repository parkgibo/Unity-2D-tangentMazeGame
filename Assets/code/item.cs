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
            // ������ ����
            OnItemCollected?.Invoke();  // �̺�Ʈ ȣ��
            Destroy(gameObject);  // ������ ����
        }
    }
}
