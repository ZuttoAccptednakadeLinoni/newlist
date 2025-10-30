using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using XScrollView;
using Object = UnityEngine.Object;

public class BagScrollViewItem : BaseXScrollViewItem
{
    [SerializeField]
    private TextMeshProUGUI text;
    
    [SerializeField]
    private Image icon;

    private AssetBundle ab;
    
    
    public TextMeshProUGUI Text
    {
        get
        {
            if (text == null)
                text = GetComponentInChildren<TextMeshProUGUI>();
            return text;
        }
    }
    public Image Icon
    {
        get
        {
            if (icon == null)
                icon = GetComponentInChildren<Image>();
            return icon;
        }
    }
    
    public override void UpdateCellInfo(int _index, IXScrollViewItemData data)
    {
        base.UpdateCellInfo(_index, data);
        var itemData = data as BagScrollViewItemData;
        //Debug.Log(data==null);
        Text.text = itemData.num.ToString();
        //Debug.Log(itemData.icon);
        var texture= abmenger.Getinstance().LoadRes("pic", itemData.icon)as Texture2D;
        Sprite convertedSprite = Sprite.Create(
            texture,                             // 源Texture2D
            new Rect(0, 0, texture.width, texture.height), // 定义纹理中用于Sprite的矩形区域（这里使用整个纹理）
            new Vector2(0.5f, 0.5f),                   // 轴心点（0.5, 0.5代表中心）
            100.0f,                                    // 每Unity单位的像素值，影响Sprite在场景中的大小
            0,                                         // 挤压网格（Extruded Mesh），通常设为0
            SpriteMeshType.Tight,                      // 网格类型
            Vector4.zero                               // 边界（Border），用于9宫格拉伸等，默认设为0
        );
        Icon.sprite = convertedSprite ;
        Debug.Log(Icon.sprite==null);
    }

    // IEnumerator LoadABRes(string abname, string icon)
    // {
    //     AssetBundle abcr =AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + abname);
    //     Icon.sprite = abcr.LoadAssetAsync(icon).asset as Sprite;
    //     abcr.Unload(false);
    //     yield return null;
    //     
    // }
}
