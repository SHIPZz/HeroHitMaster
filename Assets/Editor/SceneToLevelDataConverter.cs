using System.Collections.Generic;
using CodeBase.ScriptableObjects.LevelDataSo;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SceneToLevelDataConverter : EditorWindow
    {
        [MenuItem("Tools/Convert Scenes To LevelData")]
        public static void ConvertScenesToLevelData()
        {
            List<string> sceneNames = new List<string>();
        
            foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
            {
                if (scene.enabled)
                {
                    string sceneName = System.IO.Path.GetFileNameWithoutExtension(scene.path);
                    sceneNames.Add(sceneName);
                }
            }

            foreach (string sceneName in sceneNames)
            {
                LevelData levelData = ScriptableObject.CreateInstance<LevelData>();
                levelData.name = sceneName;
                AssetDatabase.CreateAsset(levelData, "Assets/Resources/Data/Level/" + sceneName + ".asset");
            }

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}