using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TrashLogic : MonoBehaviour {
    public AudioClip SFX;
    [SerializeField] bool grabbed = false;
    private int Width = 32, Height = 32;
    private GameObject Can;
    public Sprite[] Sprites;
    public Camera camera;
    
    private Vector3 homePosition;
    private bool returned = true;

    // Start is called before the first frame update
    void Start() {
        Can = this.transform.parent.gameObject;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
        homePosition = transform.position;
    }

    void Update()
    {
        if (!camera || !camera.gameObject.activeInHierarchy)
            camera = Camera.main;
        
        if (grabbed)
        {
            Vector2 pos = camera.ScreenToWorldPoint(
                          new Vector3(Mathf.Clamp(Input.mousePosition.x, 0, Screen.width),
                          Mathf.Clamp(Input.mousePosition.y, 0,Screen.height), transform.position.z - camera.transform.position.z));
            
            transform.position = pos;
        } else if (!returned)
            StartCoroutine(DropTimer());
    }

    IEnumerator DropTimer()
    {
        yield return new WaitForSeconds(1f);
        if (!grabbed)
            transform.position = homePosition;
        returned = true;
    }

    private void OnMouseDown() {
        grabbed = true;
        returned = false;
    }

    private void OnMouseUp() {
        grabbed = false;
    }

    private void OnTriggerStay2D(Collider2D collision) {
        GameObject other = collision.gameObject;
        
        if (other.CompareTag("TrashCan") && !grabbed) {
            other.GetComponent<CanLogic>().PlaySound(SFX);
            CanLogic canLogic = other.GetComponent<CanLogic>();
            canLogic.TotalTrash--;
            Destroy(this.gameObject);
        }
    }
}