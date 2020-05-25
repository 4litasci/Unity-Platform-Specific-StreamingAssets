using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public class DeleteAfterBuild
{
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
        
        // Let's delete all streaming assets folder and meta file. 
        
        if (Directory.Exists(Path.Combine(Application.dataPath, "StreamingAssets")))
        {
            Directory.Delete(Path.Combine(Application.dataPath, "StreamingAssets"),true);
        }
        if (File.Exists(Path.Combine(Application.dataPath, "StreamingAssets.meta")))
        {
            File.Delete(Path.Combine(Application.dataPath, "StreamingAssets.meta"));
        }
        
        AssetDatabase.Refresh();
    }
}
