using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

using XScrollView;

public class BagUI : MonoBehaviour
{
    private List<BagScrollViewItemData> cellsDataList;
    public int totalNum = 100;
    private UIXScrollView uixScrollView;
    
    
    //被选中物体的信息
    public int curSelectIndex = 0;
    public Image selectIcon;
    public TextMeshProUGUI selectItemName;
    public TextMeshProUGUI selectNum;
    void Start()
    {
        cellsDataList = new List<BagScrollViewItemData>();
        for (int i = 0; i < totalNum;i++)
        {
            var cellData = CreatCellData(i);
            cellsDataList.Add(cellData);
        }
        //初始化列表
        uixScrollView = GetComponentInChildren<UIXScrollView>();
        uixScrollView.InitXScrollView(totalNum);
        uixScrollView.AddUpdateCellAction(OnUpdateScrollItemAction);
        uixScrollView.AddCellClickAction(OnClickScrollItemAction);
    }

    BagScrollViewItemData CreatCellData(int index)
    {
        var data = new BagScrollViewItemData()
        {
            icon = $"1 ({index % 40 + 1})",
            num = Random.Range(1, 999),
        };
        return data;
    }
    private void OnUpdateScrollItemAction(BaseXScrollViewItem item, int index)
    {
        
        var data = GetItemData(index);
        item.UpdateCellInfo(index, data);
    }
    BagScrollViewItemData GetItemData(int index)
    {
        return cellsDataList[index];
    }
    private void OnClickScrollItemAction(int index)
    {
        if (index == curSelectIndex)
        {
            return;
        }
        curSelectIndex = index;
        UpdateSelectItemInfo();
        uixScrollView.UpdateScrollView(true);
    }
    private void UpdateSelectItemInfo()
   {
       var data = GetItemData(curSelectIndex);
       selectNum.text = data.num.ToString();
       selectItemName.text = $"wuqi_{curSelectIndex}";
       var texture= abmenger.Getinstance().LoadRes("pic", data.icon)as Texture2D;
       Sprite convertedSprite = Sprite.Create(
           texture,                             // 源Texture2D
           new Rect(0, 0, texture.width, texture.height), // 定义纹理中用于Sprite的矩形区域（这里使用整个纹理）
           new Vector2(0.5f, 0.5f),                   // 轴心点（0.5, 0.5代表中心）
           100.0f,                                    // 每Unity单位的像素值，影响Sprite在场景中的大小
           0,                                         // 挤压网格（Extruded Mesh），通常设为0
           SpriteMeshType.Tight,                      // 网格类型
           Vector4.zero                               // 边界（Border），用于9宫格拉伸等，默认设为0
       );
       selectIcon.sprite = convertedSprite;
    }
}
public class BagScrollViewItemData : IXScrollViewItemData
{
    /// <summary>
    /// 道具id
    /// </summary>
    public int id;
    /// <summary>
    /// 道具icon
    /// </summary>
    public string icon;
    /// <summary>
    /// 道具数量
    /// </summary>
    public int num;
}
