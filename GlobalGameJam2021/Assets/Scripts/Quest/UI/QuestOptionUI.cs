using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestOptionUI : MonoBehaviour
{
    [SerializeField] private Button button = null;
    [SerializeField] private TextMeshProUGUI label = null;

    private IQuestOption data;
    
    public void Set(IQuestOption data)
    {
        this.data = data;
        label.text = data.Title;
        button.onClick.AddListener(data.Select);
        Debug.Log("Adding this", this);
    }

    private void OnDestroy()
    {
        if (button && data != null)
            button.onClick.RemoveListener(data.Select);
    }
}
