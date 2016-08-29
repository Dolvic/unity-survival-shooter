using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;

    private Text text;

    public void Awake()
    {
        text = GetComponent<Text>();
        score = 0;
    }

    public void Update()
    {
        text.text = "Score: " + score;
    }
}