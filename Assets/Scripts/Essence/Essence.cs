using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour, IMixFunc
{
    public static Queue<Ingredient> ingredient;
    const int maxAmount = 2;

    public Dictionary<Ingredient[], Potion> combination;
    public Potion potion;

    void Start()
    {
        ingredient = new Queue<Ingredient>();
        Clear();

        combination = new Dictionary<Ingredient[], Potion>();
        SetCombination();

        potion = null;
    }

    public void SetCombination()
    {
        Bone bone = new Bone();
        Crystal crystal = new Crystal();
        Flower flower = new Flower();
        Mushroom mushroom = new Mushroom();
        Seed seed = new Seed();

        DetoxPotion detoxPotion = new DetoxPotion();
        DeburnPotion deburnPotion = new DeburnPotion();
        DeparalysePotion deparalysePotion = new DeparalysePotion();
        ExplodePotion explodePotion = new ExplodePotion();

        Ingredient[] temp = new Ingredient[2];
        for (int i = 0; i < 4; i++)
        {
            temp[0] = bone;
            temp[1] = flower;
            combination.Add(temp, detoxPotion);
        }
    }

    public void Mix()
    {
        Debug.Log("����");
        Ingredient[] temp = new Ingredient[2];
        temp[0] = ingredient.Dequeue();
        temp[1] = ingredient.Dequeue();
        potion = combination[temp];
        Debug.Log("������ ����");

        Clear();
    }

    public void Clear()
    {
        while (ingredient.Count != 0)
            ingredient.Dequeue();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("���� ����");
        if (other.GetComponent<IAddFunc>() != null)
        {
            Debug.Log("����� �ν�");
            if (ingredient.Count < maxAmount)
            {
                Debug.Log("ť�� ���� �غ�");
                ingredient.Enqueue(other.GetComponent<IAddFunc>().Add());
                Debug.Log("ť�� ����");
                if (ingredient.Count == maxAmount)
                {
                    Debug.Log("ť�� ����");
                    Mix();
                }
            }
            else
                return;
        }
    }

    public void Success()
    {

    }

    public void Fail()
    {

    }
}
