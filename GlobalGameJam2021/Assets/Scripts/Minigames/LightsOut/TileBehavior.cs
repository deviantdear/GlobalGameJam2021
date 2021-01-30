using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehavior : MonoBehaviour {
    public bool Active = false;
    public int xPos, yPos;
    public TileType Type;

    private GameObject Controller, Obj;
    private SpriteRenderer Rendy;
    private LightsOut Board;
    public List<GameObject> Targets;
    //private float Scale = 1;
    private bool[] ValidTargets = new bool[8];

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

        //Controller = GameObject.Find("LightsOutController");
        Board = LightsOut.instance.GetComponent<LightsOut>();

        switch (Type) {
            case TileType.Circle:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    ValidTargets[i] = true;
                }

                getTargets(ValidTargets);
                break;

            case TileType.Diamond:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%2 == 1) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                getTargets(ValidTargets);
                break;

            case TileType.Squares:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%2 == 0) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                getTargets(ValidTargets);
                break;

            case TileType.Triangle:
                for (int i = 0; i < ValidTargets.Length; i++) {
                    if (i%3 == 0) {
                        ValidTargets[i] = true;
                    } else {
                        ValidTargets[i] = false;
                    }
                }

                getTargets(ValidTargets);
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
        Active = !Active;

        foreach (GameObject tile in Targets) {
            TileBehavior tmp = tile.GetComponent<TileBehavior>();
            tmp.Active = !tmp.Active;
        }
    }

    private void getTargets(bool[] targets) {
        if (yPos-1 >= 0 && targets[0]) { // Top
            Targets.Add(Board.Tiles[xPos,yPos-1]);
        }
        if (xPos+1 < Board.Size-1 && yPos-1 >= 0 && targets[1]) { // Top Right
            Targets.Add(Board.Tiles[xPos+1,yPos-1]);
        }
        if (xPos+1 < Board.Size-1 && targets[2]) { // Right
            Targets.Add(Board.Tiles[xPos+1,yPos]);
        }
        if (xPos+1 < Board.Size-1 && yPos+1 < Board.Size-1 && targets[3]) { // Bottom Right
            Targets.Add(Board.Tiles[xPos+1,yPos+1]);
        }
        if (yPos+1 < Board.Size-1 && targets[4]) { // Bottom
            Targets.Add(Board.Tiles[xPos,yPos+1]);
        }
        if (xPos-1 >= 0 && yPos < Board.Size-1 && targets[5]) { // Bottom Left
            Targets.Add(Board.Tiles[xPos-1,yPos+1]);
        }
        if (xPos-1 >= 0 && targets[6]) { // Left
            Targets.Add(Board.Tiles[xPos-1,yPos]);
        }
        if (xPos-1 >= 0 && yPos-1 >= 0 && targets[7]) { // Top Left
            Targets.Add(Board.Tiles[xPos-1,yPos-1]);
        }
    }
}