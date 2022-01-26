using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarCircular : MonoBehaviour
{
    public Image image;
    // Max portion of the circle that can be filled (1 / AmmoBar amount)
    public float maxFill;

    public void setMaxFill(float maxFill) {
        this.maxFill = maxFill;
    }

    public void setFill(float ammoPercentage) {
        image.fillAmount = ammoPercentage * maxFill;
    }
}
