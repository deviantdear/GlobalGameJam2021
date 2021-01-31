using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMenu : MonoBehaviour {
    public bool Clicked = false;

    private void OnMouseDown() {
        Clicked = true;
    }
}