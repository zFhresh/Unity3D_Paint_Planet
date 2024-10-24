using UnityEngine;

public class PaintWithMouseCompute : MonoBehaviour
{
    [SerializeField] ComputeShader PaintComputeShader;
    [SerializeField] int TextureSize = 1024;
    [SerializeField]RenderTexture GroundMap;

    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] private Color paintColor;
    [SerializeField] private float brushSize;
    [SerializeField][Range(0,1)] private float brushStrength;
    [SerializeField] private bool isEraseMode;
    [SerializeField] private Camera cam;
    RaycastHit hit;

    int kernelHandle = 0;
    void OnEnable()
    {
        cam = Camera.main;
        GroundMap = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.ARGBFloat);
        GroundMap.enableRandomWrite = true;
        GroundMap.Create();
        kernelHandle = PaintComputeShader.FindKernel("CSMain");
        PaintComputeShader.SetTexture(kernelHandle, "PaintTexture", GroundMap);
        //PaintComputeShader.Dispatch(kernelHandle, TextureSize / 8, TextureSize / 8, 1);


        // Set material
        meshRenderer.sharedMaterial.SetTexture("_Texture2D", GroundMap);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0)) {
            if(Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit)) {
                Debug.Log(hit.textureCoord);
                PaintComputeShader.SetVector("_Coordinates", new Vector4(hit.textureCoord.x, hit.textureCoord.y, 0, 0) * TextureSize);
                PaintComputeShader.SetFloat("_Strength",brushStrength);
                PaintComputeShader.SetFloat("_Size",brushSize);
                PaintComputeShader.SetVector("_Color", paintColor);
                PaintComputeShader.SetFloat("_Eraser", isEraseMode ? 0 : 1);

                PaintComputeShader.Dispatch(kernelHandle, TextureSize / 8, TextureSize / 8, 1);
            }
        }
    }


    public void ChangeBrushSize(float size)
    {
        brushSize = size;
    }

    public void ChangeBrushStrength(float strength)
    {
        brushStrength = strength;
    }

    public void ChangeColor(Color color)
    {
        paintColor = color;
    }

    public bool ChangeEraseMode()
    {
        isEraseMode = !isEraseMode;
        return isEraseMode;
    }
}
