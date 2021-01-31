using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption_Destroy : QuestOption
{
    [SerializeField] private GameObject objectToDestroy = null;
    public override void Select()
    {
        base.Select();
        Destroy(objectToDestroy);
    }
}
