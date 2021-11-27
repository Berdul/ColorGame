using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBars : MonoBehaviour
{
    public GameObject[] ammoBars;
    public Dictionary<Color, GameObject> colorAmmoBar = new Dictionary<Color, GameObject>();
    public GameObject player;
    private Color activeColor;


    void Awake()
    {
        ammoBars = GameObject.FindGameObjectsWithTag("AmmoBar");
        Color[] colors = ColorManager.colors;

        if (ammoBars.Length == ColorManager.colors.Length) {
            for(int i = 0; i < ammoBars.Length; i++) {
                ammoBars[i].transform.FindChild("FilingBar").GetComponent<Image>().color = colors[i];
                colorAmmoBar.Add(colors[i], ammoBars[i]);
            }
        } else {
            Debug.Log("AmmoBars --- Number of colors available in colorPciker and ammo bars must be equal.");
        }
    }

    void OnEnable()
    {
        ColorManager.OnColorChanged += highlightColorAmmoBar;
    }

    void Start()
    {
        activeColor = player.GetComponent<PlayerFire>().activeColor;
        highlightColorAmmoBar(activeColor);
    }

    void OnDisable()
    {
        ColorManager.OnColorChanged -= highlightColorAmmoBar;
    }

    public void setColorAmmoBarValue(Color color, int value) {
        if (colorAmmoBar.TryGetValue(color, out GameObject ammoBar)) {
            ammoBar.GetComponent<Slider>().value = value;
        }
    }

    public void setColorAmmoBarMaxValue(Color color, int maxValue) {
        if (colorAmmoBar.TryGetValue(color, out GameObject ammoBar)) {
            ammoBar.GetComponent<Slider>().maxValue = maxValue;
        }
    }

    public void highlightColorAmmoBar(Color color) {
        // Deactivate current ammobar, active the new
        if (colorAmmoBar.TryGetValue(activeColor, out GameObject activeAmmoBar)) {
            // First child should be the highlighting image
            activeAmmoBar.transform.GetChild(0).gameObject.SetActive(false);
        }
        activeColor = color;
        if (colorAmmoBar.TryGetValue(color, out GameObject ammoBarToActivate)) {
            ammoBarToActivate.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
