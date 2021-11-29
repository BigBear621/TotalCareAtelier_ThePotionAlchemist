using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientPool : Singleton<IngredientPool>
{
    [HideInInspector]
    public GameObject[] bones, crystals, eggs, flowers, mushrooms, seeds;
    public List<GameObject[]> ingredientList;

    [HideInInspector]
    public Queue<GameObject> bonePool, crystalPool, eggPool, flowerPool, mushroomPool, seedPool;
    public List<Queue<GameObject>> poolList;

    [HideInInspector]
    public Transform bonePos, crystalPos, eggPos, flowerPos, mushroomPos, seedPos;
    [HideInInspector]
    public List<Transform> posList;

    const int maxSize = 10;

    void Start()
    {
        LoadResources();
        AssemblePools();
        AssignPoolPos();
        ProduceObject();

        ActivateAll();
    }

    #region Setting Object Pool
    void LoadResources()
    {
        bones = Resources.LoadAll<GameObject>("Ingredient/Bone/");
        crystals = Resources.LoadAll<GameObject>("Ingredient/Crystal/");
        eggs = Resources.LoadAll<GameObject>("Ingredient/Egg/");
        flowers = Resources.LoadAll<GameObject>("Ingredient/Flower/");
        mushrooms = Resources.LoadAll<GameObject>("Ingredient/Mushroom/");
        seeds = Resources.LoadAll<GameObject>("Ingredient/Seed/");

        ingredientList = new List<GameObject[]>();

        ingredientList.Add(bones);
        ingredientList.Add(crystals);
        ingredientList.Add(eggs);
        ingredientList.Add(flowers);
        ingredientList.Add(mushrooms);
        ingredientList.Add(seeds);

        Debug.Log("���� ������ ����: " + ingredientList.Count);
        for (int i = 0; i < ingredientList.Count; i++)
        {
            Debug.Log(i + "��° ����� ��: " + ingredientList[i].Length);
            for (int j = 0; j < ingredientList[i].Length; j++)
            {
                Debug.Log(ingredientList[i][j].name);
            }
        }
        Debug.Log("----------���� ��� ���� �Ϸ�----------");
    }

    void AssemblePools()
    {
        bonePool = new Queue<GameObject>();
        crystalPool = new Queue<GameObject>();
        eggPool = new Queue<GameObject>();
        flowerPool = new Queue<GameObject>();
        mushroomPool = new Queue<GameObject>();
        seedPool = new Queue<GameObject>();

        poolList = new List<Queue<GameObject>>();

        poolList.Add(bonePool);
        poolList.Add(crystalPool);
        poolList.Add(eggPool);
        poolList.Add(flowerPool);
        poolList.Add(mushroomPool);
        poolList.Add(seedPool);

        Debug.Log("������Ʈ Ǯ�� ����: " + poolList.Count);
        Debug.Log("----------������Ʈ Ǯ ���� �Ϸ�----------");
    }

    void AssignPoolPos()
    {
        posList = new List<Transform>();

        posList.Add(bonePos);
        posList.Add(crystalPos);
        posList.Add(eggPos);
        posList.Add(flowerPos);
        posList.Add(mushroomPos);
        posList.Add(seedPos);

        for (int i = 0; i < posList.Count; i++)
        {
            posList[i] = transform.GetChild(i);
            Debug.Log(posList[i].name + "�� Ǯ ��ġ ����");
        }
        Debug.Log("----------������Ʈ Ǯ ��ġ ���� �Ϸ�----------");
    }

    void ProduceObject()
    {
        for (int i = 0; i < ingredientList.Count; i++)                  // List<GameObject[]>
        {
            while (poolList[i].Count < maxSize)
            {
                for (int j = 0; j < ingredientList[i].Length; j++)          // GameObject[]
                {
                    GameObject temp = Instantiate(ingredientList[i][j], posList[i]);
                    RePosition(temp.transform, posList[i]);
                    temp.SetActive(false);
                    poolList[i].Enqueue(temp);
                }
            }
        }
        Debug.Log("----------������Ʈ ���� �� ��ġ �Ϸ�----------");
    }
    #endregion

    public void RePosition(Transform current, Transform target)
    {
        current.position = target.position;
        current.rotation = target.rotation;
    }

    public void ActivateAll()
    {
        for (int i = 0; i < poolList.Count; i++)
        {
            GameObject temp = poolList[i].Dequeue();
            temp.SetActive(true);
            poolList[i].Enqueue(temp);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IAddFunc>() != null)
        {
            if (OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))
            {
            }
        }
    }


}
