using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenceTest : MonoBehaviour, IMixFunc
{
    public static Queue<Ingredient> mixture;
    const int mixtureAmount = 2;

    void Start()
    {
        mixture = new Queue<Ingredient>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MixEssence();
    }

    public void AddMixture(Ingredient component)
    {
        Debug.Log("��Ḧ �߰��Ѵ�.");
        if (mixture.Count < mixtureAmount)
        {
            mixture.Enqueue(component);
            component.gameObject.SetActive(false);
        }
    }

    public void MixEssence()
    {
        Debug.Log("�������� ���´�.");
        if (mixture.Count == 2)
            CheckCombi();
        else
            Debug.Log("��ᰡ �� ����.");
    }

    public void CheckCombi()
    {
        Debug.Log("������ Ȯ���Ѵ�.");
        Potion potion = new Potion();
        Ingredient component0 = mixture.Dequeue();
        Ingredient component1 = mixture.Dequeue();
        potion.CheckCombi(component0, component1);
        if (potion == null)
        {
            Debug.Log("����!");
            Destroy(potion);
        }
        else
        {
            Debug.Log(potion.name + "���� ����!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAddFunc>() != null)
        {
            AddMixture(other.GetComponent<Ingredient>());
        }
    }
}
