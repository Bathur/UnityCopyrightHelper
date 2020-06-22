using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Bathur.CopyrightHelper.Editor
{
    public static class CopyrightHelperConst
    {
        public static string ConfigAssetDir
        {
            get
            {
                return Path.Combine("Assets", "Editor");
            }
        }

        public static string ConfigAssetFileName
        {
            get
            {
                return Path.Combine("CopyrightHelperConfig.asset");
            }
        }

        public static string ConfigAssetPath
        {
            get
            {
                return Path.Combine(ConfigAssetDir, ConfigAssetFileName);
            }
        }

        public static string DefaultCopyrightFormatString
        {
            get
            {
                return
                        "//=====================================================" + Environment.NewLine +
                        "// - FileName:      #SCRIPTNAME#.cs" + Environment.NewLine +
                        "// - Created:       #AUTORNAME#" + Environment.NewLine +
                        "// - Time:          #CREATETIME#" + Environment.NewLine +
                        "// - Email:         #AUTHOREMAIL#" + Environment.NewLine +
                        "// - Description:   " + Environment.NewLine + Environment.NewLine +
                        "// - (C) Copyright #STARTYEAR# - #CURRENTYEAR#, #COPYRIGHTOWNER#" + Environment.NewLine +
                        "// - All Rights Reserved." + Environment.NewLine +
                        "//======================================================";
            }
        }

        public static string DefaultDateFormatString
        {
            get
            {
                return "yyyy/MM/dd HH:mm:ss";
            }
        }

        private static CopyrightHelperConfig helperConfig;

        public static CopyrightHelperConfig HelperConfig
        {
            get
            {
                if (!File.Exists(ConfigAssetPath))
                {
                    CreateCopyrightHelperConfig();
                }
                helperConfig = LoadCopyrightHelperConfig();
                return helperConfig;
            }
        }

        private static void CreateCopyrightHelperConfig()
        {
            CopyrightHelperConfig CopyrightHelperConfigAsset = ScriptableObject.CreateInstance<CopyrightHelperConfig>();

            if (!Directory.Exists(ConfigAssetDir))
            {
                Directory.CreateDirectory(ConfigAssetDir);
            }

            AssetDatabase.CreateAsset(CopyrightHelperConfigAsset, ConfigAssetPath);
            AssetDatabase.Refresh();
        }

        private static CopyrightHelperConfig LoadCopyrightHelperConfig()
        {
            return AssetDatabase.LoadAssetAtPath<CopyrightHelperConfig>(ConfigAssetPath);
        }
    }

    public class DisplayOnly : PropertyAttribute { }

    [CustomPropertyDrawer(typeof(DisplayOnly))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}
