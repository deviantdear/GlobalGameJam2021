using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour
{
    private List<GameObject> initalState = null;

    [SerializeField] private Quest quest = null;
    // Start is called before the first frame update
    void Start()
    {
        initalState = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
        quest.OnActive += ResetRoot;
    }

    public void ResetRoot(bool activeState)
    {
        var currentState = new List<GameObject>(SceneManager.GetActiveScene().GetRootGameObjects());
        foreach (var item in currentState)
        {
            if (!initalState.Contains(item))
                Destroy(item);
        }
    }
}
