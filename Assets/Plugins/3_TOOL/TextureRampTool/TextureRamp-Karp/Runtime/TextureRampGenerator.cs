using System;
using UnityEngine;

namespace Karprod
{
    public static class TextureRampGenerator
    {
        public static Texture2D Generate(Gradient grd, int size = 256, bool alpha = true) { return Generate(grd, new Vector2Int(size, size), alpha); }
        public static Texture2D Generate(Gradient grd, int width, int height, bool alpha = true) { return Generate(grd, new Vector2Int(width, height), alpha); }
        public static Texture2D Generate(Gradient grd, Vector2Int size, bool alpha = true)
        {
            //Var pour répartir le gradient sur la texture
            float hToGrd = (float)1.0f / size.x;

            //Création de la Texture
            Texture2D textureRamp = TextureGenerator.Generate(size.x, size.y, alpha);

            //define the texture ramp on horizontal axis
            Color[] cols = new Color[size.x];
            for (int x = 0; x < size.x; x++)
            {
                cols[x] = grd.Evaluate(x * hToGrd);
            }

            //extend it on the texture height
            for (int y = 0; y < size.y; y++)
            {
                textureRamp.SetPixels(0, y, size.x, 1, cols);
            }

            textureRamp.Apply();

            return textureRamp;
        }
    }
}
