using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform followObject = null;
    private void Update()
    {
        transform.position = new Vector3(followObject.position.x, followObject.position.y, transform.position.z);
    }
}
