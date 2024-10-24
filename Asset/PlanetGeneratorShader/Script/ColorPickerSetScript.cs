using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ColorPickerSetScript : MonoBehaviour
{
    [SerializeField] TMP_InputField ColorHexCodeInputField;
    [SerializeField] Image ColorImage;
    [SerializeField] PaintWithMouseCompute paintWithMouseComputer;
    void OnEnable()
    {
        ColorHexCodeInputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string arg0)
    {
        try
        {
            Color color = new Color();
            if(arg0[0] != '#')
            {
                arg0 = "#" + arg0;
            }

            ColorUtility.TryParseHtmlString(arg0, out color);
            SetColor(color);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void SetColor(Color color)
    {
        ColorImage.color = color;
        paintWithMouseComputer.ChangeColor(color);
        ColorHexCodeInputField.text = ColorUtility.ToHtmlStringRGB(color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
