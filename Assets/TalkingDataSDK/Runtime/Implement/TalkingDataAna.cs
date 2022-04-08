using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace TalkingDataSDK
{
    public class TalkingDataAna: IAnalyzeEvent, IInitializable
    {
        public void Initialize()
        {
            var data = AScriptableObject.Get<TalkingDataPama>();
            TalkingDataGA.OnStart(data.appId, data.clannal);
            TalkingDataGA.SetDeviceToken();
            if (!data.enableLog)
                TalkingDataGA.SetVerboseLogDisabled();
            CreateLifeCycleGo();
            if (data.forCreateApp)
            {
                ForCreateApp();
            }
        }
        void CreateLifeCycleGo()
        {
            var clone = new GameObject();
            Object.DontDestroyOnLoad(clone);
            clone.hideFlags = HideFlags.HideInHierarchy | HideFlags.DontSave;
            clone.AddComponent<TalkingDataLifeCycle>();
        }
        void ForCreateApp()
        {
            OpenUserTrace();
        }
        
        public void SetEvent(string key)
        {
            TalkingDataGA.OnEvent(key,null);
        }

        public void SetEvent(string key, Dictionary<string, string> value)
        {
            var dic = new Dictionary<string, object>();
            foreach (var item in value)
            {
                dic.Add(item.Key, item.Value);
            }
            TalkingDataGA.OnEvent(key,dic);
        }
        #region 仅创建应用时使用
        void OpenUserTrace()
        {
            //游戏玩家以匿名（快速登录）方式在国服2区进行游戏时，做如下调用
            TDGAProfile profile = TDGAProfile.SetProfile("10000");
            profile.SetProfileType(ProfileType.ANONYMOUS);
            profile.SetLevel(1);
            profile.SetGameServer("国服2");
            //玩家升级时，做如下调用
            profile.SetLevel(2);
            //玩家显性注册成功时，做如下调用
            profile.SetProfileName("5830000@qq.com");
            profile.SetProfileType(ProfileType.QQ);
            profile.SetAge(18);
            profile.SetGender(Gender.MALE);

            //在向支付宝支付SDK发出请求时，同时调用：
            TDGAVirtualCurrency.OnChargeRequest("profile123-0923173248-11", "大号宝箱", 100, "CNY", 1000, "AliPay");
            //订单profile123-0923173248-11充值成功后调用：
            TDGAVirtualCurrency.OnChargeSuccess("profile123-0923173248-11");

            TDGAVirtualCurrency.OnReward(5, "新手奖励");

            TDGAItem.OnPurchase("helmet1", 2, 25);
            TDGAItem.OnUse("helmet1", 1);

            // 玩家进入名称为“蓝色龙之领地”的关卡。
            TDGAMission.OnBegin("蓝色龙之领地");
            // 玩家成功打过了关卡
            TDGAMission.OnCompleted("蓝色龙之领地");
        }
        #endregion
    }
}
