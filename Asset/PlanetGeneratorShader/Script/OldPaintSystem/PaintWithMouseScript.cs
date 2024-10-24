using System;
using UnityEditor;
using UnityEngine;

public class PaintWithMouseScript : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Shader drawShader;

    [SerializeField]private RenderTexture GroundMap;
    public Material CurrentMatrial, drawMaterial;
    private RaycastHit hit;

    [SerializeField] [Range(1, 500)] private float brushSize;
    [SerializeField] [Range(0, 1)] private float brushStrength;
    [SerializeField] bool isEraseMode;

    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] Color paintColor;

    void Start()
    {
        drawMaterial = new Material(drawShader);
        drawMaterial.SetVector("_Color", paintColor);

        CurrentMatrial = meshRenderer.material;

        GroundMap = new RenderTexture(1024, 1024, 0, RenderTextureFormat.ARGBFloat);
        CurrentMatrial.SetTexture("_Texture2D", GroundMap);
    }
    void OnValidate()
    {
        if (CurrentMatrial != null)
        {
            CurrentMatrial.SetVector("_Color", paintColor);
        }
        if(drawMaterial != null)
        {
            drawMaterial.SetVector("_Color", paintColor);
            // set bool
            drawMaterial.SetFloat("_Eraser", isEraseMode ? -1 : 1);
        }
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) {
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) {
                Debug.Log(hit.textureCoord);
                drawMaterial.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0));
                drawMaterial.SetFloat("_Strength",brushStrength);
                drawMaterial.SetFloat("_Size",brushSize);

                RenderTexture temp = RenderTexture.GetTemporary(GroundMap.width, GroundMap.height, 0, RenderTextureFormat.ARGBFloat);
                Graphics.Blit(GroundMap, temp);
                Graphics.Blit(temp, GroundMap, drawMaterial);

                RenderTexture.ReleaseTemporary(temp);
            }
        }
    }

    [ContextMenu("Save")]
    public void SaveDrawedTexture() {
        Texture2D tex = new Texture2D(GroundMap.width, GroundMap.height, TextureFormat.RGBAFloat, false);
        RenderTexture.active = GroundMap;
        tex.ReadPixels(new Rect(0, 0, GroundMap.width, GroundMap.height), 0, 0);
        tex.Apply();
        RenderTexture.active = null;

        byte[] bytes = tex.EncodeToPNG();
        System.IO.File.WriteAllBytes(Application.dataPath + "/SavedScreen.png", bytes);

        //AssetDatabase.Refresh();
    }
    public void ChangeBrushSize(float size) {
        brushSize = size;
    }
    public void ChangeBrushStrength(float strength) {
        brushStrength = strength;
    }

    public void UpdateBrushSize(float size) {
        brushSize = size;
    }

    public void ChangePaintColor(Color paintColor)
    {
        Debug.Log("ChangePaintColor");
        drawMaterial.SetVector("_GroundInsideColor", paintColor);
    }
}
