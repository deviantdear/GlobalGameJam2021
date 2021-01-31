using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_Collider : QuestTrigger
{
    [SerializeField] private string triggerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered");
        if (other.CompareTag(triggerTag)) 
            Trigger();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        Debug.Log("Triggered collison 2d");
        if (other.collider.CompareTag(triggerTag)) 
            Trigger();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggerd 2D");
        if (other.CompareTag(triggerTag)) 
            Trigger();
    }
}