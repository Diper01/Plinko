using DigitalRuby.Tween;
using UnityEngine;


public class TweenManager : MonoBehaviour
{
    public void TogglePanel(RectTransform settingsPanel,bool isPanelVisible)
    {
        Vector2 targetPosition = isPanelVisible ? new Vector2(-2000f, 0f) : new Vector2(0f, 0f);
        
        TweenFactory.Tween(
            settingsPanel, 
            settingsPanel.anchoredPosition,
            targetPosition,
            0.7f,
            TweenScaleFunctions.CubicEaseInOut,
            t => settingsPanel.anchoredPosition = t.CurrentValue 
        );
    }
}