using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class ColorPickerScript : MonoBehaviour
{
    [SerializeField] Color _Color;
    [SerializeField]ColorPickerSetScript colorPickerSetScript;
    
    public void OnRedSliderChanged(float value)
    {
        _Color.r = value;
        colorPickerSetScript.SetColor(_Color);
    }
    public void OnGreenSliderChanged(float value)
    {
        _Color.g = value;
        colorPickerSetScript.SetColor(_Color);
    }
    public void OnBlueSliderChanged(float value)
    {
        _Color.b = value;
        colorPickerSetScript.SetColor(_Color);
    }

}
