using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class TakeScreenshot : MonoBehaviour
{
    private string path;
    
    public void TakeGameScreenshot()
    {
        TakeScreenshotAndShare();
    }

    private void TakeScreenshotAndShare()
    {
        Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
        ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
        ss.Apply();

        string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
        File.WriteAllBytes( filePath, ss.EncodeToPNG() );

        // To avoid memory leaks
        Destroy( ss );

        path = filePath;

    }

    public void ShareSS()
    {
#if UNITY_ANDROID
        new NativeShare().AddFile( path )
            .SetSubject( "'The Wombats Racing " )
            .SetText( "Check out the Official Racing Game of the super awesome Indie rock band 'The Wombats' " +"\n" + " #thewombatsgame "+ "\n")
            .SetUrl( "https://play.google.com/store/apps/details?id=com.begames.thewombats" )
            .SetCallback( ( result, shareTarget ) => Debug.Log( "Share result: " + result + ", selected app: " + shareTarget ) )
            .Share();
    }
    #endif
}
