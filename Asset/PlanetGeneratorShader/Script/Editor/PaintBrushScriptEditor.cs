using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PaintBrushSettings))]
public class PaintBrushScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // DrawDefaultInspector();
        PaintBrushSettings myScript = (PaintBrushSettings)target;


        myScript.BrushSize = EditorGUILayout.Slider("Brush Size", myScript.BrushSize, 1, 5);
        
        myScript.BrushStrength = EditorGUILayout.Slider("Brush Strength", myScript.BrushStrength, 0, 1);

        myScript.paintColor = EditorGUILayout.ColorField("Paint Color", myScript.paintColor);

        myScript.UpdateVariables();
        if(Application.isPlaying) {
            if (GUILayout.Button("Save"))
            {
                myScript.SaveDrawedTexture();
            }
        }
    }


    private Texture2D MakeTex(int width, int height, Color color)
    {
        Color[] pix = new Color[width * height];
        for (int i = 0; i < pix.Length; i++)
            pix[i] = color;

        Texture2D result = new Texture2D(width, height);
        result.SetPixels(pix);
        result.Apply();

        return result;
    }
}
