using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidEmitter : MonoBehaviour {

    [SerializeField]
    private GameObject[] asteroidsPrefabs = new GameObject[4];

    private List<Asteroid> activeAsteroids;

    private IEnumerator emitter;

    private float timer = .25f;

	// Use this for initialization
	void Start () {
        activeAsteroids = new List<Asteroid>();
        emitter = EmitterRoutine();
        StartCoroutine(emitter);
	}

    void OnEnable()
    {
        if (emitter != null)
        {
            StartCoroutine(emitter);
        }
    }
	
    void OnDisable()
    {
        if (emitter != null)
        {
            StopCoroutine(emitter);
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
#endif

    private IEnumerator EmitterRoutine()
    {
        while (enabled)
        {
            yield return new WaitForSeconds(timer);

            for (int i = 0; i < 3; i++)
            {
                GameObject temp = GameObject.Instantiate(asteroidsPrefabs[0]);
                Vector3 pos = new Vector3();

                //assuming position is (0x,0y,nz)
                pos.x = Random.Range(-transform.localScale.x, transform.localScale.x);
                pos.y = Random.Range(-transform.localScale.y, transform.localScale.y);
                pos.z = transform.position.z;

                temp.transform.position = pos;
                activeAsteroids.Add(temp.GetComponent<Asteroid>());
            }
        }
    }
}
