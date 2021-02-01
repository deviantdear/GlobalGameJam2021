using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption_SetActive : QuestOption
{
    [SerializeField] private GameObject objectToSet = null;
    [SerializeField] private GameObject objectToSet2 = null;
    [SerializeField] private GameObject objectToSet3 = null;
    [SerializeField] private bool setting = true;
    [SerializeField] private bool setting2 = true;
    [SerializeField] private bool setting3 = true;
    public override void Select()
    {
        base.Select();
        objectToSet?.SetActive(setting);
        objectToSet2?.SetActive(setting2);
        objectToSet3?.SetActive(setting3);
    }
}
