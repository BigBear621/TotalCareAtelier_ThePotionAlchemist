using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    // ������ ����Ʈ
    public static List<GameObject> destinationList = new List<GameObject>();
    // ������ �������� ����Ʈ
    public List<int> availableNum = new List<int>();
    // �̵��� ������
    MonsterDestination destination;
    // ������ ���͵�
    public GameObject[] monsterPrefabs;
    // ���� �����ʿ��� ���� ����
    public List<GameObject> monsterPool = new List<GameObject>();
    // ��Ÿ�� ������ �ִ��
    const int spawnerMaxSize = 50;
    // ������ ���� ��
    public int spawnCount = 0;
    // �ӽ� ��ü
    GameObject tempObject;

    void Start()
    {
        monsterPrefabs = Resources.LoadAll<GameObject>("Monster/");

        // ���͵��� ������ ����Ʈ ����
        ///destinationsSpotList = new List<GameObject>(GameObject.FindGameObjectsWithTag("Destination"));
        destinationList.AddRange(GameObject.FindGameObjectsWithTag("Destination"));
        ///for (int i = 0; i < destinationList.Count; i++)
            ///destinationList[i].SetActive(false);

        // �����ʿ� �ִ� ���͵� (50���� �̸� �־���� ��)
        ///for (int i = 0; i < spawnerMaxSize; i++)
        while (monsterPool.Count < spawnerMaxSize)
        {
            int selection = Random.Range(0, monsterPrefabs.Length);
            // ���͸� �������� �ڽ� ������Ʈ�� �ֱ�
            tempObject = Instantiate(monsterPrefabs[selection], transform);
            ///tempObject.GetComponent<MonsterState>().monsterSpawner = this;
            ReturnToSpawner(tempObject);
            monsterPool.Add(tempObject);
        }
        
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        int spawnDelay;

        while (true)
        {
            Spawn();
            // 20~30�ʿ� �� ���� ���� Ȱ��ȭ
            spawnDelay = Random.Range(1,5);
            yield return new WaitForSeconds(spawnDelay);
        }
    }
   
    public void Spawn()
    {
        CheckDestination();
        if (availableNum.Count == 0)
            return;
        // �Ź� �ٲ�� ������ ����Ʈ�⿡ �ʱ�ȭ���ش�.
        availableNum.Clear();

        spawnCount++;
        // spawnCount �� 50���� Ŀ���� �ٽ� ó������ ���ư��� �Ѵ�. 
        ///if (spawnerCount > spawnerMaxSize) spawnerCount = 0;
        if (spawnCount > monsterPool.Count)
            spawnCount = 0;

        tempObject = monsterPool[spawnCount];

        // �̹� Ȱ��ȭ�� ���Ͷ�� 
        if (tempObject.activeSelf == true)
        {
            spawnCount++;
            return;
        }

        // ���� Ȱ��ȭ
        ///monsterPool[spawnerCount].transform.position = transform.position;
        tempObject.SetActive(true);
        Debug.Log(tempObject.name + "�� �����߽��ϴ�.");
        // ������ ����
        tempObject.GetComponent<MonsterState>().Setting(destination.gameObject);
        ///tempObject.GetComponent<MonsterState>().state = "SpawnerToDestination";
        ///tempObject.GetComponent<MonsterState>().Walking();
    }

    public void CheckDestination()
    {
        for (int i = 0; i < destinationList.Count; i++)
        {
            destination = destinationList[i].GetComponent<MonsterDestination>();
            if (destination.isOccupied == false)
                availableNum.Add(i);
        }
        if (availableNum.Count == 0)
            return;
        int random = Random.Range(0, availableNum.Count);
        destination = destinationList[availableNum[random]].GetComponent<MonsterDestination>();
    }

    public void ReturnToSpawner(GameObject tempObject)
    {
        tempObject.transform.position = transform.position;
        tempObject.transform.rotation = transform.rotation;
        tempObject.SetActive(false);
    }
} 
