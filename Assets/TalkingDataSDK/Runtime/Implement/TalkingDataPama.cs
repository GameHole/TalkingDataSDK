using System.Collections.Generic;
using UnityEngine;
namespace TalkingDataSDK
{
    public class TalkingDataPama : AScriptableObject
    {
        public override string filePath => "TalkingData参数";
        public string appId;
        public string clannal;
        public bool enableLog;
        public bool forCreateApp;
    }
}
