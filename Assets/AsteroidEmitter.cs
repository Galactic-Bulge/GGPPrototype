using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidEmitter : MonoBehaviour {

    [SerializeField]
    private GameObject[] asteroidsPrefabs = new GameObject[4];

    private List<Asteroid> activeAsteroids;

    private float timer = 5;

	// Use this for initialization
	void Start () {
        activeAsteroids = new List<Asteroid>();
	}

    void OnEnable()
    {
        StartCoroutine(EmitterRoutine());
    }
	
    void OnDisable()
    {
        StopCoroutine(EmitterRoutine());
    }

	// Update is called once per frame
	void Update () {
	    
	}

    private IEnumerator EmitterRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(timer);
            GameObject temp = GameObject.Instantiate(asteroidsPrefabs[0]);
            temp.transform.position = transform.position;
            activeAsteroids.Add(temp.GetComponent<Asteroid>());
        }
    }
}
