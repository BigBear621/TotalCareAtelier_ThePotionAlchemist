using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGoBack : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(GoBack());
    }

    IEnumerator GoBack()
    {
        yield return new WaitForSeconds(2);

        if (MonsterSpawner.spawnerPosition.transform.position == gameObject.transform.position)
        {
            gameObject.SetActive(false);
            Debug.Log(gameObject.name + "�� ��Ȱ��ȭ �Ǿ����ϴ�");
            MonsterSpawner.spawnerCount--;
        }
    }
}
