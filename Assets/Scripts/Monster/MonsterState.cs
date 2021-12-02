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
    public GameObject particles;
    public List<GameObject> particle;

    public GameObject returnDestination = null;
    public GameObject effect = null;

    // monster state
    string monsterState = "MonsterState";

    // switch ��
    public string state;

    // isSuccess property
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
        particles = GameObject.FindGameObjectWithTag("Effect");
        particle = new List<GameObject>();

        /////////////////////////////////////////////////////////////
        particle.Add(particles.transform.GetChild(0).gameObject);
        particle.Add(particles.transform.GetChild(1).gameObject);
        particle.Add(particles.transform.GetChild(2).gameObject);
        particles.transform.GetChild(0).gameObject.SetActive(true);
        particles.transform.GetChild(1).gameObject.SetActive(true);
        particles.transform.GetChild(2).gameObject.SetActive(true);
        /////////////////////////////////////////////////////////////
        isSuccess = false;
    }

    private void Update()
    {
        Debug.Log(particles);
        Debug.Log(particle.Count);
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
    }

    public void Walking()
    {
        switch (state)
        {
            // [---------------- spawner���� destination���� �̵� ----------------]
            case "SpawnerToDestination":

                if (destinations.Count <= 0) break;

                GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
                animator.SetBool("Walking", true);
                playerPosition = player.transform;
                int selectDestination = Random.Range(0, destinations.Count);
                GameObject destination = destinations[selectDestination];

                if (destination.activeSelf == false)
                {
                    nav.SetDestination(destination.transform.position);
                    destination.SetActive(true);
                    returnDestination = destination;
                    MonsterSpawner.destinationsSpotList.RemoveAt(selectDestination);
                    Debug.Log(gameObject.name + "�̵� ����" + destination.name);
                }

                break;

            // [---------------- destination���� spawner�� �̵� ----------------]
            case "DestinationToSpawner":

                // ����
                if (isSuccess == true)
                {
                    effect.SetActive(false);
                    animator.SetBool("Drinking", true);
                    animator.SetBool("Walking", true);
                }

                // ����
                else animator.SetBool("SadWalk", true);

                spawnerPosition = monsterSpawner.gameObject.transform;
                nav.SetDestination(spawnerPosition.position);
                returnDestination.SetActive(false);

                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        // monster�� destination�� �����ϸ� ����
        if (other.tag == "Destination")
        {
            if (other.gameObject == returnDestination)
            {
                transform.LookAt(playerPosition.position);
                nav.speed = 0;

                // ���� animation
                int aniSelection = Random.Range(1, 6);
                animator.SetInteger(monsterState, aniSelection);

                // ���� effect(particle)
                int effectSelection = Random.Range(0, 2);
                particles.transform.GetChild(effectSelection).gameObject.SetActive(true);
                effect = particle[effectSelection];
            }
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
