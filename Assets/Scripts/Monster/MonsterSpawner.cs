using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : Singleton<MonsterSpawner>
{
    // ������ ����Ʈ
    public static List<GameObject> destinationList = new List<GameObject>();
    // ������ �������� ����Ʈ
    public List<int> availableDestination = new List<int>();
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
        destinationList.AddRange(GameObject.FindGameObjectsWithTag("Destination"));

        // �����ʿ� �ִ� ���͵� (50���� �̸� �־���� ��)
        while (monsterPool.Count < spawnerMaxSize)
        {
            int selection = Random.Range(0, monsterPrefabs.Length);
            // ���͸� �������� �ڽ� ������Ʈ�� �ֱ�
            tempObject = Instantiate(monsterPrefabs[selection], transform);
            ReturnToSpawner(tempObject);
            monsterPool.Add(tempObject);
        }
        
        StartCoroutine(SpawnCoroutine());
    }

    IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            Spawn();
            // 20~30�ʿ� �� ���� ���� Ȱ��ȭ
            yield return new WaitForSeconds(Random.Range(20, 30));
        }
    }
   
    public void Spawn()
    {
        CheckDestination();
        if (availableDestination.Count == 0)
            return;
        // �Ź� �ٲ�� ������ ����Ʈ�� �ʱ�ȭ���ش�.
        availableDestination.Clear();

        spawnCount++;
        // spawnCount �� 50���� Ŀ���� �ٽ� ó������ ���ư��� �Ѵ�.
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
        tempObject.SetActive(true);
        Debug.Log(tempObject.name + "�� �����߽��ϴ�.");

        // ������ ����
        destination.Occupy();
        tempObject.GetComponent<MonsterState>().Setting(destination.gameObject);
    }

    public void CheckDestination()
    {
        for (int i = 0; i < destinationList.Count; i++)
        {
            destination = destinationList[i].GetComponent<MonsterDestination>();
            if (destination.isOccupied == false)
                availableDestination.Add(i);
        }
        if (availableDestination.Count == 0)
            return;

        int random = Random.Range(0, availableDestination.Count);
        destination = destinationList[availableDestination[random]].GetComponent<MonsterDestination>();
    }

    public void ReturnToSpawner(GameObject tempObject)
    {
        tempObject.transform.SetPositionAndRotation(transform.position, transform.rotation);
        tempObject.SetActive(false);
    }
} 
