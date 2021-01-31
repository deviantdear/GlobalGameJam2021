using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeGame : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetButtonDown("Exit"))
            Quit();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
