using System;
using UnityEditor;
using UnityEngine;

namespace UnityStandardAssets.ImageEffects
{
    [CustomEditor(typeof(ColorCorrectionLookup))]
    class ColorCorrectionLookupEditor : Editor
    {
        SerializedObject serObj;

        private Texture2D tempClutTex2D;

        void OnEnable()
        {
            serObj = new SerializedObject(target);
        }

        public override void OnInspectorGUI()
        {
            serObj.Update();

            EditorGUILayout.LabelField("Converts textures into color lookup volumes (for grading)", EditorStyles.miniLabel);

            tempClutTex2D = EditorGUILayout.ObjectField(" Based on", tempClutTex2D, typeof(Texture2D), false) as Texture2D;

            if (tempClutTex2D == null)
            {
                var path = ((ColorCorrectionLookup)target).basedOnTempTex;
                if (!string.IsNullOrEmpty(path))
                {
                    Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(path);
                    if (t) tempClutTex2D = t;
                }
            }

            Texture2D tex = tempClutTex2D;

            if (tex && ((ColorCorrectionLookup)target).basedOnTempTex != AssetDatabase.GetAssetPath(tex))
            {
                EditorGUILayout.Space();

                if (!((ColorCorrectionLookup)target).ValidDimensions(tex))
                {
                    EditorGUILayout.HelpBox("Invalid texture dimensions!\nPick another texture or adjust dimensions to e.g. 256x16.", MessageType.Warning);
                }
                else if (GUILayout.Button("Convert and Apply"))
                {
                    string path = AssetDatabase.GetAssetPath(tex);
                    TextureImporter textureImporter = AssetImporter.GetAtPath(path) as TextureImporter;
                    bool doImport = false;

                    if (textureImporter != null)
                    {
                        if (!textureImporter.isReadable || textureImporter.mipmapEnabled || textureImporter.textureCompression != TextureImporterCompression.Uncompressed)
                        {
                            doImport = true;
                        }

                        if (doImport)
                        {
                            textureImporter.isReadable = true;
                            textureImporter.mipmapEnabled = false;
                            textureImporter.textureCompression = TextureImporterCompression.Uncompressed;

                            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
                        }
                    }

                    ((ColorCorrectionLookup)target).Convert(tex, path);
                }
            }

            if (!string.IsNullOrEmpty(((ColorCorrectionLookup)target).basedOnTempTex))
            {
                EditorGUILayout.HelpBox("Using " + ((ColorCorrectionLookup)target).basedOnTempTex, MessageType.Info);

                Texture2D t = AssetDatabase.LoadAssetAtPath<Texture2D>(((ColorCorrectionLookup)target).basedOnTempTex);
                if (t)
                {
                    Rect r = GUILayoutUtility.GetLastRect();
                    r = GUILayoutUtility.GetRect(r.width, 20);
                    r.x += r.width * 0.05f / 2.0f;
                    r.width *= 0.95f;
                    GUI.DrawTexture(r, t, ScaleMode.ScaleToFit);
                    GUILayoutUtility.GetRect(r.width, 4);
                }
            }

            serObj.ApplyModifiedProperties();
        }
    }
}
