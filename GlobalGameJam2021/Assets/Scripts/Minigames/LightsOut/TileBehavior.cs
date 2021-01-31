using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour {
    public bool Active = false;
    public bool[] ValidTargets = new bool[8];
    public int xPos, yPos;
    public TileType Type;

    private GameObject Obj;
    private SpriteRenderer Rendy;

    public enum TileType {
        Blank,
        Circle,
        Diamond,
        Squares,
        Triangle
    }

    // Start is called before the first frame update
    void Start() {
        Obj = this.gameObject;
        Rendy = Obj.GetComponent<SpriteRenderer>();

        switch (Type) {
            case TileType.Circle:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    ValidTargets[i] = true;
                }

                break;

            case TileType.Diamond:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%2 == 1) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                break;

            case TileType.Squares:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%2 == 0) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                break;

            case TileType.Triangle:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%3 == 0) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                break;
        }
    }

    // Update is called once per frame
    void Update() {
        if (Active) {
            Rendy.color = Color.white;
        } else {
            Rendy.color = Color.red;
        }
    }

    void OnMouseDown () {
        if (Type != TileType.Blank) {
            Active = !Active;

            LightsOut.FlipTiles(xPos, yPos, ValidTargets);
        }
    }
}