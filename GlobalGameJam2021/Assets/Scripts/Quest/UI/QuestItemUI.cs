using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleField = null;
    [SerializeField] private TextMeshProUGUI summaryField = null;
    [SerializeField] private Transform compassPointer = null;
    private Transform trackedObject = null;
    private Transform playerTransform = null;

    public void Set(IQuest data)
    {
        titleField.text = data.Name;
        summaryField.text = data.Summary;
        trackedObject = data.SubjectTransform;
        // Turn on the compass if it exists, and the tracked object is set.
        if (compassPointer)
        {
            playerTransform = GameObject.FindWithTag("Player")?.transform;
            compassPointer.gameObject.SetActive(trackedObject != null);
        }
    }

    private Vector3 lastAngle;
    public void Update()
    {
        if (!compassPointer || !trackedObject)
            return;
        // Aim the pointer at the subject
        var vectorTo = playerTransform.position - trackedObject.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 180) * vectorTo;
        Quaternion targetRotation = Quaternion.LookRotation(Vector3.forward, rotatedVectorToTarget);
        compassPointer.rotation = Quaternion.RotateTowards(compassPointer.rotation, targetRotation, 90f * Time.deltaTime);
    }
}
