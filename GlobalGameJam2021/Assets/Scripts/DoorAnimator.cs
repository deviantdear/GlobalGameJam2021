using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : QuestOption
{
    [SerializeField] private Animator openAnimation = null;
    
    public override void Select()
    {
        base.Select();
        openAnimation.Play("Open");
    }
}
