/* 
*   NatCorder
*   Copyright (c) 2022 NatML Inc. All Rights Reserved.
*/

    using NatSuite.Sharing;
namespace NatSuite.Examples {

    using UnityEngine;
    using Recorders;
    using Recorders.Inputs;

    public class GifRecording : MonoBehaviour {
        
        [Header("GIF Settings")]
        public int imageWidth = 640;
        public int imageHeight = 480;
        public float frameDuration = 0.1f; // seconds

        private GIFRecorder recorder;
        private CameraInput cameraInput;
        private string path;

        private bool isRecordingStarted;

        public void StartRecording ()
        {

            isRecordingStarted = true;
            // Start recording
            recorder = new GIFRecorder(imageWidth, imageHeight, frameDuration);
            cameraInput = new CameraInput(recorder, Camera.main);
            // Get a real GIF look by skipping frames
            cameraInput.frameSkip = 4;
        }

        public async void StopRecording () {

            if (isRecordingStarted)
            {
                // Stop the recording
                cameraInput.Dispose();
                path = await recorder.FinishWriting();
                // Log path
                // Debug.Log($"Saved animated GIF image to: {path}");
                // Application.OpenURL($"file://{path}");
                isRecordingStarted = false;
            }
           
           
        }

        public void Share()
        {
#if UNITY_ANDROID
            var payload = new SharePayload();
            payload.AddMedia(path);
            payload.AddText(" Check out the Official Racing Game of the super awesome Indie rock band 'The Wombats'" + 
                            "\n" + 
                            " #thewombatsgame"+
                            "\n" +
                                " https://play.google.com/store/apps/details?id=com.begames.thewombats ");
            payload.Commit();
#endif
        
#if UNITY_IOS
           var payload = new SharePayload();
            payload.AddMedia(path);
            payload.AddText(" Check out the Official Racing Game of the super awesome Indie rock band 'The Wombats'" + 
                            "\n" + 
                            " #thewombatsgame"+
                            "\n" +
                                " https://apps.apple.com/us/app/the-wombats-official-game/id1616417200 ");
            payload.Commit();
#endif
            
        }
    }
}