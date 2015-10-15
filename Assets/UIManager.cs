using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private Text scoreUI;

    [SerializeField]
    private Text livesUI;

    [SerializeField]
    private Image healthUI;

    void Start()
    {
        instance = this;
    }

    public void SetScore(int score)
    {
        scoreUI.text = score.ToString();
    }

    public void SetLives(int lives)
    {
        livesUI.text = lives.ToString();
    }

    public void SetHealth(int health)
    {
        float perc = (float)health / 100;
        float width = perc * 300;

        healthUI.rectTransform.sizeDelta = new Vector2(width, 25);
    }
}
