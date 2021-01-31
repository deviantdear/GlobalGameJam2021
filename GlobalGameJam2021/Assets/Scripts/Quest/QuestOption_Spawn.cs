using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestOption_Spawn : QuestOption
{
    [SerializeField] private GameObject spawnablePrefab = null;

    [SerializeField] private Transform prefabContainer = null;
    public override void Select()
    {
        base.Select();
        Instantiate(spawnablePrefab, prefabContainer);
    }
}
