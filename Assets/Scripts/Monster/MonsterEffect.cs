using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    [Tooltip("0: ȭ��, 1: ����, 2: �ߵ�")]
    public GameObject[] particles;
    public GameObject effect = null;


    public void HideEffect()
    {
        effect.SetActive(false);
        effect = null;
        Debug.Log("��ƼŬ ��Ȱ��ȭ");
    }

    public void ShowEffect()
    {
        int randomEffect = Random.Range(0, particles.Length);
        effect = particles[randomEffect];
        effect.SetActive(true);
        Debug.Log("��ƼŬ Ȱ��ȭ: " + name + effect.name);
    }

}
