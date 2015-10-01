using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour {

    private float speed = 1;
    private float timeToLive = 1;
    private float timeAlive;

    public float Speed
    {
        get { return speed; }
    }

	// Update is called once per frame
	void Update () {
        timeAlive += Time.deltaTime;

        transform.position += transform.up * speed;

        if(timeAlive > timeToLive)
        {
            GameObject.Destroy(gameObject);
        }
	}
}
