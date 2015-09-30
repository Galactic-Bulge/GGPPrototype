using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour {

    public delegate void DestroyedDelegate(bool byPlayer);
    public event DestroyedDelegate onDestroyed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.z < -15)
        {
            if(onDestroyed != null)
            {
                onDestroyed(false);
                GameObject.Destroy(gameObject);
            }
        }

        transform.position -= new Vector3(0, 0, 5);
    }
}
