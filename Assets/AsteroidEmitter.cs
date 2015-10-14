using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsteroidEmitter : MonoBehaviour {

    /// <summary>
    /// List of rock prefabs to use. Currently only one
    /// </summary>
    [SerializeField]
    private GameObject[] asteroidsPrefabs = new GameObject[4];

    /// <summary>
    /// List of currently active/flying asteroids
    /// </summary>
    private List<Asteroid> activeAsteroids;

    /// <summary>
    /// Routine for periodically firing off asteroids
    /// </summary>
    private IEnumerator emitter;

    /// <summary>
    /// The time bewteen asteroid emission
    /// </summary>
    private float timer;

	/// <summary>
    /// Instantiate the emitter
    /// </summary>
	void Start () {
        activeAsteroids = new List<Asteroid>();
        emitter = EmitterRoutine();
        timer = 1 / GameManager.Instance.level;
        StartCoroutine(emitter);
	}

    /// <summary>
    /// Start up the emission routine when the emitter is activated
    /// </summary>
    void OnEnable()
    {
        if (emitter != null)
        {
            // set the time between emissions to be inversely proportional to the current level
            timer = 1.0f / GameManager.Instance.level;
            StartCoroutine(emitter);
        }
    }
	
    /// <summary>
    /// When the emitter is disabled stop the emission routine
    /// </summary>
    void OnDisable()
    {
        if (emitter != null)
        {
            StopCoroutine(emitter);
        }
    }

#if UNITY_EDITOR

    // just highlight the emitter in the editor
    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
#endif

    /// <summary>
    /// This routine creates a field of asteroids based upon the bounds of the emitter object.
    /// </summary>
    /// <returns></returns>
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
                temp.GetComponent<Asteroid>().onDestroyed += GameManager.Instance.OnDestroyedAsteroid;

                activeAsteroids.Add(temp.GetComponent<Asteroid>());
            }
        }
    }
}
