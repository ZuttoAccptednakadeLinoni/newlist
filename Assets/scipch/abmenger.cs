using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class abmenger : SingletonAutoMono<abmenger>
{
    private AssetBundle mainAB = null;
    private AssetBundleManifest manifest = null;

    private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

    private string PathUrl
    {
        get
        {
            return Application.streamingAssetsPath + '/';
        }
    }
    public Object LoadRes(string abName, string resName)
    {
        if (mainAB == null)
        {
            mainAB=AssetBundle.LoadFromFile(PathUrl+'/'+"pc");
            manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            
        }

        AssetBundle ab = null;
        string[] strs = manifest.GetAllDependencies(abName);
        //Debug.Log(strs.Length);
        for (int i = 0; i < strs.Length; i++)
        {
            if (!abDic.ContainsKey(strs[i]))
            {
                ab=AssetBundle.LoadFromFile(PathUrl+strs[i]);
                abDic.Add(strs[i],ab);
                
            }
        }
        
        if (!abDic.ContainsKey(abName))
        {
            ab=AssetBundle.LoadFromFile(PathUrl+abName);
            abDic.Add(abName,ab);
        }

        Debug.Log(abDic[abName].LoadAsset(resName).GetType());
        return abDic[abName].LoadAsset(resName);
    }
}
