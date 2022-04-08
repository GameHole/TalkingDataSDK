using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using UnityEngine;
namespace TalkingDataSDK
{
	public class TalkingDataPost
	{
        [PostProcessBuild(999)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.iOS)
            {
                // Read.
                string projectPath = PBXProject.GetPBXProjectPath(path);
                PBXProject project = new PBXProject();
                project.ReadFromString(File.ReadAllText(projectPath));
                string targetName = PBXProject.GetUnityTargetName();
                string targetGUID = project.TargetGuidByName(targetName);

                AddFrameworks(project, targetGUID);

                // Write.
                File.WriteAllText(projectPath, project.WriteToString());
            }
        }

        static void AddFrameworks(PBXProject project, string targetGUID)
        {
            // Frameworks 

            project.AddFrameworkToProject(targetGUID, "AdSupport.framework", false);
            project.AddFrameworkToProject(targetGUID, "CoreTelephony.framework", false);
            project.AddFrameworkToProject(targetGUID, "CoreMotion.framework", false);
            project.AddFrameworkToProject(targetGUID, "Security.framework", false);
            project.AddFrameworkToProject(targetGUID, "SystemConfiguration.framework", false);
            project.AddFrameworkToProject(targetGUID, "libc++.tbd", false);
            project.AddFrameworkToProject(targetGUID, "libz.tbd", false);
        }
       
    }
}
