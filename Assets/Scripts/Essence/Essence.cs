using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour, IMixFunc
{
    public static Queue<GameObject> essence;
    const int maxAmount = 2;

    public GameObject[] potionPrefabs;

    enum POTION_TYPE
    {
        DEBURN, DEPARALYSE, DETOX, EXPLODE
    }

    //[HideInInspector]
    //public GameObject deburnPotion, deparalysePotion, detoxPotion, explodePotion;

    public Transform spawner;

    [Tooltip("1: ���� ���� ����, 2: ���� ���� ����")]
    public AudioClip[] clips;

    void Start()
    {
        essence = new Queue<GameObject>();
        potionPrefabs = Resources.LoadAll<GameObject>("Potion/");
        //deburnPotion = Resources.Load<GameObject>("Potion/DeburnPotion");
        //deparalysePotion = Resources.Load<GameObject>("Potion/DeparalysePotion");
        //detoxPotion = Resources.Load<GameObject>("Potion/DetoxPotion");
        //explodePotion = Resources.Load<GameObject>("Potion/ExplodePotion");

    }

    public void AddToEssence(GameObject component)
    {
        Debug.Log("��Ḧ �߰��Ѵ�.");
        essence.Enqueue(component);
        component.gameObject.SetActive(false);
    }

    public void CreatePotion()
    {
        Debug.Log("������ �����Ѵ�.");
        GameObject component0 = essence.Dequeue();
        GameObject component1 = essence.Dequeue();
        int potionNum = -1;
        potionNum = potionNum.CheckCombi(component0, component1);
        if (potionNum == -1)
        {
            Debug.Log("���� ����!");
            SoundController.instance.source.PlayOneShot(clips[1]);
        }
        else
        {
            Debug.Log(potionNum + "���� ���� ����!");
            Instantiate(potionPrefabs[potionNum], spawner);
            SoundController.instance.source.PlayOneShot(clips[0]);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� ��Ҵ�.");
        if (other.GetComponent<Ingredient>() != null)
        {
            Debug.Log("��Ḧ �ν��ߴ�.");
            if (other.GetComponent<OVRGrabbable>().isGrabbed != true)
            {
                if (essence.Count < maxAmount)
                {
                    AddToEssence(other.gameObject);
                    if (essence.Count == maxAmount)
                        CreatePotion();
                }
                else
                    return;
            }
        }
    }
}
