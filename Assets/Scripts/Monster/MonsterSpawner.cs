using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterSpawner : MonoBehaviour
{
    public static List<GameObject> destinationsSpotList = new List<GameObject>();
    // ������ ���͵�
    public GameObject[] monsters;
    // ���� ������
    public List<GameObject> monstersSpawner = new List<GameObject>();
    // ��Ÿ�� ������ �ִ��
    const int spawnerMaxSize = 2;
    // ������ ���� ��
    public int spawnerCount = 0;

    // �ӽ� ��ü
    GameObject tempObject;

    private void Start()
    {
        monsters = Resources.LoadAll<GameObject>("Monster/");

        // ���͵��� ������ ����Ʈ ����
        destinationsSpotList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Destination"));
        for (int i = 0; i < destinationsSpotList.Count; i++)
            destinationsSpotList[i].SetActive(false);

        // �����ʿ� ���� 6���� �־����(������Ʈ Ǯ��)
        for (int i = 0; i < monsters.Length; i++)
        {
            tempObject = Instantiate(monsters[i], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterState>().monsterSpawner = this;
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
            Spawn();
            // 20~40�ʿ� �� ���� ���� Ȱ��ȭ
            yield return new WaitForSeconds(UnityEngine.Random.Range(3,5));
        }
    }
    int temp = 0;
    void Spawn()
    {
        // ���� ���� ����
        //int selection = UnityEngine.Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[temp];
        monster.transform.position = transform.position;
        temp++;

        // �̹� Ȱ��ȭ�� ����
        if (monster.activeSelf == true) return;

        // ���� Ȱ��ȭ
        if (spawnerCount < spawnerMaxSize)
        {
            monster.SetActive(true);
            Debug.Log(monster.name + "�����Ǿ����ϴ�");
            GameObject.FindWithTag("Door").GetComponent<DoorOpen>().Open();
            monster.GetComponent<MonsterState>().Setting();
            monster.GetComponent<MonsterState>().state =  "SpawnerToDestination";
            monster.GetComponent<MonsterState>().Walking();
            spawnerCount++;
        }
    }
}
