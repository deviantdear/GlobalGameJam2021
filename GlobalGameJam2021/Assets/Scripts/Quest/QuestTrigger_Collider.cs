using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Collider : QuestTrigger
{
    [SerializeField] private string triggerTag = "player";
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag(triggerTag)) 
            Trigger();
    }
}