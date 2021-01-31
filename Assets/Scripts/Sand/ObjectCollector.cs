using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ObjectCollector : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI[] itemNumbers;
    [SerializeField]
    TextMeshProUGUI[] totalValue;

    public static ObjectCollector instance;

    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        instance = this;
    }

    public void SetIntemNumber(string text)
    {
        foreach(var item in itemNumbers)
        {
            item.text = text;
        }
    }

    public void SetTotalValue(string text)
    {
        foreach (var item in totalValue)
        {
            item.text = text;
        }
    }
}
