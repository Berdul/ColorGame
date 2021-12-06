using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneManager : MonoBehaviour
{
    public static int maxScore = 2;

    private static Dictionary<Color, int> colorPointsPlayerHold = new Dictionary<Color, int>();
    private static Dictionary<Color, int> colorScore = new Dictionary<Color, int>();


    void Awake()
    {
        foreach (var color in ColorManager.colors)
        {
            colorScore.Add(color, 0);
            colorPointsPlayerHold.Add(color, 0);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void holdPoints(Color color, int holdPoints) {
        if (colorPointsPlayerHold.TryGetValue(color, out int heldPoints)) {
            colorPointsPlayerHold[color] += holdPoints;
            Debug.Log("Heldpoints : " + heldPoints);
        }
    }

    public static void scorePoints(Color color) {
        Debug.Log("colorScore[color] : " + colorScore[color] + " / colorPointsPlayerHold[color] : " + colorPointsPlayerHold[color]);
        if (colorScore.TryGetValue(color, out int score) && colorPointsPlayerHold.TryGetValue(color, out int heldPoints) && score < maxScore && heldPoints > 0) {
            colorScore[color] +=  1;
            colorPointsPlayerHold[color] -= 1;
            Debug.Log("score : " + score + " / heldpoints : " + heldPoints);

            if (colorScore[color] >= maxScore) {
                Debug.Log("Zone CAPTURED");
                // emit event zone captured
            }
        }
    }
}
