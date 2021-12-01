using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MonsterSpawner : MonoBehaviour
{
    // ������ ����Ʈ
    public static List<GameObject> destinationsSpotList = new List<GameObject>();
    // ������ ���͵�
    public GameObject[] monsters;
    // ���� �����ʿ��� ���� ����
    public List<GameObject> monster = new List<GameObject>();
    // ��Ÿ�� ������ �ִ��
    const int spawnerMaxSize = 100;
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

        // �����ʿ� �ִ� ���͵� (100���� �̸� �־���� ��)
        for (int i = 0; i < spawnerMaxSize; i++)
        {
            int selection = UnityEngine.Random.Range(0, monsters.Length);
            tempObject = Instantiate(monsters[selection], transform.position, transform.rotation);
            tempObject.GetComponent<MonsterState>().monsterSpawner = this;
            tempObject.transform.parent = transform; // ���̾��Űâ ����
            tempObject.SetActive(false);
            monster.Add(tempObject);
        }
        StartCoroutine(SpawnMonster());
    }

    IEnumerator SpawnMonster()
    {
        while (true)
        {
            Spawn();
            // 20~30�ʿ� �� ���� ���� Ȱ��ȭ
            yield return new WaitForSeconds(UnityEngine.Random.Range(20,30));
        }
    }
   
    void Spawn()
    {
        spawnerCount++;
        // spawnerCount �� 100���� Ŀ���� �ٽ� ó������ ���ư��� �Ѵ�. 
        if (spawnerCount > spawnerMaxSize) spawnerCount = 0;

        // �̹� Ȱ��ȭ�� ����
        if (monster[spawnerCount].activeSelf == true) return;

        // ���� ���� ����
        monster[spawnerCount].transform.position = transform.position;
        monster[spawnerCount].SetActive(true);
        Debug.Log(monster[spawnerCount].name + "�����Ǿ����ϴ�");
        monster[spawnerCount].GetComponent<MonsterState>().Setting();
        GameObject.FindGameObjectWithTag("Door").GetComponent<DoorOpen>().Open();
        monster[spawnerCount].GetComponent<MonsterState>().state = "SpawnerToDestination";
        monster[spawnerCount].GetComponent<MonsterState>().Walking();
    }
}
