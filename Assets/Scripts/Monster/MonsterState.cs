using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    
    GameObject player;
    Transform playerPosition;

    // monster �̵�
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    // switch ��
    public string state;

    public bool isHeal = false;

    // isHeal property
    public bool IsHeal
    {
        get
        {
            return isHeal;
        }

        set
        {
            isHeal = value;

            if (isHeal == true)
                GameManager.instance.score += 100;

            else
                GameManager.instance.score -= 100;
        }
    }

    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Target");
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking()
    {
        switch (state)
        {
            // spawner���� destination���� �̵�
            case "SpawnerToDestination":

                animator.SetBool("Walking", true);
                playerPosition = player.transform;
                int selectDestination = Random.Range(0, destinations.Count);
                GameObject destination = destinations[selectDestination];

                Debug.Log("�̵� ����Ʈ  : " + destination.name + selectDestination);

                if (destination.activeSelf == false)
                {
                    nav.SetDestination(destination.transform.position);
                    destination.SetActive(true);
                }

                // destination�� Ȱ��ȭ ������ ��, �ٽ� ��Ȱ��ȭ ������ destination�� ã�� ��
                else
                {
                    // return Walking();
                }

                break;

            // destination���� spawner�� �̵�
            case "DestinationToSpawner":

                // if : drinking�� ���� ���� �׳� �̵�
                // else : ȭ���� ��, sad wallking

                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // monster�� destination�� �����ϸ� ����
        if (other.tag == "Destination")
        {
            animator.SetBool("Walking", false);
            transform.LookAt(playerPosition.position);
            nav.speed = 0;
        }

        // monster�� spawner�� �����ϸ� ��Ȱ��ȭ
        if (state == "DestinationToSpawner")
        {
            if (other.tag == "Spawner")
            {
                gameObject.SetActive(false);
                monsterSpawner.spawnerCount--;
                Debug.Log(gameObject.name + "�� ��Ȱ��ȭ �Ǿ����ϴ�");
            }
        }
    }
}
