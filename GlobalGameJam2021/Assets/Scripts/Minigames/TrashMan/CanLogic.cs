using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanLogic : QuestTrigger {
    [SerializeField] private GameObject turnOffOnWin = null;
    [SerializeField] private GameObject overWorld = null;
    [SerializeField] private Transform trashSpawnContainer = null;
    public GameObject Trash;
    public Vector2 trashSpawnRange = new Vector2(16.5f, 9f);
    private GameObject MenuBox;
    private AudioSource AudioS;
    public Sprite EmptySprite, FullSprite;
    public int TotalTrash;
    public bool Victory = false;
    private bool Empty = false, Menu = false;
    private Camera _camera = null;

    // Start is called before the first frame update
    void Start() {
        MenuBox = this.gameObject.transform.GetChild(0).gameObject;
        AudioS = this.GetComponent<AudioSource>();

        if (TotalTrash <= 0) {
            TotalTrash = (int)Random.Range(9, 15);
        }

        for (int i = 0; i < TotalTrash; i++) {
            Vector2 pos = new Vector2(transform.position.x+Random.Range(1, trashSpawnRange.x),
                transform.position.y-Random.Range(1, trashSpawnRange.y));

            GameObject tmp = Instantiate(Trash, pos, Quaternion.identity);
            tmp.transform.SetParent(trashSpawnContainer);
        }
    }

    // Update is called once per frame
    void Update() {
        if (!_camera || !_camera.gameObject.activeInHierarchy)
            _camera = Camera.main;
        
        Vector2 pos = _camera.ScreenToWorldPoint(
            new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - _camera.transform.position.z));
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
            Trigger();
            turnOffOnWin?.SetActive(false);
            overWorld?.SetActive(true);
        }
    }

    public void PlaySound (AudioClip sfx) {
        AudioS.PlayOneShot(sfx);
    }
}