using System;
using System.IO;

namespace Bathur.CopyrightHelper.Editor
{
    public class Copyright : UnityEditor.AssetModificationProcessor
    {
        static CopyrightHelperConfig HelperConfig;

        private static void OnWillCreateAsset(string path)
        {
            path = path.Replace(".meta", "");
            if (Path.GetExtension(path) == ".cs")
            {
                HelperConfig = CopyrightHelperConst.HelperConfig;

                if (HelperConfig == null)
                {
                    return;
                }

                if (HelperConfig.EnableHelper == false)
                {
                    return;
                }

                string CopyrightString = HelperConfig.FormatString;

                foreach (var item in HelperConfig.ReplacementList)
                {
                    if (item.Key == "#SCRIPTNAME#")
                    {
                        CopyrightString = CopyrightString.Replace(item.Key, Path.GetFileNameWithoutExtension(path));
                    }
                    else if (item.Key == "#CREATETIME#")
                    {
                        CopyrightString = CopyrightString.Replace(item.Key, System.DateTime.Now.ToString(item.Value));
                    }
                    else
                    {
                        CopyrightString = CopyrightString.Replace(item.Key, item.Value);
                    }
                }

                string AllText = File.ReadAllText(path);
                AllText = CopyrightString + Environment.NewLine + Environment.NewLine + AllText;

                File.WriteAllText(path, AllText);
                UnityEditor.AssetDatabase.Refresh();
            }

        }
    }
}