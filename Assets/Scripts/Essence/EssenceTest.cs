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

    public void AddMixture(Ingredient component)
    {
        Debug.Log("��Ḧ �߰��Ѵ�.");
        if (mixture.Count < mixtureAmount)
            mixture.Enqueue(component);
    }

    public void MixEssence()
    {
        Debug.Log("�������� ���´�.");
        if (mixture.Count == 2)
            CheckCombi();
    }

    public void CheckCombi()
    {
        Debug.Log("������ Ȯ���Ѵ�.");
        Ingredient component0 = mixture.Dequeue();
        Ingredient component1 = mixture.Dequeue();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IAddFunc>() != null)
        {
            AddMixture(other.GetComponent<Ingredient>());
        }
    }
}
