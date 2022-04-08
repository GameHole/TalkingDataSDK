using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
using UnityEditor;
namespace TalkingDataSDK
{
    [InitializeOnLoad]
	public class TalkingDataEditor
	{
        static TalkingDataEditor()
        {
            if (!AScriptableObject.Get<TalkingDataPama>())
                SetPama();
        }
        [MenuItem("SDK/创建TalkingData参数")]
        static void SetPama()
        {
            AssetHelper.CreateAsset<TalkingDataPama>();
            SetXml();
            AssetDatabase.Refresh();
        }
        static void SetXml()
        {
            var dc = XmlHelper.GetAndroidManifest();
            var actUnity = dc.FindNode("/manifest/application/activity", "android:name", "com.unity3d.player.UnityPlayerActivity");
            var findMeta = dc.FindNode("/manifest/application/activity/meta-data", "android:name", "android.app.lib_name");
            if (findMeta != null) return;
            var meta = dc.CreateElement("meta-data");
            meta.AppendAttribute("name", "android.app.lib_name")
                .AppendAttribute("value", "unity");
            actUnity.AppendChild(meta);
            meta = dc.CreateElement("meta-data");
            meta.AppendAttribute("name", "unityplayer.ForwardNativeEventsToDalvik")
                .AppendAttribute("value", "false");
            actUnity.AppendChild(meta);
            dc.SetPermission("android.permission.INTERNET");
            dc.SetPermission("android.permission.READ_PHONE_STATE");
            dc.SetPermission("android.permission.ACCESS_NETWORK_STATE");
            dc.SetPermission("android.permission.WRITE_EXTERNAL_STORAGE");
            dc.SetPermission("android.permission.ACCESS_WIFI_STATE");
            dc.SetPermission("android.permission.ACCESS_COARSE_LOCATION");
            dc.SetPermission("android.permission.GET_TASKS");
            dc.SetPermission("android.permission.ACCESS_FINE_LOCATION");
            dc.Save();
        }
    }
}
