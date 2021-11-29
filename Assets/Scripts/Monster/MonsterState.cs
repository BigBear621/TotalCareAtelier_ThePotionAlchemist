using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterState : MonoBehaviour
{
    public MonsterSpawner monsterSpawner;
    NavMeshAgent nav;
    // ������ �迭(�÷��̾� �տ� �����ϴ� ���)
    public GameObject[] destination;

    private void OnEnable()
    {
        StartCoroutine(Disappear());
    }

    private void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        destination = new GameObject[6];
        destination = Resources.LoadAll<GameObject>("MonsterDestination/");
    }

    // �÷��̾� ������ �̵�
    public void WalkingToDestination()
    {
        // null ���� Ȯ���ϱ�
        //int selection = Random.Range(0, destination.Length);
        //nav.SetDestination(destination[selection].transform.position);
    }

    // ���Ͱ� spawner�� ���ƿ��� �� ��Ȱ��ȭ
    IEnumerator Disappear()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            // ���Ϳ� spawner�� �Ÿ� ���̰� 0.05���� ������ ���� ��Ȱ��ȭ
            if (Vector3.Distance(monsterSpawner.transform.position, transform.position) < 0.05f)
            {
                gameObject.SetActive(false);
                Debug.Log(gameObject.name + "�� ��Ȱ��ȭ �Ǿ����ϴ�");
                monsterSpawner.spawnerCount--;
            }
        }
    }
}
