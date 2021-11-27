using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorBehovior : MonoBehaviour
{
    public Texture2D cursor;
    public int newSizeDimension;

    void OnEnable()
    {
        ColorManager.OnColorChanged += setCursorColor;
        //cursor.Resize(newSizeDimension, newSizeDimension);
    }

    void Start()
    {
        //Cursor.SetCursor(cursor, new Vector2(newSizeDimension/2, newSizeDimension/2), CursorMode.ForceSoftware);
    }

    void OnDisable()
    {
        ColorManager.OnColorChanged -= setCursorColor;
    }

    public void setCursorColor(Color color) {

    }
    
}
