using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // ������ ���͵�
    public GameObject[] monsters;
    // ���� ������
    public List<GameObject> monstersSpawner;
    // ��Ÿ�� ������ �ִ��
    const int spawnerMaxSize = 2;
    // ������ ���� ��
    public int spawnerCount = 0;
    // ���� ������ ��
    int monsterSize = 6;
    // �ӽ� ��ü
    GameObject tempObject;

    // ������ ���� ����
    MonsterState monsterState;

    private void Start()
    {
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();
        monsterState = new MonsterState();

        // �����ʿ� ���� 6���� �־����
        for (int i = 0; i < monsterSize; i++)
        {
            tempObject = Instantiate(monsters[i], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterDisappear>().monsterSpawner = this;
            tempObject.transform.parent = transform;
            tempObject.SetActive(false);
            monstersSpawner.Add(tempObject);
        }
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            Debug.Log("���� ����");
            Spawn();
            // 20~40�ʿ� �� ���� ���� Ȱ��ȭ
            yield return new WaitForSeconds(Random.Range(1,5));
        }
    }

    void Spawn()
    {
        // ���� ���� ����
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];
        monster.transform.position = transform.position;

        // �̹� Ȱ��ȭ�� ����
        if (monster.activeSelf == true) return;

        // ���� Ȱ��ȭ
        if (spawnerCount < spawnerMaxSize)
        {
            monster.SetActive(true);
            Debug.Log(monster.name + "�����Ǿ����ϴ�");
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            monsterState.WalkingToDestination();
            spawnerCount++;
        }
    }
}
