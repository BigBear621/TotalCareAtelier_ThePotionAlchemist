using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    #region Monster's Lifecycle ������ ��ƾ
    ///public MonsterSpawner monsterSpawner;
    ///public List<GameObject> destinationList;
    public GameObject destination;
    public GameObject exit;

    NavMeshAgent nav;
    GameObject player;
    #endregion

    // switch ��
    public string monsterState;

    ///Transform playerPosition;
    public bool isSuccess = false; // ------------------------------ player�� �����ؾ� ��, ���� ��

    Animator animator;
    MonsterEffect monsterEffect;

    // Potion �޴� �� ��ġ
    public Transform potionHand;
    public GameObject potion;

    GameTimer gameTimer;

    ///string state;

    void OnEnable()
    {
        isSuccess = false;
        player = GameObject.Find("Player");
        exit = GameObject.Find("Exit");
        potionHand = transform.Find("PotionPos");

        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        monsterEffect = GetComponent<MonsterEffect>();
        gameTimer = transform.Find("Canvas").GetComponentInChildren<GameTimer>();
    }

    public void Setting(GameObject destination)
    {
        this.destination = destination;
        ///destinationList = MonsterSpawner.destinationList;
        monsterState = "SpawnerToDestination";
        Walking();
    }

    public void Walking()
    {
        switch (monsterState)
        {
            // [---------------- spawner���� destination���� �̵� ----------------]
            case "SpawnerToDestination":

                ///if (destinationList.Count <= 0) break;

                GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
                animator.SetBool("Walking", true);
                ///playerPosition = player.transform;
                ///int selectDestination = Random.Range(0, destinationList.Count);
                ///GameObject destination = destinationList[selectDestination];

                ///if (destination.activeSelf == false)
                ///{
                nav.SetDestination(destination.transform.position);
                ///destination.SetActive(true);
                ///this.destination = destination;
                ///MonsterSpawner.destinationList.RemoveAt(selectDestination);
                Debug.Log(destination.name + "�� �̵��ϴ� " + gameObject.name);
                ///}
                break;

            // [---------------- destination���� exit�� �̵� ----------------]
            case "DestinationToExit":

                nav.SetDestination(exit.transform.position);
                Debug.Log(exit.name + "�� �̵��ϴ� " + gameObject.name);
                ///MonsterSpawner.destinationList.Add(this.destination);
                ///this.destination.SetActive(false);
                destination.GetComponent<MonsterDestination>().Leave();
                break;
        }
    }
    
    public void TakePotion(GameObject potion)
    {
        potion.transform.position = potionHand.position;
        potion.transform.parent = potionHand;
        this.potion = potion;

        if (isSuccess)
        {
            GameManager.instance.Score += 10;
            animator.SetBool("Drinking", true);
            animator.SetBool("Walking", true);
            monsterEffect.HideEffect();
        }

        else
        {
            GameManager.instance.Score -= 10;
            animator.SetBool("SadWalk", true);
        }

        monsterState = "DestinationToExit";
        Walking();
    }

    void OnTriggerEnter(Collider other)
    {
        // monster�� destination�� �����ϸ� ����
        ///if (other.tag == "Destination")
        if (other.gameObject == destination)
        {
            ///if (other.gameObject == destination)
            ///{
            transform.LookAt(player.transform.position);
            nav.speed = 0;
            animator.SetBool("Walking", false);
            destination.GetComponent<MonsterDestination>().Occupy();

            // ���� animation
            int aniSelection = Random.Range(1, 6);
            animator.SetInteger("MonsterState", aniSelection);

            monsterEffect.ShowEffect();
            gameTimer.DecreaseTime();
            Debug.Log("�ð� ���ҽ���");
            ///}
        }

        // monster�� exit�� �����ϸ� ������Ʈ ��Ȱ��ȭ
        if (other.tag == "Exit")
        {
            Debug.Log("�ⱸ�� ����");
            nav.speed = 0;
            Destroy(potion);
            MonsterSpawner.instance.ReturnToSpawner(gameObject);
            ///gameObject.SetActive(false);
        }

        // potion�� �޾��� ��
        if (other.tag == "Potion")
        {
            if (other.GetComponent<OVRGrabbable>().isGrabbed == false)
            {
                if (other.GetComponent<Potion>().symptom == monsterEffect.effect.name)
                    isSuccess = true;
                else
                    isSuccess = false;
                //if (���� �̸� == particles[effectSelection].name) isSuccess = true;
                //else isSuccess = false;
                gameTimer.ResetTime();
                TakePotion(other.gameObject);
            }
        }
    }
}
