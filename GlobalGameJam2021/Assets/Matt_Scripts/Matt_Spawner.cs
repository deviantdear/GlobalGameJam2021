using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Matt_Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstacles; // 0 = flying, 1 = ground

    [SerializeField] int leastDifficultPrefab;
    [SerializeField] int mostDifficultPrefab;

    int tiersToIncreasePrefabDifficulty;
    [SerializeField] private GameObject winObject;
    [SerializeField] private Matt_Controller gameController;
    [SerializeField] private GameObject cloud;

    [SerializeField] private float tickCount;

    private float ticksBetweenSpawns;
    private float minimumTicksBetweenSpawns; // hardest speed

    private bool spawnedMaxObjects; // if the spawner has created enough objects to reach the score needed to win

    private int prefabsSpawnedTotal;
    private int prefabsSpawnedSinceChange;
    private int prefabsToIncreaseSpeed;

    private int obstaclesSpawnedTotal;
    private int obstaclesSpawnedSinceChange;
    private int objectsToIncreaseSpeed;

    bool maxObjects;
    bool winSpawned;



    public void Initialize()
    {
        leastDifficultPrefab = 0;
        mostDifficultPrefab = 3;

        tiersToIncreasePrefabDifficulty = 0;

        ticksBetweenSpawns = 2.5f;
        minimumTicksBetweenSpawns = 1f; // hardest speed

        tickCount = ticksBetweenSpawns - 2f; // spwns the first object nearly immediately

        spawnedMaxObjects = false; // if the spawner has created enough objects to reach the score needed to win

        prefabsSpawnedTotal = 0;
        prefabsSpawnedSinceChange = 0;
        prefabsToIncreaseSpeed = 5;

        obstaclesSpawnedTotal = 0;
        obstaclesSpawnedSinceChange = 0;
        objectsToIncreaseSpeed = 5;

        maxObjects = false;
        winSpawned = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        gameController = GameObject.FindGameObjectWithTag("Matt_Controller").GetComponent<Matt_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.GetGameActive())
        {
            if (gameController.GetGameActive() && winSpawned == false)
            {
                tickCount += Time.deltaTime;
            }

            if (tickCount > ticksBetweenSpawns && !maxObjects)
            {
                //int obstacleType = Random.Range(0, 5); // I want ground traps to be more common than flying ones, 0/1 = flying, 2/3/4 = ground

                //if (obstacleType == 0 || obstacleType == 1)
                //{
                //    Instantiate(obstacles[0], new Vector3(this.gameObject.transform.position.x, flyingHeight, 0f), Quaternion.identity);
                //}
                //else if (obstacleType == 2 || obstacleType == 3 || obstacleType == 4)
                //{
                //    Instantiate(obstacles[1], new Vector3(this.gameObject.transform.position.x, groundHeight, 0f), Quaternion.identity);
                //}

                int obstacleType = Random.Range(leastDifficultPrefab, mostDifficultPrefab);

                GameObject obstacleSpawned = obstacles[obstacleType];

                int cloudSpawnChance = Random.Range(0, 5);
                if (cloudSpawnChance == 0)
                {
                    float cloudHeight = Random.Range(7.5f, 12.0f);
                    Instantiate(cloud, new Vector3(this.gameObject.transform.position.x, cloudHeight, 0f), Quaternion.identity);
                }

                Instantiate(obstacleSpawned, new Vector3(this.gameObject.transform.position.x, 0f, 0f), Quaternion.identity);

                prefabsSpawnedTotal++;
                prefabsSpawnedSinceChange++;

                int obstaclesInPrefab = 0;

                var children = obstacleSpawned.transform.root.GetComponentsInChildren<Transform>();
                obstaclesInPrefab = children.Length - 1;

                //Debug.Log("Obstacles Spawned: " + obstaclesInPrefab);

                //foreach (var child in children)
                //{
                //    obstaclesInPrefab++;
                //}

                obstaclesSpawnedTotal += obstaclesInPrefab;
                obstaclesSpawnedSinceChange += obstaclesInPrefab;

                maxObjects = CheckForMaxObjects();

                tickCount = 0;

                //if (obstaclesSpawnedSinceChange > objectsToIncreaseSpeed && ticksBetweenSpawns > minimumTicksBetweenSpawns)
                if (prefabsSpawnedSinceChange > prefabsToIncreaseSpeed && ticksBetweenSpawns > minimumTicksBetweenSpawns)
                {
                    obstaclesSpawnedSinceChange = 0;
                    prefabsSpawnedSinceChange = 0;
                    ticksBetweenSpawns -= (int) ((float)ticksBetweenSpawns * .1f); // decrease time between spawns by 10% til minimum reached
                    
                    if (ticksBetweenSpawns < minimumTicksBetweenSpawns) { ticksBetweenSpawns = minimumTicksBetweenSpawns; }

                    if (mostDifficultPrefab <= obstacles.Length)
                    {
                        tiersToIncreasePrefabDifficulty++;

                        if (tiersToIncreasePrefabDifficulty % 2 == 0)
                        {
                            mostDifficultPrefab++;
                        }

                        if (tiersToIncreasePrefabDifficulty % 3 == 0)
                        {
                            if (leastDifficultPrefab < mostDifficultPrefab - 2)
                            {
                                leastDifficultPrefab++;
                            }
                        }
                    }

                    //Debug.Log("Decreased ticks between spawns to: " + ticksBetweenSpawns);

                    //objectsToIncreaseSpeed = (int)(objectsToIncreaseSpeed + (float)objectsToIncreaseSpeed % .1f); // increase obstacles needed to reach next difficulty
                }
            }
            else if (tickCount > ticksBetweenSpawns * 2 && maxObjects && winSpawned == false)
            {
                Instantiate(winObject, new Vector3(this.gameObject.transform.position.x, 0f, 0f), Quaternion.identity);
                winSpawned = true;
            }

        }

    }

    bool CheckForMaxObjects()
    {
        if (obstaclesSpawnedTotal >= gameController.GetScoreToWin() / gameController.GetObstacleScoreValue())
        {       
            return true; // game has spawned enough obstacles for player to win
        }

        return false;

    }

}
