using UnityEngine;

public class PaintBrushSettings : MonoBehaviour
{
    public float BrushSize;
    public float BrushStrength;
    public Color paintColor;
    private PaintWithMouseScript paintWithMouseScript;
    void OnEnable()
    {
        paintWithMouseScript= GetComponent<PaintWithMouseScript>();
    }
    void OnValidate()
    {
        paintWithMouseScript.ChangeBrushSize(BrushSize);
        paintWithMouseScript.ChangeBrushStrength(BrushStrength);
        //paintWithMouseScript.ChangePaintColor(paintColor);
    }
    public void UpdateVariables()
    {
        // paintWithMouseScript.ChangeBrushSize(BrushSize);
        // paintWithMouseScript.ChangeBrushStrength(BrushStrength);
        // //paintWithMouseScript.ChangePaintColor(paintColor);
    }
    public void SaveDrawedTexture() {
        paintWithMouseScript.SaveDrawedTexture();
    }
}
