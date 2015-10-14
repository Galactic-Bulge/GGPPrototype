using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Delegate for handling when a level ends
    /// </summary>
    public delegate void RoundOverDelegate();

    /// <summary>
    /// Event for handling when a level ends
    /// </summary>
    public event RoundOverDelegate onRoundOver;

    #region singleton
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }
    #endregion

    /// <summary>
    /// The asteroid emitter
    /// </summary>
    [SerializeField]
    private AsteroidEmitter emitter;

    /// <summary>
    /// How long a level lasts
    /// </summary>
    [SerializeField]
    private float levelTime = 120;

    /// <summary>
    /// How long the current level has run
    /// </summary>
    private float levelElapsed;

    /// <summary>
    /// How long warp animation lasts
    /// </summary>
    [SerializeField]
    private float warpTime = 15;

    /// <summary>
    /// How long the warp animation has run
    /// </summary>
    private float warpElapsed;

    /// <summary>
    /// Is the player in a warp state?
    /// </summary>
    bool warping;

    /// <summary>
    /// Current level
    /// </summary>
    [HideInInspector]
    public int level = 1;

    /// <summary>
    /// Acquired points
    /// </summary>
    private int points;

    /// <summary>
    /// Instantiate and set up the singleton
    /// </summary>
    void Start()
    {
        level = 1;
        instance = this;
    }

    /// <summary>
    /// Game logic
    /// </summary>
    void Update()
    {
        // update appropriate timers
        if (warping)
        {
            warpElapsed += Time.deltaTime;
        }
        else
        {
            levelElapsed += Time.deltaTime;
        }

        // stop timers
        if (levelElapsed > levelTime)
        {
            emitter.enabled = false;
            level++;
            warping = true;
            levelElapsed = 0;
            onRoundOver();
        }
        else if (warpElapsed > warpTime)
        {
            emitter.enabled = true;
            warping = false;
        }
    }

    /// <summary>
    /// Event callback for when the user destroys an asteroid
    /// </summary>
    /// <param name="byShip">Did the player destroy the asteroid?</param>
    public void OnDestroyedAsteroid(bool byShip)
    {
        points += byShip ? 10 : 0;

        Debug.Log(points);
    }

}
