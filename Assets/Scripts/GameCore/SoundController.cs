using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    [Header("�÷��̾��� ����� �ҽ� 2��")]
    public AudioSource[] sources;
    private AudioSource tempSource;

    [Header("�������(��Ŭ���� ���� �ȳ�)")]
    [Tooltip("0: ��ŸƮ ��� ����\n1: �ΰ��� ���� ����\n2: ����� ��Ȳ ����\n3: �¿��� ����\n4: ��忣�� ����")]
    public AudioClip[] clips;

    [Header("���� ��� ���� ������� Ŭ��")]
    private int musicLoader = 0;
    public int MusicLoader
    {
        get { return musicLoader; }
        set
        {
            musicLoader = value;

            for (int i = 0; i < sources.Length; i++)
            {
                if (sources[i].isPlaying == false)
                {
                    tempSource = sources[i];
                    tempSource.clip = clips[musicLoader];
                    StartCoroutine(CrossFade(tempSource, sources[Mathf.Abs(i - sources.Length + 1)]));
                    break;
                }
            }
        }
    }

    private IEnumerator CrossFade(AudioSource fadeIn, AudioSource fadeOut)
    {
        float fadeInVol = 0;
        float fadeOutVol = 1;

        fadeIn.Play();
        while (fadeOut.volume > 0)
        {
            fadeInVol += Time.deltaTime/2;
            if (fadeInVol >= 1)
                fadeInVol = 1;
            fadeIn.volume = fadeInVol;

            fadeOutVol -= Time.deltaTime/2;
            if (fadeOutVol <= 0)
                fadeOutVol = 0;
            fadeOut.volume = fadeOutVol;

            yield return null;
        }
        fadeOut.Stop();
    }

    public void StopMusic()
    {
        sources[0].Stop();
        sources[1].Stop();
    }
}
