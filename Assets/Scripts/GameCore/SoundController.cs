using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Singleton<SoundController>
{
    public AudioSource source;
    [Tooltip("0: ��ŸƮ ��� ����\n1: �ΰ��� ���� ����\n2: ����� ��Ȳ ����\n3: �¿��� ����\n4: ��忣�� ����")]
    public AudioClip[] clips;

    public int musicState;
    public int MusicState
    {
        get
        {
            return musicState;
        }
        set
        {
            musicState = value;
            source.clip = clips[musicState];
            source.Play();
        }
    }

    void Start()
    {
        MusicState = 0;
    }
}
