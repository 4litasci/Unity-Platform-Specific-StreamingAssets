# Unity Platform Specific StreamingAssets

Simple usage of platform specific StreamingAssets folder. Working both iOS and Android when Building with Unity Cloud Build or Local Build.

## Usage

Copy your files from target destination to Assets/StreamingAssets folder. 

```csharp
public void OnPreprocessBuild(BuildReport report)
    {
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
```

After build finished delete Assets/StreamingAssets folder.

```csharp
public class DeleteAfterBuild
{
    [PostProcessBuild(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject) {
   
        
        if (Directory.Exists(Path.Combine(Application.dataPath, "StreamingAssets")))
        {
            Directory.Delete(Path.Combine(Application.dataPath, "StreamingAssets"),true);
        }
        if (File.Exists(Path.Combine(Application.dataPath, "StreamingAssets.meta")))
        {
            File.Delete(Path.Combine(Application.dataPath, "StreamingAssets.meta"));
        }

    }
}
```

My personal advice copy frequently used files to Application.persistentDataPath and access later usages. There is a simple example usage in project. I strongly advice you to use  [BetterStreamingAssets](https://github.com/gwiazdorrr/BetterStreamingAssets)  