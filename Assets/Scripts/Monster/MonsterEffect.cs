using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterEffect : MonsterState
{
    GameObject[] particle;
    public GameObject effect = null;

    private void Start()
    {
        particle = gameObject.transform.GetComponentInChildren<GameObject[]>();
    }

    public void ChangeEffect()
    {
        if (isSuccess) effect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        // monster�� destination�� �����ϸ� ����
        if (other.tag == "Destination")
        {
            if (other.gameObject == returnDestination)
            {
                // ���� particle(effect)
                int effectSelection = Random.Range(0, particle.Length);
                particle[effectSelection].SetActive(true);
                Debug.Log("��ƼŬ Ȱ��ȭ : " + gameObject.name + particle[effectSelection].name);
                effect = particle[effectSelection];
            }
        }
    }
}
