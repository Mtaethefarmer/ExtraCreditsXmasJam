using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour {

    public GameObject[] Bases;

    public GameObject[] Attachments;

    public float spawnInterval = 2.0f;

    public Vector3 spawnPosition = new Vector3();

    private float timeSinceLastSpawn = 0.0f;

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        timeSinceLastSpawn += Time.deltaTime;

        if(timeSinceLastSpawn > spawnInterval)
        {
            SpawnComponent();

            timeSinceLastSpawn = 0.0f;
        }

	}

    private void SpawnComponent()
    {
        // Pick randomly whether to spawn a base or attachment
        if(Random.Range(0, 2) == 1)
        {
            Instantiate(Bases[Random.Range(0, Bases.Length)], spawnPosition, new Quaternion());
        }
        else
        {
            Instantiate(Attachments[Random.Range(0, Bases.Length)], spawnPosition, new Quaternion());
        }
    }
}
