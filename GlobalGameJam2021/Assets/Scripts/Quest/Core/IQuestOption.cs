using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestOption
{
    /// <summary>
    /// Text given to player when they pick this as the quest option
    /// </summary>
    string Title { get; set; }
    /// <summary>
    /// Action to run when selected
    /// </summary>
    Action OnSelect { get; set; }
    /// <summary>
    /// Quest this is attached to
    /// </summary>
    IQuest Quest { get; set; }
    /// <summary>
    /// Make available to the quest
    /// </summary>
    bool Available { get; set; }
    /// <summary>
    /// Select this quest option
    /// </summary>
    void Select();
}
