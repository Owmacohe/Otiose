using UnityEngine;

#if UNITY_EDITOR
namespace Karprod
{
    public static class TextureRampCreator
    {
        public static void Create(Gradient grd, string fullPath, TextureType fileType, Vector2Int size)
        {
            //Creating The texture
            Texture2D texture = TextureRampGenerator.Generate(grd, size.x, size.y, fileType == TextureType.PNG);

            //Check for copy
            TextureGenerator.DeleteCopy(fullPath);

            //create the asset
            if (fileType == TextureType.JPG)
            {
                TextureGenerator.TextureToAssetJPG(fullPath, texture);
            }
            else
            {
                TextureGenerator.TextureToAssetPNG(fullPath, texture);
            }

            //Debug.log the test       
            if (TextureGenerator.TestTexture2DAtPath(fullPath))
            {
                Debug.Log("The TextureRamp have been successfully Created");
            }
            else
            {
                Debug.LogError("Error during the TextureRamp creation");
            }
        }
        public static void Create(Gradient grd, string fullPath, TextureType fileType = TextureType.PNG, int size = 256)
        {
            Create(grd, fullPath, fileType, new Vector2Int(size, size));
        }
        public static void Create(Gradient grd, string fullPath, TextureType fileType = TextureType.PNG, int width = 256, int height = 256)
        {
            Create(grd, fullPath, fileType, new Vector2Int(width, height));
        }
    }
}
#endif