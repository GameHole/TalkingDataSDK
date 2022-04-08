using System.Collections.Generic;
using UnityEngine;
namespace TalkingDataSDK
{
	public class TalkingDataLifeCycle:MonoBehaviour
	{
#if UNITY_ANDROID
        private void OnApplicationPause(bool pause)
        {
            var app = TalkingDataGA.gameAnalyticsClass;
            if (app != null)
            {
                var activity = TalkingDataGA.GetCurrentActivity();
                if (pause)
                {
                    app.CallStatic("onPause", activity);
                }
                else
                {
                    app.CallStatic("onResume", activity);
                }
            }
        }
        private void OnDestroy()
        {
            TalkingDataGA.OnKill();
        }
#endif
    }
}
