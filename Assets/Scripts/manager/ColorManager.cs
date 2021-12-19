using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public delegate void ColorChangeAction(Color color);
    public static event ColorChangeAction OnColorChanged;

    private int activeColorIndex;
    private KeyCode[] keyCodes = new KeyCode[]{};

    // public static Color[] colors = new Color[] {
    //     new Color(0, 48f/255f, 73/255f, 1), 
    //     new Color(214/255f, 40/255f, 40/255f, 1), 
    //     new Color(247/255f, 127/255f, 0, 1),
    //     new Color(252/255f, 191/255f, 73/255f, 1), 
    //     new Color(234/255f, 226/255f, 183/255f, 1)};

    public static Color[] colors = new Color[] {
        new Color(0, 48f/255f, 73/255f, 1), 
        new Color(214/255f, 40/255f, 40/255f, 1), 
        new Color(247/255f, 127/255f, 0, 1), 
        new Color(234/255f, 226/255f, 183/255f, 1)};

    void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);    
    }

    void Start()
    {
        activeColorIndex = 0;
        emitColorChanged(activeColorIndex);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) {
            activeColorIndex = 0;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetKey(KeyCode.Alpha2)) {
            activeColorIndex = 1;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetKey(KeyCode.Alpha3)) {
            activeColorIndex = 2;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetKey(KeyCode.Alpha4)) {
            activeColorIndex = 3;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetKey(KeyCode.Alpha5)) {
            activeColorIndex = 4;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            activeColorIndex = (activeColorIndex + 1) % colors.Length;
            emitColorChanged(activeColorIndex);
        } else if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            activeColorIndex = Mathf.Abs(colors.Length + activeColorIndex - 1) % colors.Length;
            emitColorChanged(activeColorIndex);
        } 

    }

    private void emitColorChanged(int colorIndex) {
        if (OnColorChanged != null) {
            OnColorChanged(colors[colorIndex]);
        }
    }

    public static Color pickColor(int index) {
        return colors[index];
    }
}
