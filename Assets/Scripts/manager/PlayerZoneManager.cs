using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerZoneManager : MonoBehaviour
{
    public delegate void ZoneCaptureAction(Color color);
    public static event ZoneCaptureAction OnZoneCaptured;
    public int maxScore = 2;

    private Dictionary<Color, int> colorPointsPlayerHold = new Dictionary<Color, int>();
    private Dictionary<Color, int> colorScore = new Dictionary<Color, int>();
    private Dictionary<Color, bool> zoneCaptured = new Dictionary<Color, bool>();

    void Awake()
    {
        foreach (var color in ColorManager.colors)
        {
            colorScore.Add(color, 0);
            colorPointsPlayerHold.Add(color, 0);
            zoneCaptured.Add(color, false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void holdPoints(Color color, int holdPoints) {
        if (colorPointsPlayerHold.TryGetValue(color, out int heldPoints)) {
            colorPointsPlayerHold[color] += holdPoints;
            Debug.Log("Heldpoints : " + heldPoints);
        }
    }

    public void scorePoints(Color color) {
        //Debug.Log("colorScore[color] : " + colorScore[color] + " / colorPointsPlayerHold[color] : " + colorPointsPlayerHold[color]);

        // If conditions are met, take points held by the player and score them on the corresponding zone. If maxscore is reached, capture the zone
        if (colorScore.TryGetValue(color, out int score) && colorPointsPlayerHold.TryGetValue(color, out int heldPoints) &&
            zoneCaptured.TryGetValue(color, out bool captured) && !captured && heldPoints > 0 ) {
            colorScore[color] +=  1;
            colorPointsPlayerHold[color] -= 1;
            //Debug.Log("score : " + score + " / heldpoints : " + heldPoints + " / color : " + color);

            if (colorScore[color] >= maxScore) {
                zoneCaptured[color] = true;
            }
        }
    }

    private void emitZoneCaptured(Color color) {
        if (OnZoneCaptured != null) {
            OnZoneCaptured(color);
        }
    }
}
