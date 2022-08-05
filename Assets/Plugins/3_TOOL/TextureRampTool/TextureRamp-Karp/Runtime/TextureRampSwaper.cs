using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Karprod
{
    public class TextureRampSwaper : MonoBehaviour
    {
        public Gradient gradient;


        public Renderer objectRender;
        public string propTextRampName = "_TextureRamp";

        public MaterialPropertyBlock propBlock;

        void Start()
        {
            ChangeGradient();
        }

        void Update()
        {
            ChangeGradient();
        }

        [ContextMenu("Change Gradient")]
        public void ChangeGradient()
        {
            //permet d'overide les param sans modif le mat ou créer d'instance
            propBlock = new MaterialPropertyBlock();

            //Recup Data
            objectRender.GetPropertyBlock(propBlock);

            //EditZone
            propBlock.SetTexture(propTextRampName, TextureRampGenerator.Generate(gradient));

            //Push Data
            objectRender.SetPropertyBlock(propBlock);
        }
    }
}
