using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBarsCircular : MonoBehaviour
{
    private GameObject[] ammoBarsCircular;
    public Dictionary<Color, GameObject> colorAmmoBarCircular = new Dictionary<Color, GameObject>();
    public GameObject player;
    private Color activeColor;
    private int maxAmmo;

    public Vector3 smallScale;
    public Vector3 bigScale;

    void Awake()
    {
        ammoBarsCircular = GameObject.FindGameObjectsWithTag("AmmoBarCircular");
        Color[] colors = ColorManager.colors;

        if (ammoBarsCircular.Length == ColorManager.colors.Length) {
            for(int i = 0; i < ammoBarsCircular.Length; i++) {
                ammoBarsCircular[i].transform.FindChild("FillingBarCircular").GetComponent<Image>().color = colors[i];
                //ammoBarsCircular[i].transform.FindChild("FillingBarCircular").GetComponent<Image>().rectTransform.sizeDelta = 
                ammoBarsCircular[i].transform.localScale = smallScale;
                colorAmmoBarCircular.Add(colors[i], ammoBarsCircular[i]);
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
        maxAmmo = player.GetComponent<PlayerFire>().maxAmmo;
    }

    void OnDisable()
    {
        ColorManager.OnColorChanged -= highlightColorAmmoBar;
    }

    public void setColorAmmoBarValue(Color color, int value) {
        if (colorAmmoBarCircular.TryGetValue(color, out GameObject ammoBar)) {
            ammoBar.GetComponent<AmmoBarCircular>().setFill( (float)value / (float)maxAmmo);
        }
    }

    public void setColorAmmoBarMaxValue(Color color, float maxValue) {
        if (colorAmmoBarCircular.TryGetValue(color, out GameObject ammoBar)) {
            ammoBar.GetComponent<AmmoBarCircular>().setMaxFill(maxValue);
        }
    }

    public void highlightColorAmmoBar(Color color) {
        // Deactivate current ammobar, active the new
        if (colorAmmoBarCircular.TryGetValue(activeColor, out GameObject activeAmmoBar)) {
            // First child should be the highlighting image
            activeAmmoBar.transform.localScale = smallScale;
        }
        if (colorAmmoBarCircular.TryGetValue(color, out GameObject ammoBarToActivate)) {
            ammoBarToActivate.transform.localScale = bigScale;
            activeColor = color;
        }
    }
}
