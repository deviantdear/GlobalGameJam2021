using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsOut : QuestTrigger {
    public List<GameObject> TilesObjs;
    public int SizeX, SizeY;
    public GameObject[,] Tiles;
    public bool Victory = false, Failure = false;
    public float TimeLimit;

    public static LightsOut instance;
    [SerializeField] private GameObject turnOffOnWin = null;
    [SerializeField] private GameObject overWorld = null;
    private AudioSource AudioS;

    private void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start() {
        Tiles = new GameObject[SizeX, SizeY];
        AudioS = this.GetComponent<AudioSource>();
        int size = SizeX*SizeY;

        for (int i = 0; i < size; i++) {
            TilesObjs.Add(this.gameObject.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < SizeX; i++) {
            for (int j = 0; j < SizeY; j++) {
                GameObject tmpObj = TilesObjs[0];
                TileBehavior tmp = tmpObj.GetComponent<TileBehavior>();
                tmp.xPos = i;
                tmp.yPos = j;

                Tiles[i, j] = tmpObj;
                TilesObjs.RemoveAt(0);
            }
        }
    }

    // Update is called once per frame
    void Update() {
        bool pass = true;

        for (int i = 0; i < SizeX; i++) { 
            for (int j = 0; j < SizeY; j++) {
                TileBehavior tmp = Tiles[j, i].GetComponent<TileBehavior>();
                if (!tmp.Active) {
                    pass = false;
                }
            }
        }

        if (pass && !Victory) {
            AudioS.Play();
            Victory = true;
        }

        if (!Victory && !Failure) {
            TimeLimit = TimeLimit-Time.deltaTime < 0 ? 0 : TimeLimit-Time.deltaTime;

            if (TimeLimit == 0) {
                Failure = true;
            }
        }

        if (Victory || Failure)
        {
            Trigger();
            turnOffOnWin?.SetActive(false);
            overWorld?.SetActive(true);
        }
    }

    public static void FlipTiles(int yPos, int xPos, bool[] targets) {
        List<GameObject> Targets = new List<GameObject>();

        if (yPos-1 >= 0 && targets[0]) { // Top
            Targets.Add(LightsOut.instance.Tiles[yPos-1,xPos]);
        }
        if (xPos+1 < LightsOut.instance.SizeX && yPos-1 >= 0 && targets[1]) { // Top Right
            Targets.Add(LightsOut.instance.Tiles[yPos-1,xPos+1]);
        }
        if (xPos+1 < LightsOut.instance.SizeX && targets[2]) { // Right
            Targets.Add(LightsOut.instance.Tiles[yPos,xPos+1]);
        }
        if (xPos+1 < LightsOut.instance.SizeX && yPos+1 < LightsOut.instance.SizeY && targets[3]) { // Bottom Right
            Targets.Add(LightsOut.instance.Tiles[yPos+1,xPos+1]);
        }
        if (yPos+1 < LightsOut.instance.SizeY && targets[4]) { // Bottom
            Targets.Add(LightsOut.instance.Tiles[yPos+1,xPos]);
        }
        if (xPos-1 >= 0 && yPos+1 < LightsOut.instance.SizeY && targets[5]) { // Bottom Left
            Targets.Add(LightsOut.instance.Tiles[yPos+1,xPos-1]);
        }
        if (xPos-1 >= 0 && targets[6]) { // Left
            Targets.Add(LightsOut.instance.Tiles[yPos,xPos-1]);
        }
        if (xPos-1 >= 0 && yPos-1 >= 0 && targets[7]) { // Top Left
            Targets.Add(LightsOut.instance.Tiles[yPos-1,xPos-1]);
        }

        foreach (GameObject tile in Targets) {
            TileBehavior tmp = tile.GetComponent<TileBehavior>();
            tmp.Active = !tmp.Active;
        }
    }
}