using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanLogic : MonoBehaviour {
    public GameObject Trash;
    public int TotalTrash;
    public bool Victory = false;

    // Start is called before the first frame update
    void Start() {
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
        if (TotalTrash <= 0 && !Victory) {
            Victory = true;
        }
    }
}