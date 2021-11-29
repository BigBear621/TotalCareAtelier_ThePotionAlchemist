using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;

    // monster �̵�
    public NavMeshAgent nav;
    public List<GameObject> destinations;
    public Animator animator;

    private void OnEnable()
    {
        StartCoroutine(Disappear());
        nav = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Setting()
    {
        destinations = MonsterSpawner.destinationsSpotList;
        Debug.Log("��ŸƮ �׽�Ʈ : " + destinations.Count);
    }

    // �÷��̾� ������ �̵�
    public void WalkingToDestination()
    {
        Debug.Log("�̵� �׽�Ʈ : " + destinations.Count);
        animator.SetBool("Walking", true);
        int selectDestination = Random.Range(0, destinations.Count);
        GameObject destination = destinations[selectDestination];

        Debug.Log("�̵� ����Ʈ  : " + destination.name + selectDestination);

        if (destination.activeSelf == false)
        {
            nav.SetDestination(destination.transform.position);
            destination.SetActive(true);
        }

        else
        {
            //WalkingToDestination();
        }
    }

    // ���Ͱ� spawner�� ���ƿ��� �� ��Ȱ��ȭ
    IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            // ���Ϳ� spawner�� �Ÿ� ���̰� 0.001���� ������ ���� ��Ȱ��ȭ
            if (Vector3.Distance(monsterSpawner.transform.position, transform.position) < 0.001f)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + "�� ��Ȱ��ȭ �Ǿ����ϴ�");
                monsterSpawner.spawnerCount--;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Destination") animator.SetBool("Walking", false);
    }
}
