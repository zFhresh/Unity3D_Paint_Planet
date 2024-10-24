using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SizeMenuScript : MonoBehaviour
{
    [SerializeField] Slider brushSizeSlider;
    [SerializeField] TMP_InputField brushSizeText;

    [SerializeField] Slider BrushStrengthSlider;
    [SerializeField] TMP_InputField BrushStrengthText;
    [SerializeField] PaintWithMouseCompute paintWithMouseCompute;
    void Start()
    {
        
    }

    public void SetBrushSize(float size)
    {
        brushSizeText.text = size.ToString();

        paintWithMouseCompute.ChangeBrushSize(size);


    }
    public void SetBrushSize_String(string Size)
    {
        float size = float.Parse(Size);

        if (size < 1)
        {
            size = 1;
        }

        brushSizeText.text = size.ToString();
        brushSizeSlider.value = size;

        paintWithMouseCompute.ChangeBrushSize(size);

        
    }

    public void SetBrushStrength(float strength)
    {
        BrushStrengthText.text = strength.ToString();
        paintWithMouseCompute.ChangeBrushStrength(strength);
    }

    public void SetBrushStrength_String(string strength)
    {

        float str = float.Parse(strength);
        if (str > 1)
        {
            str = 1;
        }
        else if (str < 0)
        {
            str = 0;
        }
        BrushStrengthText.text = str.ToString();
        BrushStrengthSlider.value = str;
        paintWithMouseCompute.ChangeBrushStrength(str);
    }
}
