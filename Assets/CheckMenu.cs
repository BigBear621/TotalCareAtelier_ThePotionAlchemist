using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMenu : MonoBehaviour
{
    public GameObject complitePotion;
    Potion potion;
    float timeSpan;  //��� �ð��� ���� ����
    float[] checkTime = { 1.0f, 3.0f, 5.0f };  // Ư�� �ð��� ���� ����
    float[] size = { 0.01f, 0.05f, 0.12f };
    void Start()
    {
        timeSpan = 0.0f;
        //checkTime = 5.0f;  // Ư���ð��� 5�ʷ� ����
    }
    public void check()
    {
        Debug.Log("��2");
    }

    public void putWaterCheck()
    {
        Debug.Log("���� �����ð� :" + timeSpan);
        timeSpan += Time.deltaTime;  // ��� �ð��� ��� ���
       
        // �ð������ ���� �׾Ƹ��� ��ũ�� ����
        for (int i = 0; i < checkTime.Length; i++)
        {
            if (timeSpan > checkTime[i])  // ��� �ð��� Ư�� �ð��� ���� Ŀ���� ���
            {
                complitePotion.transform.localScale = new Vector3(0.35f, size[i], 0.35f);

                /*
                  �׾Ƹ� ���� ��ũ�� üũ �� 
                */
                if (i == 2)
                {
                    timeSpan = 0;
                    SceneManager.LoadScene("_Scene");
                }
            }
        }
    }
}
