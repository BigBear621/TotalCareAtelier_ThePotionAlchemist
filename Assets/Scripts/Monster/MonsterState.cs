using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    Transform spawnerPosition;
    
    GameObject player;
    Transform playerPosition;
    public bool isSuccess; // ------------------------------ player�� �����ؾ� ��, ���� ��

    // monster �̵�
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    // monster state
    string monsterState = "MonsterState";

    // switch ��
    public string state;

    // isHeal property
    public bool IsSuccess
    {
        get
        {
            return isSuccess;
        }

        set
        {
            isSuccess = value;

            if (isSuccess == true)
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
        isSuccess = false;
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking(int num)
    {
        switch (state)
        {
            // [---------------- spawner���� destination���� �̵� ----------------]
            case "SpawnerToDestination":

                while (num > 0)
                {
                    if (num == 0) break;

                    animator.SetBool("Walking", true);
                    playerPosition = player.transform;
                    int selectDestination = Random.Range(0, destinations.Count);
                    GameObject destination = destinations[selectDestination];

                    if (destination.activeSelf == false)
                    {
                        nav.SetDestination(destination.transform.position);
                        destination.SetActive(true);
                        num--;
                        break;
                    }

                    // destination�� Ȱ��ȭ ������ ��, �ٽ� ��Ȱ��ȭ ������ destination�� ã�� ��
                    else
                    {
                        continue;
                    }

                }
                break;

            // [---------------- destination���� spawner�� �̵� ----------------]
            case "DestinationToSpawner":

                // ����
                if(isSuccess == true)
                {
                    animator.SetBool("Drinking", true);
                    animator.SetBool("Walking", true);
                    spawnerPosition = monsterSpawner.gameObject.transform;
                    nav.SetDestination(spawnerPosition.position);
                }

                // ����
                else
                {
                    animator.SetBool("SadWalk", true);
                    spawnerPosition = monsterSpawner.gameObject.transform;
                    nav.SetDestination(spawnerPosition.position);
                }

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
            int aniSelection = Random.Range(1, 6);
            Debug.Log(gameObject.name + " : " + aniSelection);
            animator.SetInteger(monsterState, aniSelection);
        }

        // monster�� spawner�� �����ϸ� ��Ȱ��ȭ
        if (state == "DestinationToSpawner")
        {
            if (other.tag == "Spawner")
            {
                nav.speed = 0;
                gameObject.SetActive(false);
            }
        }
    }
}
