using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    NavMeshAgent nav;
    public GameObject[] destination;

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        // ������ ����(�÷��̾� �տ� �����ϴ� ���)
        destination = new GameObject[6];
        destination = GameObject.FindGameObjectsWithTag("Destination");
    }

    public void WalkingToDestination()
    {
        //int selection = Random.Range(0, destination.Length);
        //nav.SetDestination(destination[selection].transform.position);
    }
}
