using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainbgm : MonoBehaviour
{
    [SerializeField] AudioSource Music;
    [SerializeField] float fadeDuration = 1.0f;  // Fade �ð� (�� ����)
    [SerializeField] float soundvoulume = 1.0f;  // Fade �ð� (�� ����)
    private bool isPlaying = false;  // ���� ������ ��� ������ ����
    private mainbgm mainSound;


    // Fade-in �ڷ�ƾ (���� ���)
    public IEnumerator FadeIn()
    {
        Music.Play();
        float startVolume = 0f;
        Music.volume = startVolume;

        while (Music.volume < 0.5f)
        {
            Music.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        Music.volume = soundvoulume;
        isPlaying = true;
    }

    // Fade-out �ڷ�ƾ (���� ����)
    public IEnumerator FadeOut()
    {
        float startVolume = Music.volume;

        while (Music.volume > 0f)
        {
            Music.volume -= Time.deltaTime / fadeDuration;
            yield return null;
        }

        Music.Stop();
        Music.volume = startVolume;
        isPlaying = false;
    }
    public void OnOff() // ���� �浹 �� ���� ����
    {
        if (isPlaying)
        {
            StartCoroutine(FadeOut());
        }
    }
    public void OnMusic() //on��ư�� ������ �뷡����
    {
        if (!isPlaying)
        {
            StartCoroutine(FadeIn());
        }
    }
}
