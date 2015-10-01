using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{

    public delegate void DestroyedDelegate(bool byPlayer);
    public event DestroyedDelegate onDestroyed;

    private float speed;

    public float Speed
    {
        get { return speed; }
    }

    // Use this for initialization
    void Start()
    {
        float scale = Random.Range(.1f, .3f);
        //speed = Random.Range(3, 10);
        speed = 1;
        transform.localScale = new Vector3(scale, scale, scale);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Beam")
        {
            if (onDestroyed != null)
            {
                onDestroyed(false);
            }
            GameObject.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < -15)
        {
            if (onDestroyed != null)
            {
                onDestroyed(false);
            }
            GameObject.Destroy(gameObject);

        }

        transform.position -= new Vector3(0, 0, speed);
    }
}
