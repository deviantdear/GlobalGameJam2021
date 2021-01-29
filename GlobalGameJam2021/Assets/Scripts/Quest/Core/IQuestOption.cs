using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IQuestOption
{
    string Title { get; set; }
    Action OnSelect { get; set; }
}
