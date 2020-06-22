# UnityCopyrightHelper
Add customizable copyright information when creating a new script in Unity.
# How to use?
- Use UPM(**Recommend**)
  1. Open UnityPackageManager(UPM)
  2. Click "+"(In the upper left corner of the window)
  3. Click "Add package from git URL"
  4. Input `git@github.com:Bathur/UnityCopyrightHelper.git`
  5. Clcik "Add"
- Download ZIP
  1. Download ZIP
  2. Unzip it
  3. Put the folder anywhere in your Asset folder
# Notices
- You can find the settings of this plugin in "Edit > Project Settings > Bathur > Copyright Helper"
- When you customize copyright information, please wrap letters, numbers or underscores with two pound signs(#)
- Taken over by the plugin: `#SCRIPTNAME#`, `#CREATETIME#`
  - `#SCRIPTNAME#` **CANNOT** be customized, it will always be replaced with the file name of the new script
  - `#CREATETIME#`'s custom content should be a date format string, it will always be replaced with the time when the new script was created
# Requirements
- Unity 2019.4 or later
# Author Info
Blog: http://bathur.cn/
# License
This project is under the MIT License.
