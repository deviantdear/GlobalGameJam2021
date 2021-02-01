using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanLogic : MonoBehaviour {
    public GameObject Trash;
    private GameObject MenuBox;
    private AudioSource AudioS;
    public Sprite EmptySprite, FullSprite;
    public int TotalTrash;
    public bool Victory = false;
    private bool Empty = false, Menu = false;

    // Start is called before the first frame update
    void Start() {
        MenuBox = this.gameObject.transform.GetChild(0).gameObject;
        AudioS = this.GetComponent<AudioSource>();

        if (TotalTrash <= 0) {
            TotalTrash = (int)Random.Range(9, 15);
        }

        for (int i = 0; i < TotalTrash; i++) {
            Vector2 pos = new Vector2(transform.position.x+Random.Range(1, 16.5f),
                transform.position.y-Random.Range(1, 9));

            GameObject tmp = Instantiate(Trash, pos, Quaternion.identity);
            tmp.transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update() {
        Vector2 pos = Camera.main.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        float dist = Vector2.Distance(pos, transform.position);

        if (TotalTrash <= 0 && !Victory && Empty) {
            Victory = true;
        } else if (TotalTrash <= 0 && Input.GetMouseButtonDown(1) && dist < 0.5f && !Empty && !Menu) {
            Menu = true;

        } else if (Input.GetMouseButtonDown(0) && dist > 2f && Menu) {
            Menu = false;
        } else if (Menu && !MenuBox.GetComponent<TrashMenu>().Clicked) {
            MenuBox.SetActive(true);
        } else if (Menu && MenuBox.GetComponent<TrashMenu>().Clicked) {
            Menu = false;
            Empty = true;
            Victory = true;
            MenuBox.SetActive(false);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = EmptySprite;
            AudioS.Play();
        }  else {
            MenuBox.SetActive(false);
        }

        if (TotalTrash <= 0 && !Victory) {
           this.gameObject.GetComponent<SpriteRenderer>().sprite = FullSprite;
        } else if (Victory) {
            // Delete it or something idk
        }
    }

    public void PlaySound (AudioClip sfx) {
        AudioS.PlayOneShot(sfx);
    }
}