using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleField = null;
    [SerializeField] private TextMeshProUGUI summaryField = null;

    public void Set(IQuest data)
    {
        titleField.text = data.Name;
        summaryField.text = data.Summary;
    }
}
