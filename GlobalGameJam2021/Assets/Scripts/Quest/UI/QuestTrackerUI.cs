using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrackerUI : MonoBehaviour
{
    [SerializeField] private QuestItemUI questItemUIPrefab = null;
    [SerializeField] private Transform activeQuestList = null;

    private List<GameObject> questItemUIs = new List<GameObject>();
    
    
    
}
