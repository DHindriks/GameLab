using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Image Icon;
    [SerializeField] Image ProgressBar;
    [SerializeField] List<Image> ColorSprites;

    void Start()
    {
        if (Icon.sprite == null)
        {
            Icon.gameObject.SetActive(false);
        }
    }

    public void SetIcon(Sprite NewIcon = null)
    {
        if (NewIcon != null)
        {
            Icon.gameObject.SetActive(true);
            Icon.sprite = NewIcon;
        }else
        {
            Icon.sprite = NewIcon;
            Icon.gameObject.SetActive(false);
        }
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
