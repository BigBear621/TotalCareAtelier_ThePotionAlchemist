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
    int selectDestination;
    public Animator animator;
    public GameObject particle;
    public List<ParticleSystem> particles;

    public GameObject returnDestination = null;
    public ParticleSystem effect = null;

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
        particle = GameObject.FindGameObjectWithTag("Effect");
        particles = new List<ParticleSystem>();

        // ��ƼŬ ��������
        for(int i = 0; i < particle.transform.childCount; i++)
        {
            // particles.Add(particle.transform.GetChild(i).parent);
        }

        isSuccess = false;
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
                selectDestination = Random.Range(0, destinations.Count);
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
                    effect.Stop();
                    animator.SetBool("Drinking", true);
                    animator.SetBool("Walking", true);
                }

                // ����
                else animator.SetBool("SadWalk", true);

                spawnerPosition = monsterSpawner.gameObject.transform;
                nav.SetDestination(spawnerPosition.position);
                MonsterSpawner.destinationsSpotList.Add(returnDestination);
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
                int effectSelection = Random.Range(0, 3);
                particles[effectSelection].Play();
                effect = particles[effectSelection];
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
