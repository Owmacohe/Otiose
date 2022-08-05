using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karprod
{
    [System.Serializable]
    public class TextureRampSettings : ScriptableObject
	{
        #region Singleton
        static TextureRampSettings _instance;
		public static TextureRampSettings instance
		{
			get
			{
				if (_instance != null) return _instance;
				else
				{
					TextureRampSettings trs = Resources.Load("TextureRampSettings") as TextureRampSettings;
					_instance = trs;
					return _instance;
				}
			}
		}
        #endregion

		[Header("Default Value")]
		public string defaultPath = "Assets/";

		[Space(5)]
		public string defaultPrefixe = "TR_";
		public string defaultName = "NewTextureRamp";

		[Space(15)]
		public Gradient defaultGradient = new Gradient() { colorKeys = new GradientColorKey[5] 
		{
				new GradientColorKey(Color.red, 0),
				new GradientColorKey(Color.yellow, 0.25f),
				new GradientColorKey(Color.green, 0.5f),
				new GradientColorKey(Color.cyan, 0.75f),
				new GradientColorKey(Color.blue, 1)
		}};
		public int defaultSize = 256;

		[Space(15)]
		public ComputeShader textureCompute;
	}
}
