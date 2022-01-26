using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int score;
    public Text text;

    void Start()
    {
        score = 0;
    }

    void Update()
    {       
        GetComponent<Text>().text = score.ToString();
    }

    public int addToScore(int points) {
        score += points;
        return score;
    }
}
