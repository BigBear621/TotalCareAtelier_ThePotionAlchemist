using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    // �׺�޽�
    NavMeshAgent nav;
    // �̵��� ������
    public List<Transform> destination;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        destination = new List<Transform>();
    }

    public void WalkingToDestination()
    {
        int selection = Random.Range(0, destination.Count);
        nav.SetDestination(destination[selection].position);
    }
}
