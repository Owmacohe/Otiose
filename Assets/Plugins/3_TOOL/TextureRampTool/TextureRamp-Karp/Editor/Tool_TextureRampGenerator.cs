using UnityEditor;
using UnityEngine;

namespace Karprod
{
    public class Tool_TextureRampGenerator : EditorWindow
    {
        #region Constante
        //GUI
        private const int MinHeight = 50;
        private const int LeftBordWidth = 150;
        private const int Width = 250;
        private const int MaxWidth = Width * 2;
        #endregion
        #region Variable
        //Settings
        private TextureRampSettings settings;

        //Parameter
        private string rootPath = "Assets/";
        //
        private string prefixe = "TR_";
        private string textureName = "NewTextureRamp";
        private Gradient gradient;
        private TextureType actualTexType = TextureType.PNG;

        //Texture Size
        private bool isSquare = false;
        private int squareSize = 256;
        private Vector2Int size = new Vector2Int(256, 256);
        #endregion
        
        [MenuItem("Tools/KarProd/TextureRampGenerator")]
        public static void ShowWindow()
        {
            EditorWindow myWindow = GetWindow(typeof(Tool_TextureRampGenerator), false, "TextureRampGenerator");

            myWindow.name = "TextureRampGenerator";
            myWindow.minSize = new Vector2(550, 220);
        }

        private void OnGUI()
        {
            if(settings is null)
            {
                settings = TextureRampSettings.instance;
                //
                rootPath = settings.defaultPath;
                prefixe = settings.defaultPrefixe;
                textureName = settings.defaultName;
                gradient = settings.defaultGradient;
                squareSize = settings.defaultSize;
                size = new Vector2Int(squareSize, squareSize);
            }

            GUILayout.Space(5);
            //Main Property
            using (new GUILayout.VerticalScope())
            {
                GUILayout.Label("Texture Ramp", EditorStyles.boldLabel);
                using (new GUILayout.HorizontalScope())
                {
                    prefixe = EditorGUILayout.TextField("Prefixe", prefixe, GUILayout.Width(LeftBordWidth * 1.5f));
                    textureName = EditorGUILayout.TextField("Name", textureName);
                }
                gradient = EditorGUILayout.GradientField("Gradient", gradient);
            }
            GUILayout.Space(15);
            //Texture Size
            using (new GUILayout.VerticalScope())
            {
                using (new GUILayout.HorizontalScope())
                {
                    GUILayout.Label("Texture Size", EditorStyles.boldLabel);
                    GUILayout.Label("", EditorStyles.boldLabel);
                }
                using (new GUILayout.HorizontalScope())
                {
                    isSquare = EditorGUILayout.Toggle("Texture Square ?", isSquare, GUILayout.Width(LeftBordWidth * 1.5f));
                    if (isSquare)
                    {
                        squareSize = EditorGUILayout.IntField("", squareSize);
                    }
                    else
                    {
                        size = EditorGUILayout.Vector2IntField("", new Vector2Int(size.x, size.y));
                    }
                }
            }
            GUILayout.Space(5);
            //Texture Format
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Texture Format", EditorStyles.boldLabel, GUILayout.Width(LeftBordWidth));
                using (new GUILayout.HorizontalScope())
                {
                    TextureFormatButton("PNG", TextureType.PNG);
                    TextureFormatButton("JPG", TextureType.JPG);
                }
            }
            GUILayout.Space(10);
            //Creation Button
            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("", EditorStyles.boldLabel, GUILayout.Width(LeftBordWidth));
                using (new GUILayout.VerticalScope(EditorStyles.helpBox))
                {
                    if (GUILayout.Button("Generate Texture Ramp", GUILayout.MinHeight(MinHeight)))
                    {
                        ButtonCreateGradient();
                    }
                }
            }
        }

        void TextureFormatButton(string name, TextureType texType)
        {
            GUIContent content = new GUIContent(name);

            var oldBackGroundColor = GUI.backgroundColor;
            var oldContentColor = GUI.contentColor;

            if (actualTexType == texType)
            {
                GUI.backgroundColor = new Color(0.3f, 0.3f, 0.3f);
                GUI.contentColor = Color.white;
            }

            if (GUILayout.Button(content, GetButtonStyle(), GUILayout.Height(25), GUILayout.MinWidth(40), GUILayout.MaxWidth((Width + 50) / 2)))
            {
                actualTexType = texType;
            }

            GUI.backgroundColor = oldBackGroundColor;
            GUI.contentColor = oldContentColor;
        }
        GUIStyle GetButtonStyle()
        {
            var s = new GUIStyle(GUI.skin.button);
            s.margin.left = 0;
            s.margin.top = 0;
            s.margin.right = 0;
            s.margin.bottom = 0;
            s.border.left = 0;
            s.border.top = 0;
            s.border.right = 0;
            s.border.bottom = 0;
            return s;
        }

        private void ButtonCreateGradient()
        {
            rootPath = KarpToolUtilities.TexturePathAsking(prefixe + textureName, actualTexType, rootPath);
            if (rootPath is null || rootPath == string.Empty)
            {
                Debug.Log("[TOOL : TextureRampKarp] TextureRamp creation have been cancel");
                return;
            }

            if (isSquare)
            {
                TextureRampCreator.Create(gradient, rootPath, actualTexType, squareSize);
            }
            else
            {
                TextureRampCreator.Create(gradient, rootPath, actualTexType, size);
            }
        }
    }
}