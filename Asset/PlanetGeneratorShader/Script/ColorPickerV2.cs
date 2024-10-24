using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class ColorPickerV2 : MonoBehaviour , IPointerClickHandler, IPointerDownHandler , IPointerUpHandler
{
    public Color output;
    [SerializeField]bool PointerIsDown = false;
    [Header("Optional")]
    [SerializeField] ColorPickerCreateImageScript colorPickerCreateImageScript;
    
    [SerializeField] ColorPickerSetScript colorPickerSetScript;
    public void OnPointerUp(PointerEventData eventData)
    {
        PointerIsDown = false;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        PointerIsDown = true;
    }
    public void OnPointerClick (PointerEventData eventData)
    {
        //PointerIsDown = true;
    }
    void Update()
    {
        if (PointerIsDown)
        {
            output = Pick(Camera.main.WorldToScreenPoint(Input.mousePosition), GetComponent<Image>());

            if (colorPickerCreateImageScript != null)
                colorPickerCreateImageScript.SetColorPickerColor(output);
            if (colorPickerSetScript != null)
                colorPickerSetScript.SetColor(output);
        }
    }

    Color Pick (Vector2 screenPoint, Image imageToPick)
    {

        Vector2 point;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(imageToPick.rectTransform, screenPoint, Camera.main, out point);
        point += imageToPick.rectTransform.sizeDelta / 2;

        Texture2D t = GetComponent<Image>().sprite.texture;

        Vector2Int m_point =
            new Vector2Int((int)((t.width * point.x) / imageToPick.rectTransform.sizeDelta.x)
            , (int)((t.height * point.y) / imageToPick.rectTransform.sizeDelta.y));


        return t.GetPixel(m_point.x, m_point.y);


    }

    Texture2D ConvertRenderTextureToTexture2D(RenderTexture renderTexture)
    {
        // Create a new Texture2D with the same dimensions as the RenderTexture
        Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, false);

        // Set the active RenderTexture to the one we want to convert
        RenderTexture.active = renderTexture;

        // Read pixels from the RenderTexture into the Texture2D
        texture2D.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        texture2D.Apply();

        // Reset the active RenderTexture
        RenderTexture.active = null;

        return texture2D;
    }

   
}
