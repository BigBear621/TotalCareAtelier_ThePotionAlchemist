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

    private void Start()
    {
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();

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
            yield return new WaitForSeconds(Random.Range(20, 40));
        }
    }

    void Spawn()
    {
        // ���� ���� ����
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];

        // �̹� Ȱ��ȭ�� ����
        if (monster.activeSelf == true) return;

        monster.transform.position = transform.position;

        // ���� Ȱ��ȭ
        if (spawnerCount < spawnerMaxSize)
        {
            Debug.Log("��");
            monster.SetActive(true);
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            GetComponent<MonsterState>().WalkingToDestination();
            Debug.Log(monster.name + "�����Ǿ����ϴ�");
            spawnerCount++;
        }
    }
}
