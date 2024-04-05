using System.Linq;
using CodeBase.ScriptableObjects.LevelDataSo;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Utils
{
    public class SceneSelectionEditor : OdinMenuEditorWindow
    {
        private OdinMenuTree _tree;

        [MenuItem("Tools/Select Scene")]
        public static void SelectScene()
        {
            GetWindow<SceneSelectionEditor>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            _tree = new OdinMenuTree();

            _tree.AddAllAssetsAtPath("Select Scene", "Assets/Resources/Data/Level/");

            return _tree;
        }

        protected override void OnBeginDrawEditors()
        {
            base.OnBeginDrawEditors();

            if (GUILayout.Button("Apply"))
            {
                RememberSelectedScene();
            }
        }

        private void RememberSelectedScene()
        {
            var selectedScene = _tree.Selection.FirstOrDefault()?.Value as LevelData;

            if (selectedScene != null)
            {
                EditorPrefs.SetString("Level id", selectedScene.Id);
                return;
            }

            Debug.LogWarning("Select SceneId!");
        }
    }
}