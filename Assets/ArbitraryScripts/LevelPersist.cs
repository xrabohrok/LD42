using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPersist : MonoBehaviour
{
    public bool attachToMainCamera;
    public List<string> ValidSceneNames;

    void OnEnable()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }

    void OnDisable()
    {
        SceneManager.activeSceneChanged -= ChangedActiveScene;

    }

    // Use this for initialization
    void Start () {
		
        DontDestroyOnLoad(gameObject);
	}

    private void ChangedActiveScene(Scene current, Scene next)
    {
        var stayAlive = false;
        foreach (var validSceneName in ValidSceneNames)
        {
            stayAlive |= validSceneName.Equals(next.name);
//            Debug.Log("next:" + next.name + " current: " + current.name);
        }

        if (attachToMainCamera)
        {
            this.gameObject.transform.parent = Camera.main.transform;
        }

        if (!stayAlive)
        {
            Destroy(gameObject);
        }
    }


}
