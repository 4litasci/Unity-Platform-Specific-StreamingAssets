using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AssetController : MonoBehaviour
{
    public Image imageUI;
    public Text textUI;

    private void Start()
    {
        string platform = "";
        
#if UNITY_ANDROID
        platform = "android";
#elif UNITY_IOS
        platform = "ios"
#endif
        
        // Very useful asset => https://github.com/gwiazdorrr/BetterStreamingAssets
        // If you want to test in editor. Don't forget to copy your files.

        BetterStreamingAssets.Initialize();
        
        var bruhImage = BetterStreamingAssets.ReadAllBytes(Path.Combine(platform , "bruh.jpg"));
        var bruhText = BetterStreamingAssets.ReadAllText(Path.Combine(platform , "bruh.txt"));
        
        var tex2D = new Texture2D(2, 2);
        tex2D.LoadImage(bruhImage);
        
        var  bruhSprite = Sprite.Create(tex2D, new Rect(0, 0, tex2D.width, tex2D.height), new Vector2(0, 0), 100.0f, 0 , SpriteMeshType.Tight);
        
        imageUI.sprite = bruhSprite;
        textUI.text = bruhText;

    }
}
