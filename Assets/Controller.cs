using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour {

    /// <summary>
    /// The Game object for the origin of the left laser
    /// </summary>
    [SerializeField]
    private GameObject leftLaser;

    /// <summary>
    /// The Game object for the origin of the right laser
    /// </summary>
    [SerializeField]
    private GameObject rightLaser;

    /// <summary>
    /// Prefab for laser beams
    /// </summary>
    [SerializeField]
    private GameObject beam;

    /// <summary>
    /// Total health
    /// </summary>
    private int health = 100;

    /// <summary>
    /// Total lives
    /// </summary>
    private int lives = 3;

    /// <summary>
    /// Set in inspector. The total time the ship can be invincible after a death
    /// </summary>
    [SerializeField]
    private float invincibleTime = 5;

    /// <summary>
    /// Timer for how long the ship has been invincible
    /// </summary>
    private float invincibleElapsed;

    /// <summary>
    /// Is the ship currently invincible
    /// </summary>
    private bool invincible;

    void Start()
    {
        UIManager.Instance.SetLives(lives);
    }

    /// <summary>
    /// Collision Event
    /// </summary>
    /// <param name="other"></param>
    void  OnTriggerEnter(Collider other)
    {
        // if we hit an asteroid take damage.
        if(other.tag == "Asteroid" && !invincible)
        {
            health -= 10;
            UIManager.Instance.SetHealth(health);
        }
    }

    /// <summary>
    /// Probably shouldn't be a coroutine. Oh Well, but this is called to flash the ship red 
    /// just to indicate that we have been hit. Prototype purposes only
    /// </summary>
    /// <returns></returns>
    IEnumerator FlashRed()
    {
        Material shipMat = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        Color standard = Color.white;
        Color blink = Color.red;

        shipMat.color = blink;
        yield return new WaitForSeconds(.2f);
        shipMat.color = standard;
    }

    /// <summary>
    /// Flash the ship blue for a set period of time as an indication that we are invincible
    /// </summary>
    /// <returns></returns>
    IEnumerator InvinicbleRoutine()
    {
        Material shipMat = GetComponentInChildren<MeshRenderer>().sharedMaterial;
        Color standard = Color.white;
        Color blink = Color.blue;

        while (invincibleElapsed < invincibleTime) {
            invincibleElapsed += Time.deltaTime + .4f;

            shipMat.color = blink;
            yield return new WaitForSeconds(.2f);

            shipMat.color = standard;
            yield return new WaitForSeconds(.2f);

            invincibleElapsed += Time.deltaTime + .4f;
            Debug.Log(invincibleElapsed);
        }

        invincible = false;
    }

	// Update is called once per frame
	void Update () {

        // check and handle current health
        if(health <= 0)
        {
            lives--;

            UIManager.Instance.SetLives(lives);

            // check lives
            if(lives <= 0) // end game
            {
                Debug.Log("you lose");
            }
            else // reset player
            {
                invincible = true;
                invincibleElapsed = 0;
                health = 100;
                StartCoroutine(InvinicbleRoutine());
            }
        }

        #region input & movement

        // the ship is set below the origin, so just rotate around the forward axis at (0,0,0)

        // rotate left
        if (Input.GetKey(KeyCode.A))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, -1);
        }

        // rotate right
        if (Input.GetKey(KeyCode.D))
        {
            transform.RotateAround(Vector3.zero, Vector3.forward, 1);
        }

        // fire on mouse down
        if (Input.GetMouseButtonDown(0))
        {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            Vector3 point = mouseRay.origin + (mouseRay.direction * 100);

            GameObject left = GameObject.Instantiate(beam);
            left.transform.position = leftLaser.transform.position;

            GameObject right = GameObject.Instantiate(beam);
            right.transform.position = rightLaser.transform.position;

            if(Physics.Raycast(mouseRay,out hit, 500))
            {
                if (hit.collider.tag == "Asteroid")
                {
                    Asteroid a = hit.collider.GetComponent<Asteroid>();

                    // super rough trajectory prediction.
                    Vector3 between = (leftLaser.transform.position - rightLaser.transform.position)/2 + rightLaser.transform.position;
                    float distance = (between - hit.point).magnitude;
                    float beamTime = (distance/left.GetComponent<LaserBeam>().Speed)/2;
                    Vector3 future = hit.point - (Vector3.forward * a.Speed * beamTime);

                    point = future;
                }
            }

            // beams are cylinders currently. if we use something else we will need to adjust which vector is "forward"
            left.transform.up = point - leftLaser.transform.position;
            right.transform.up = point - rightLaser.transform.position;          
        }
        #endregion
    }
}
