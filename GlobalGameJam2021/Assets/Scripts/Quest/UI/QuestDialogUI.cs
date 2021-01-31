using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDialogUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subjectField = null;
    [SerializeField] private TextMeshProUGUI titleField = null;
    [SerializeField] private TextMeshProUGUI descriptionField = null;
    [SerializeField] private TextMeshProUGUI summaryField = null;
    [SerializeField] private QuestOptionUI optionPrefab = null;
    [SerializeField] private Transform optionListContainer = null;
    [SerializeField] private Image subjectImage = null;

    public void Set(IQuest questData)
    {
        Reset();
        if (subjectField)
            subjectField.text = questData.Subject;
        if (titleField)
            titleField.text = questData.Name;
        if (descriptionField)
            descriptionField.text = questData.Description;
        if (summaryField)
            summaryField.text = questData.Summary;
        if (subjectImage)
        {
            if (questData.SubjectImage)
            {
                subjectImage.sprite = questData.SubjectImage;
                subjectImage.color = Color.white;
            }
            else
                subjectImage.color = Color.clear;
        }
        

        foreach (var option in questData.QuestOptions)
        {
            Instantiate(optionPrefab, optionListContainer).Set(option);
        }
        gameObject.SetActive(true);
    }

    public void Reset()
    {
        foreach (Transform option in optionListContainer)
        {
            Destroy(option.gameObject);
        }
    }
}
