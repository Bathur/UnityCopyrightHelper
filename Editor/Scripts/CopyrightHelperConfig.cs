using Bathur.CopyrightHelper.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CopyrightHelperConfig : ScriptableObject
{
    [Serializable]
    public struct ReplacementValue
    {
        public string Key;
        public string Value;
    }

    [Header("Enable Copyright Helper?")]
    [Tooltip("[DISPLAY ONLY]\nPlease edit in\n'Project Settings > Bathur > Copyright Helper'")]
    [DisplayOnly]
    public bool EnableHelper;

    [Header("Copyright Format String:")]
    [Tooltip("[DISPLAY ONLY]\nPlease edit in\n'Project Settings > Bathur > Copyright Helper'")]
    [DisplayOnly]
    public string FormatString = CopyrightHelperConst.DefaultCopyrightFormatString;

    [HideInInspector]
    public List<ReplacementValue> ReplacementList = new List<ReplacementValue>();
}
