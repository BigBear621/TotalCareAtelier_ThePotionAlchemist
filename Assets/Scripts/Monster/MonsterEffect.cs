using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffect : MonoBehaviour
{
    [Tooltip("0: ȭ��, 1: ����, 2: �ߵ�")]
    public GameObject[] particles;
    public GameObject effect = null;

    ///void Start()
    ///{
        ///particles = GetComponentsInChildren<GameObject>();
    ///}

    public void HideEffect()
    {
        ///if (GetComponent<MonsterState>().isSuccess)
        ///{
        effect.SetActive(false);
        effect = null;
        Debug.Log("��ƼŬ ��Ȱ��ȭ");
        ///}
    }

    public void ShowEffect()
    {
        int randomEffect = Random.Range(0, particles.Length);
        effect = particles[randomEffect];
        effect.SetActive(true);
        Debug.Log("��ƼŬ Ȱ��ȭ: " + name + effect.name);
    }

    ////void OnTriggerEnter(Collider other)
    ////{
    ////    // monster�� destination�� �����ϸ� ����
    ////    if (other.tag == "Destination")
    ////    {
    ////        if (other.gameObject == GetComponent<MonsterState>().returnDestination)
    ////        {
    ////            // ���� particle(effect)
    ////            int effectSelection = Random.Range(0, particle.Length);
    ////            particle[effectSelection].SetActive(true);
    ////            Debug.Log("��ƼŬ Ȱ��ȭ : " + gameObject.name + particle[effectSelection].name);
    ////            effect = particle[effectSelection];
    ////        }
    ////    }
    ////}
}
