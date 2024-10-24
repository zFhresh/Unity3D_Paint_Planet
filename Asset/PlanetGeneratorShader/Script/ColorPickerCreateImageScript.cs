using UnityEngine;
using UnityEngine.UI;

public class ColorPickerCreateImageScript : MonoBehaviour
{
    [SerializeField] ComputeShader ColorPickerComputeShader;
    [SerializeField]int KarnelHandle = 0;
    int TextureSize = 1024;
    [SerializeField]Image rawImage;
    [SerializeField] RenderTexture ColorPickerImageTex;

    [SerializeField] private Color ColorPickerColor;
    void Start()
    {
        rawImage = GetComponent<Image>();
        KarnelHandle = ColorPickerComputeShader.FindKernel("CSMain");
        ColorPickerImageTex = new RenderTexture(256, 256, 0, RenderTextureFormat.ARGBFloat);
        ColorPickerImageTex.enableRandomWrite = true;
        ColorPickerImageTex.Create();

        ColorPickerComputeShader.SetTexture(KarnelHandle, "ColorPickerImageTex", ColorPickerImageTex);
        
        ColorPickerComputeShader.SetFloat("TextureSize", TextureSize);

        if(ColorPickerColor != null) {
            PlayComputeShader();
        }
    }
    void OnValidate()
    {
        if(ColorPickerColor != null && Application.isPlaying) {
            PlayComputeShader();
        }
    }

    // Update is called once per frame
    public void SetColorPickerColor(Color _color) {
        ColorPickerColor = _color;
        PlayComputeShader();
    }

    public void PlayComputeShader() {
        KarnelHandle = ColorPickerComputeShader.FindKernel("CSMain");
        ColorPickerComputeShader.SetVector("PickedColor", ColorPickerColor);
        ColorPickerComputeShader.Dispatch(KarnelHandle, TextureSize / 8, TextureSize / 8, 1);
        rawImage.sprite = RenderTextureToSprite(ColorPickerImageTex);
    }

    public Sprite RenderTextureToSprite(RenderTexture tex) {
        Texture2D t = new Texture2D(tex.width, tex.height, TextureFormat.RGBA32, false);
        RenderTexture.active = tex;
        t.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        t.Apply();
        RenderTexture.active = null;
        return Sprite.Create(t, new Rect(0, 0, t.width, t.height), new Vector2(0.5f, 0.5f));
    }
}
