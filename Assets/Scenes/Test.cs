using System.Collections.Generic;
using UnityEngine;
using MiniGameSDK;
namespace Default
{
	public class Test:MonoBehaviour
	{
        IAnalyzeEvent analyze;
        private void Start()
        {
           
        }
        public void Send()
        {
            analyze.SetEvent("test_aaa");
        }
        public void SendPama()
        {
            analyze.SetEvent("test_aaa",new Dictionary<string, string> { {"testP","AAA" } });
        }
    }
}
