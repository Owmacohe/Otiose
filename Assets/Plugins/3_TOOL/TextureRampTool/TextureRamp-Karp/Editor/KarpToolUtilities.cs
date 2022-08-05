#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

namespace Karprod
{
    public static class KarpToolUtilities
    {
        #region Path Research
        public static string PathAsking(string name = "FileName", string beginDirectory = "Assets/", string extension = "png", string title = "Asking for Path")
        {
            string path;
            path = EditorUtility.SaveFilePanel(title, beginDirectory, name, extension);
            path = FileUtil.GetProjectRelativePath(path);
            return path;
        }
        public static string TexturePathAsking(string name, TextureType fileType = TextureType.PNG, string beginDirectory = "Assets/")
        {
            string TextureTypeExtension(TextureType fileType)
            {
                switch (fileType)
                {
                    case TextureType.PNG: return "png";
                    case TextureType.JPG: return "jpg";
                    default: return "png";
                }
            }
            return PathAsking(name, beginDirectory, TextureTypeExtension(fileType), "Save Texture Asset");
        }

        /// <summary> Name is without the extension (.cs) </summary>
        /// <param name="scripName"></param>
        /// <returns></returns>
        public static string FindScriptFolder(string scripName, bool relative = true)
        {
            string[] fullFilePath = System.IO.Directory.GetFiles(Application.dataPath, scripName + ".cs", System.IO.SearchOption.AllDirectories);
            if (fullFilePath.Length == 0)
            {
                //Can't find script (verify scripName)
                return null;
            }
            string path = fullFilePath[0].Replace(scripName + ".cs", "").Replace("\\", "/");
            if (relative)
            {
                return "Assets" + path.Split("Assets")[1];
            }
            else
            {
                return path;
            }
        }
        #endregion

        public static void HighLightObject(string floaderPath, string objName)
        {
            SettingsService.OpenProjectSettings(floaderPath + objName);
        }
        [System.Obsolete]
        public static void HighLightObject(ScriptableObject sco)
        {
            EditorGUIUtility.PingObject(sco);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = sco;
        }
    }
}
#endif