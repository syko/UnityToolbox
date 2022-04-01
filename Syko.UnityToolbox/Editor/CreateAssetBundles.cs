using UnityEditor;
using UnityEngine;
using System.IO;

namespace Syko.UnityToolbox
{
  public class CreateAssetBundles
  {
    [MenuItem("Assets/Build AssetBundles")]
    static void BuildAllAssetBundles()
    {
      string assetBundleDirectory = Path.Combine(Application.streamingAssetsPath, "AssetBundles");
      if (!Directory.Exists(assetBundleDirectory))
      {
        Directory.CreateDirectory(assetBundleDirectory);
      }
      BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                      BuildAssetBundleOptions.None,
                                      EditorUserBuildSettings.activeBuildTarget);
    }
  }
}
