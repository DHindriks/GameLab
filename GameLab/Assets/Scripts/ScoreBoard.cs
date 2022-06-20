using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Image ProgressBar;
    [SerializeField] List<Image> ColorSprites;

    public void SetIcon(Sprite NewIcon)
    {
        Icon.sprite = NewIcon;
    }

    public void SetFillAmount(float Fillamount)
    {
        ProgressBar.fillAmount = Fillamount;
    }

    public void SetColor(Color NewColor)
    {
        foreach (Image sprite in ColorSprites)
        {
            sprite.color = NewColor;
        }
    }
}
