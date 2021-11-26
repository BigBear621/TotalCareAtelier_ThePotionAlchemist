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
    int monsterSize = 6;
    // ������ ���� ��
    public int spawnerCount = 0;

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
            yield return new WaitForSeconds(Random.Range(1, 5));
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
            Debug.Log(monster.name + "�����Ǿ����ϴ�");
            spawnerCount++;
        }
    }
}
