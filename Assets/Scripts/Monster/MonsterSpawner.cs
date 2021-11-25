using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    // ������ ���͵�
    GameObject[] monsters;
    // ���� ������
    public List<GameObject> monstersSpawner;
    // ��Ÿ�� ������ �ִ��
    const int spawnerMaxSize = 5;
    int monsterSize = 12;
    // ������ ���� ��
    public static int spawnerCount = 0;
    // ���� ���� ��ġ
    public static Transform spawnerPosition;
    // �ӽ� ��ü
    GameObject tempObject;

    private void Start()
    {
        spawnerPosition = gameObject.transform;
        monsters = Resources.LoadAll <GameObject> ("Monster/");
        monstersSpawner = new List<GameObject>();

        // �����ʿ� ���� 12���� �־����
        for (int i = 0; i < monsterSize; i++)
        {
            tempObject = Instantiate(monsters[i], spawnerPosition.position, spawnerPosition.rotation);
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
            yield return new WaitForSeconds(Random.Range(1, 5));
        }
    }

    void Spawn()
    {
        Debug.Log("���� ��");

        // ���� ���� ����
        int selection = Random.Range(0, monsters.Length);
        GameObject monster = monstersSpawner[selection];

        // �̹� Ȱ��ȭ�� ����
        if (monster.activeSelf == true) return;

        monster.transform.position = spawnerPosition.position;
        monster.transform.rotation = spawnerPosition.rotation;

        // ���� Ȱ��ȭ
        if (spawnerCount < spawnerMaxSize)
        {
            Debug.Log("��");
            monster.SetActive(true);
            Debug.Log(monster.name + "�����Ǿ����ϴ�");
            spawnerCount++;
        }
    }
}
