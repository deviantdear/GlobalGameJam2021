using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_UseInProximity : QuestTrigger
{
    [SerializeField] private string triggerTag = "player";
    [SerializeField] private string useKey = "E";
    private bool isInProximity = false;

    private void Update()
    {
        if (isInProximity && Input.GetButtonDown(useKey))
        {
            Trigger();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(triggerTag))
            isInProximity = true;
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag(triggerTag))
            isInProximity = false;
    }
}
