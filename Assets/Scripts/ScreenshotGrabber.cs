#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;
    
public class ScreenshotGrabber
{
    [MenuItem("Screenshot/Grab")]
    public static void Grab()
    {   
        string folderPath = Directory.GetCurrentDirectory() + "/Assets/Sprites";
 
         if (!System.IO.Directory.Exists(folderPath))
             System.IO.Directory.CreateDirectory(folderPath);
 
         var screenshotName = 
                                 "Screenshot_" + 
                                 System.DateTime.Now.ToString("dd-MM-yyyy-HH-mm-ss") + 
                                 ".png";
         ScreenCapture.CaptureScreenshot(System.IO.Path.Combine(folderPath, screenshotName));
    }
}
#endif
