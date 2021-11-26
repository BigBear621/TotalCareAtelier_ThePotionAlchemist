using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTest : MonoBehaviour
{
    NavMeshAgent nav;
    // �̵��� ������
    public Transform targetPos;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        nav.SetDestination(targetPos.position);
    }
}
