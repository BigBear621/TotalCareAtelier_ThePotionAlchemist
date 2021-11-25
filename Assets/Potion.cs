using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour,IGrabable
{
    public string potionName;
    public int price;
    public string potionColor;

    public Transform Grab(Transform grabber)
    {
        transform.parent = grabber.transform;
        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = true;
        return transform;
    }

    public void Release()
    {
        if (GetComponent<Rigidbody>() != null)
            GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;
    }
}
