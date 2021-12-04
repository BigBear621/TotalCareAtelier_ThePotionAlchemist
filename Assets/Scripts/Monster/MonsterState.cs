using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    GameObject exit;
    
    GameObject player;
    Transform playerPosition;
    public bool isSuccess = false; // ------------------------------ player�� �����ؾ� ��, ���� ��

    // monster �̵�
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public GameObject returnDestination = null;

    public Animator animator;
    MonsterEffect monsterEffect = new MonsterEffect();

    // monster state
    string monsterState = "MonsterState";

    // Potion �޴� ��ġ
    public Transform potionHand;

    // switch ��
    public string state;

    private void OnEnable()
    {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        potionHand = transform.Find("PotionPos");
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

            // [---------------- destination���� exit�� �̵� ----------------]
            case "DestinationToExit":

                exit = GameObject.Find("Exit");
                nav.SetDestination(exit.transform.position);
                MonsterSpawner.destinationsSpotList.Add(returnDestination);
                returnDestination.SetActive(false);

                break;
        }
    }
    
    public void TakePotion()
    {
        if (isSuccess)
        {
            GameManager.instance.Score += 10;
            animator.SetBool("Drinking", true);
            animator.SetBool("Walking", true);
            monsterEffect.ChangeEffect();
        }

        else
        {
            GameManager.instance.Score -= 10;
            animator.SetBool("SadWalk", true);
        }

        state = "DestinationToExit";
        Walking();
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
            }
        }

        // monster�� exit�� �����ϸ� ������Ʈ ��Ȱ��ȭ
        if (other.tag == "Exit")
        {
            Debug.Log("exit �±׿� ����");
            nav.speed = 0;
            gameObject.SetActive(false);
        }

        // potion�� �޾��� ��
        if (other.tag == "Potion")
        {
            // -------------------------------- OVRGrabbable ���� ���� namespace �����̤�
            //if (other.GetComponent<OVRGrabbable>().isGrabbed == false)
            //{
            //    other.transform.position = potionHand.transform.position;

            //    //if (���� �̸� == particles[effectSelection].name) isSuccess = true;
            //    //else isSuccess = false;

            //    TakePotion();
            //}
        }
    }
}
