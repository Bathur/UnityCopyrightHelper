using System.IO;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Bathur.CopyrightHelper.Editor
{
    static class CopyrightHelperEditor
    {
        static bool isConfigFileExist = false;
        static CopyrightHelperConfig HelperConfig;

        static void ProcessingConfigFile()
        {
            HelperConfig = CopyrightHelperConst.HelperConfig;
            isConfigFileExist = true;
        }

        static void ProcessingFormatString()
        {
            HelperConfig.ReplacementList.Clear();

            MatchCollection KeyCollection = Regex.Matches(HelperConfig.FormatString, @"#[_a-zA-Z0-9]+#");
            foreach (Match item in KeyCollection)
            {
                if (item.Value == "#CREATETIME#")
                {
                    HelperConfig.ReplacementList.Add(new CopyrightHelperConfig.ReplacementValue { Key = item.Value, Value = CopyrightHelperConst.DefaultDateFormatString });
                }
                else
                {
                    HelperConfig.ReplacementList.Add(new CopyrightHelperConfig.ReplacementValue { Key = item.Value, Value = "" });
                }
            }

        }

        [SettingsProvider]
        public static SettingsProvider CopyrightHelperEditorSettingsProvider()
        {
            SettingsProvider Provider = new SettingsProvider("Bathur/Copyright Helper", SettingsScope.Project, , new string[] { "Copyright", "Helper", "Bathur" })
            {
                label = "Copyright Helper",
                activateHandler = (SearchContext, RootElement) =>
                {
                    if (File.Exists(CopyrightHelperConst.ConfigAssetPath))
                    {
                        ProcessingConfigFile();
                    }
                },
                guiHandler = (SearchContext) =>
                {
                    if (isConfigFileExist)
                    {
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.Space();

                        if (EditorGUILayout.Toggle("Enable Copyright Helper? ", HelperConfig.EnableHelper))
                        {
                            HelperConfig.EnableHelper = true;

                            EditorGUILayout.Space();

                            EditorGUILayout.HelpBox("NOTE:\nPlease wrap letters, numbers or underscores with two pound signs(#)", MessageType.Info);
                            EditorGUILayout.HelpBox("Taken over by the plugin: #SCRIPTNAME#, #CREATETIME#", MessageType.Info);
                            EditorGUILayout.LabelField("Copyright Format String: ");
                            HelperConfig.FormatString = EditorGUILayout.TextArea(HelperConfig.FormatString);

                            EditorGUILayout.Space();
                            if (GUILayout.Button("Handle Format String"))
                            {
                                ProcessingFormatString();
                            }

                            EditorGUILayout.Space();
                            EditorGUILayout.LabelField("Replacement List: ");
                            if (HelperConfig.ReplacementList.Count == 0)
                            {
                                EditorGUILayout.HelpBox("Empty list!\nPlease check if the format string is empty.\nOr if you click the process button?", MessageType.Info);
                            }
                            else
                            {
                                for (int i = 0; i < HelperConfig.ReplacementList.Count; i++) 
                                {
                                    var Item = HelperConfig.ReplacementList[i];
                                    if (Item.Key == "#SCRIPTNAME#")
                                    {
                                        EditorGUILayout.HelpBox("#SCRIPTNAME# CANNOT be customized", MessageType.Info);
                                    }
                                    else if (Item.Key == "#CREATETIME#")
                                    {
                                        EditorGUILayout.HelpBox("#CREATETIME# 's custom content should be a date format string", MessageType.Info);
                                        Item.Value = EditorGUILayout.TextField(Item.Key, Item.Value);
                                    }
                                    else
                                    {
                                        Item.Value = EditorGUILayout.TextField(Item.Key, Item.Value);
                                    }
                                    HelperConfig.ReplacementList[i] = Item;
                                }
                            }
                        }
                        else
                        {
                            HelperConfig.EnableHelper = false;
                        }

                        EditorGUILayout.EndVertical();
                    }
                    else
                    {
                        EditorGUILayout.BeginVertical();

                        EditorGUILayout.Space();

                        EditorGUILayout.HelpBox("No configuration file found, please create first", MessageType.Warning);

                        EditorGUILayout.Space();
                        if (GUILayout.Button("Create Copyright Helper Config Asset"))
                        {
                            ProcessingConfigFile();
                        }

                        EditorGUILayout.EndVertical();
                    }
                },
                deactivateHandler = () =>
                {

                }
            };

            return Provider;
        }
    }
}