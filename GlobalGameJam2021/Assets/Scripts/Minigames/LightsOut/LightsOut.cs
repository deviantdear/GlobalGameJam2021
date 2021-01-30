using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : MonoBehaviour {
    public List<GameObject> TilesObjs;
    public int Size;
    public GameObject[,] Tiles = new GameObject[64, 64];
    public bool Victory = false;

    public static LightsOut instance;

 private void Awake() {
   if (instance != null) {
     Destroy(gameObject);
   }else{
     instance = this;
   }
 }
    // Start is called before the first frame update
    void Start() {
        for (int i = 0; i < Size; i++) {
            for (int j = 0; j < Size; j++) {
                Tiles[i, j] = TilesObjs[0];
                TilesObjs.RemoveAt(0);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        bool pass = true;

        for (int i = 0; i < Size; i++) { 
            for (int j = 0; j < Size; j++) {
                TileBehavior tmp = Tiles[j, i].GetComponent<TileBehavior>();
                if (!tmp.Active) {
                    pass = false;
                }
            }
        }

        if (pass) {
            Victory = true;
        }
    }
}