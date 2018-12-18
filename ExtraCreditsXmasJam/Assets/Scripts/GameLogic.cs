using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScoreUpdateEvent : UnityEvent<int>
{}

[System.Serializable]
public class TimeUpdateEvent : UnityEvent<float>
{ }

public class GameLogic : MonoBehaviour {

    public static GameLogic instance;

    public GameObject[] Bases;

    public GameObject[] Attachments;

    public float spawnInterval = 2.0f;

    public Vector3 baseSpawnPosition = new Vector3();

    public Vector3 attachmentSpawnPosition = new Vector3();

    public float maxTime = 180.0f;

    public ScoreUpdateEvent scoreUpdate = new ScoreUpdateEvent();

    public TimeUpdateEvent timeUpdate = new TimeUpdateEvent();

    private float timeSinceLastSpawn = 10.0f;

    private float time = 0.0f;

    private int score = 0;

    private void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

            // Reset the score, assuming this state only happens on game start.
            score = 0;
        }

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        time = maxTime;

        if (scoreUpdate == null)
            scoreUpdate = new ScoreUpdateEvent();
        if (timeUpdate == null)
            timeUpdate = new TimeUpdateEvent();

        SpawnBase();
    }
	
	// Update is called once per frame
	void Update () {

        timeSinceLastSpawn += Time.deltaTime;

        time -= Time.deltaTime;

        timeUpdate.Invoke(time);

        if(timeSinceLastSpawn > spawnInterval)
        {
            SpawnAttachment();

            timeSinceLastSpawn = 0.0f;
        }

	}

    public int Score()
    {
        return score;
    }

    private void SpawnBase()
    {
        Instantiate(Bases[UnityEngine.Random.Range(0, Bases.Length)], baseSpawnPosition, new Quaternion());
    }

    private void SpawnAttachment()
    {
        Instantiate(Attachments[UnityEngine.Random.Range(0, Attachments.Length)], attachmentSpawnPosition, new Quaternion());
    }

    public void SubmitToy(GameObject obj)
    {
        score += RateToy(obj);

        Destroy(obj);

        scoreUpdate.Invoke(score);

        // Spawn new base
        SpawnBase();
    }

    private int RateToy(GameObject obj)
    {
        int total = 0;

        // Check base
        ToyComponent toydata = obj.GetComponent<ToyComponent>();

        // Return 0 if passed an object without toy data
        if (!toydata)
        {
            Debug.Log("Passed object without ToyComponent");
            return 0;
        }

        // Create integer array, with one value for each member of ComponentTheme.
        // Each index of enum will correspond to its number of items.
        int[] themeCounts = new int[Enum.GetNames(typeof(ComponentTheme)).Length];

        for (int i = 0; i < themeCounts.Length; i++)
        {
            // Initialize array because I can't find an answer to if it is initialized
            // by default, or how to initialize a dynamically sized array.
            themeCounts[i] = 0;
        }
        
        // Get all toycomponents on current object, and children
        ToyComponent[] children = obj.GetComponentsInChildren<ToyComponent>();

        int AttachmentCount = -1;

        // Add themes of components
        foreach(ToyComponent data in children)
        {
            AttachmentCount++;
            themeCounts[(int)data.compTheme]++;
        }

        // Add points based on the themes found
        for(int i = 0; i < themeCounts.Length; i++)
        {
            // Get 100 points for each theme after the first instance
            if(themeCounts[i] != 0)
                total += (themeCounts[i] - 1) * 100;
        }

        // Add random bonus proportional to attachment count, to
        // make bases with few attachments a little better.
        if(AttachmentCount > 0)
            total += (int)(UnityEngine.Random.Range(200.0f, 400.0f) * ((float)1 / AttachmentCount));

        return total;
    }
}
