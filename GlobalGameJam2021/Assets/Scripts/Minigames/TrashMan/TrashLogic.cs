using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashLogic : MonoBehaviour {
    public AudioClip SFX;
    private bool grabbed = false;
    private int Width = 32, Height = 32;
    private GameObject Can;
    public Sprite[] Sprites;

    // Start is called before the first frame update
    void Start() {
        Can = this.transform.parent.gameObject;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length-1)];
    }

    // Update is called once per frame
    void Update() {
        if (grabbed) {
            Vector2 pos = Camera.main.ScreenToWorldPoint(
                new Vector3(Mathf.Clamp(Input.mousePosition.x, Width, 1920-Width),
                Mathf.Clamp(Input.mousePosition.y, Height, 1080-Height), Camera.main.nearClipPlane));
            transform.position = pos;
        } else {
            Vector2 pos = new Vector2(Mathf.Clamp(transform.position.x, Can.transform.position.x-0.5f,
                Can.transform.position.x+16.5f), Mathf.Clamp(transform.position.y,
                Can.transform.position.y-8.5f, Can.transform.position.y+0.5f));
            transform.position = pos;
        }
    }

    private void OnMouseDown() {
        grabbed = true;
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