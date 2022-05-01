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

        public void StartRecording () {
            // Start recording
            recorder = new GIFRecorder(imageWidth, imageHeight, frameDuration);
            cameraInput = new CameraInput(recorder, Camera.main);
            // Get a real GIF look by skipping frames
            cameraInput.frameSkip = 4;
        }

        public async void StopRecording () {
            // Stop the recording
            cameraInput.Dispose();
            var path = await recorder.FinishWriting();
            // Log path
            // Debug.Log($"Saved animated GIF image to: {path}");
            // Application.OpenURL($"file://{path}");
            var payload = new SharePayload();
            payload.AddMedia(path);
            payload.AddText(" Checkout the album " + "\n" + " https://open.spotify.com/album/3J9a9IUBPJL3WhkC86mCw1 ");
            payload.Commit();
        }
    }
}