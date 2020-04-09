using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class CopyBeforeBuild : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        // First we will copy our source folder before build. Then we will delete all StreamingAssets folder.
        // Basically import a folder to project and delete after.
        // Don't forget. Put your xxx ProcessBuild scripts to Editor folder.
        // You can choose CopySource folder wherever you want BUT if you use Unity Cloud Build, Put them in your project Folder!!
        // This work well both custom user build and Unity Cloud Build. 
#if UNITY_ANDROID
        var copyPath = Path.Combine(Directory.GetCurrentDirectory(), "CopySource", "android");
        var destinationPath = Path.Combine(Application.dataPath, "StreamingAssets","android");
        DirectoryCopy(copyPath,destinationPath);
#elif UNITY_IOS
        var copyPath = Path.Combine(Directory.GetCurrentDirectory(), "CopySource", "ios");
        var destinationPath = Path.Combine(Application.dataPath, "StreamingAssets","ios");
        DirectoryCopy(copyPath,destinationPath);
#endif
    }
    
    private static void DirectoryCopy(string sourceDirName, string destDirName)
    {
        DirectoryInfo dir = new DirectoryInfo(sourceDirName);
        if (!dir.Exists)
            return;

        if (!Directory.Exists(destDirName))
        {
            Directory.CreateDirectory(destDirName);
        }

        FileInfo[] files = dir.GetFiles();

        foreach (FileInfo file in files)
        {
            string temppath = Path.Combine(destDirName, file.Name);
            file.CopyTo(temppath, true);
        }

        DirectoryInfo[] dirs = dir.GetDirectories();
        foreach (DirectoryInfo subdir in dirs)
        {
            string temppath = Path.Combine(destDirName, subdir.Name);
            DirectoryCopy(subdir.FullName, temppath);
        }
    }
}
