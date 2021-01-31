using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestTrigger_UseInProximity : QuestTrigger
{
    [SerializeField] private string triggerTag = "Player";
    [SerializeField] private string useKey = "Interact";
    private bool isInProximity = false;

    private void Update()
    {
        if (isInProximity && Input.GetButtonDown(useKey))
        {
            Trigger();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))
            isInProximity = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(triggerTag))
            isInProximity = false;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
        if (other.collider.CompareTag(triggerTag))
            isInProximity = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.collider.CompareTag(triggerTag))
            isInProximity = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
            isInProximity = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(triggerTag))
            isInProximity = false;
    }
}
