using UnityEngine;
using System.Collections;

public class Asteroid : MonoBehaviour
{

    /// <summary>
    /// Delegate for handling when an asteroid is destroyed
    /// </summary>
    /// <param name="byPlayer">Destroyed(true) or left the field(false)</param>
    public delegate void DestroyedDelegate(bool byPlayer);

    /// <summary>
    /// Event for handling when an asteroid is destroyed
    /// </summary>
    public event DestroyedDelegate onDestroyed;

    /// <summary>
    /// How fast the asteroid travels. No acceleration
    /// </summary>
    private float speed;

    /// <summary>
    /// Accessor for speed. Used in trajectory prediction
    /// </summary>
    public float Speed
    {
        get { return speed; }
    }

    /// <summary>
    /// Instantiate asteroid
    /// </summary>
    void Start()
    {
        // give it a random size
        float scale = Random.Range(.1f, .3f);
        speed = 1;
        transform.localScale = new Vector3(scale, scale, scale);

        // when the round ends start a 'warp'
        GameManager.Instance.onRoundOver += delegate () { speed *= 10; };
    }

    /// <summary>
    /// Collision event
    /// </summary>
    /// <param name="c"></param>
    void OnTriggerEnter(Collider c)
    {
        if (c.tag == "Beam")
        {
            if (onDestroyed != null)
            {
                onDestroyed(true);
            }
            GameObject.Destroy(gameObject);
        }
    }

    /// <summary>
    /// Update asteroid position
    /// </summary>
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
