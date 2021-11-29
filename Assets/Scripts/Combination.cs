using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Combination
{
    public static Potion CheckCombi(this Potion value, Ingredient component0, Ingredient component1)
    {
        if (component0.GetComponent<Seed>() != null && component1.GetComponent<Mushroom>() != null)
        {
            value = new DetoxPotion();
            return value;
        }
        else
            return null;
    }
}