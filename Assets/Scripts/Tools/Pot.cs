using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Recipe
{
    [Tooltip("Result: ����, Ingredients: ���")]
    public GameObject result;
    public List<Ingredient> ingredients;
}

public class Pot : Singleton<Pot>
{
    #region Audio Clips
    [Tooltip("0: DEBURN\n1: DEPARALYSE\n2: DETOX\n3: EXPLODE")]
    public AudioClip[] resultGoodClips;
    enum POTION_TYPE
    {
        DEBURN, DEPARALYSE, DETOX, EXPLODE
    }
    Dictionary<GameObject, AudioClip> clipDic = new Dictionary<GameObject, AudioClip>();
    public AudioClip[] resultFailClips;
    public AudioClip loopBoilingClip;
    #endregion

    #region Recipes
    public Transform createSpot;
    public List<Recipe> recipes;

    List<string> potList = new List<string>();
    List<List<string>> recipeList = new List<List<string>>();
    Dictionary<List<string>, GameObject> resultDic = new Dictionary<List<string>, GameObject>();
    #endregion

    void Start()
    {
        for (int i = 0; i < recipes.Count; i++)
            CreateRecipe(recipes[i].result, recipes[i].ingredients, i);
    }

    public void CreateRecipe(GameObject resultObj, List<Ingredient> ingredients, int i)
    {
        List<string> tempList = new List<string>();
        for (int j = 0; j < ingredients.Count; j++)
            tempList.Add(ingredients[j].ingredientName);
        recipeList.Add(tempList);
        resultDic.Add(tempList, resultObj);
        clipDic.Add(resultObj, resultGoodClips[i]);
    }

    public void CombineRecipe()
    {
        bool isPossible = true;

        int count = 0;

        foreach (List<string> currentRecipe in recipeList)
        {
            Debug.Log(count + "�� �������� ��� ��: " + currentRecipe.Count);
            Debug.Log("���Ե� ��� ��: " + potList.Count);

            if (currentRecipe.Count != potList.Count)
                continue;

            foreach (string ingredient in currentRecipe)
                Debug.Log(count + "�� �������� ���: " + ingredient);

            foreach (string ingredient in potList)
            {
                Debug.Log("���Ե� ���: " + ingredient);
                isPossible &= currentRecipe.Contains(ingredient);
                Debug.Log(isPossible);
            }

            if (isPossible)
            {
                Instantiate(resultDic[currentRecipe], createSpot);
                Debug.Log(resultDic[currentRecipe].name + "���� ���� ����!");
                SoundController.instance.sources[0].PlayOneShot(clipDic[resultDic[currentRecipe]]);
                potList.Clear();
                return;
            }

            isPossible = true;
            count++;
        }

        Debug.Log("���� ����... ���� �����.");
        SoundController.instance.sources[0].PlayOneShot(resultFailClips[Random.Range(0, resultFailClips.Length)]);
        potList.Clear();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ingredient>() != null)
        {
            if (other.GetComponent<OVRGrabbable>().isGrabbed == false)
            {
                string content = other.GetComponent<Ingredient>().ingredientName;
                if (potList.Contains(content) == false)
                {
                    potList.Add(content);
                    Debug.Log(content + "�� �����ߴ�.");
                    other.gameObject.SetActive(false);
                    StartCoroutine(other.GetComponent<Ingredient>().ReturnToSpawner());
                }
            }
        }

        if (other.GetComponent<IMixFunc>() != null)
            CombineRecipe();
    }
}
