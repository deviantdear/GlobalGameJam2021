using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption_SetActive : QuestOption
{
    [SerializeField] private GameObject objectToSet = null;
    [SerializeField] private bool setting = true;
    public override void Select()
    {
        base.Select();
        objectToSet.SetActive(setting);
    }
}
