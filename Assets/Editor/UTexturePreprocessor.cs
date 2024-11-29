using UnityEditor;
using UnityEngine;

namespace PostProcessor
{
    public class UTexturePreprocessor : AssetPostprocessor
    {
        public static bool IsHD = false;

        void OnPreprocessTexture()
        {
            TextureImporter textureImporter = (TextureImporter)assetImporter;
            textureImporter.isReadable = false;

            if (assetPath.StartsWith("Assets/UI"))
            {
                textureImporter.mipmapEnabled = false; // 自动禁用 mipmap
                textureImporter.textureType = TextureImporterType.Sprite;
            }
            else if (assetPath.StartsWith("Assets/Units"))
            {
                // 使用 textureCompression 替代 textureFormat
                textureImporter.textureCompression = TextureImporterCompression.Compressed;
            }
            else if (assetPath.StartsWith("Assets/FX/Textures"))
            {
                // 使用 PlatformTextureSettings 来设置平台特定的格式
                if (textureImporter.textureCompression == TextureImporterCompression.Uncompressed)
                {
                    TextureImporterPlatformSettings platformSettings = new TextureImporterPlatformSettings();
                    platformSettings.format = TextureImporterFormat.RGBA16; // 示例，设置为合适的格式
                    textureImporter.SetPlatformTextureSettings(platformSettings);
                }
                else
                {
                    textureImporter.textureCompression = TextureImporterCompression.Compressed;
                }
            }
        }
    }
}
