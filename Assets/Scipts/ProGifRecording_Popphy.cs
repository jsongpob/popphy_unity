using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProGifRecording_Popphy : MonoBehaviour
{
	public float gameTimingToStopRecord;

	public Camera mCamera = null;
	string optionalGifFileName;

    public static bool ValueToStart = false;

    [Space()]
    /// <summary>
    /// The recorder will save gif using this filename if this is provided. The new gif will replace the old one if their filename are the same.
    /// </summary>
    [Tooltip("The recorder will save gif using this filename if this is provided. The new gif will replace the old one if their filename are the same.")]
    //public string optionalGifFileName = fileName;

    [Header("Native Save (+MobileMediaPlugin)")]
    public bool saveToNative = false;
    public bool deleteOriginGif = false;
    public string folderName = "GIF Demo";

    CameraScript CameraScript;
    AnimationController AnimationController;

    // Use this for initialization
    void Start()
	{
        CameraScript = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraScript>();

        //Create an instance for ProGifManager
        ProGifManager gifMgr = ProGifManager.Instance;

        //Make some changes to the record settings, you can let it auto aspect with screen size.. 
        gifMgr.SetRecordSettings(true, 360, 360, 4, 4, 0, 25); //OPTIMIZE VER
        //gifMgr.SetRecordSettings(true, 720, 1280, 5, 50, 0, 80);

        //Or give an aspect ratio for cropping gif frames just before encoding
        //gifMgr.SetRecordSettings(new Vector2(1, 1), 300, 300, 1, 15, 0, 30);

    }

    void Update()
    {
        //Create an instance for ProGifManager
/*        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(WaitForSaving());
        }*/

        if (ValueToStart == true)
        {
            RunCapture();
            AnimationController.CapturingState = "1";
        }
    }

    public void RunCapture()
    {
        ValueToStart = false;
        StartCoroutine(WaitForSaving());
    }

    IEnumerator WaitForSaving()
    {
        AnimationController.CapturingState = "2";

        yield return new WaitForSeconds(5);

        AnimationController.CapturingState = "3";

        yield return new WaitForSeconds(2);

        ProGifManager gifMgr = ProGifManager.Instance;
        //Start gif recording
        gifMgr.StartRecord((mCamera != null) ? mCamera : Camera.main, (progress) =>
        {
            Debug.Log("[SimpleStartDemo] On record progress: " + progress);
        },
        () => {
            Debug.Log("[SimpleStartDemo] On recorder buffer max.");
        });

        yield return new WaitForSeconds(1);
        AnimationController.CapturingState = "4";
        yield return new WaitForSeconds(1);

        Capture();
    }

    private void Capture()
    {
        ProGifManager gifMgr = ProGifManager.Instance;
        gifMgr.m_GifRecorder.recorderCom.SaveFolder = "C:\\Users\\rctvj\\Desktop\\Popphy_Image";
        optionalGifFileName = System.DateTime.Now.ToString("dd_MM_yyyy_HHmmss") + "_PopphyTest";
        //Stop the recording

        gifMgr.StopAndSaveRecord(
            () =>
            {
                Debug.Log("[SimpleStartDemo] On pre-processing done.");
            },
            (id, progress) =>
            {
                //Debug.Log("[SimpleStartDemo] On save progress: " + progress);
                if (progress < 1f)
                {

                }
                else
                {

                }
            },
            (id, path) =>
            {
                //Clear the existing recorder, player and reset gif manager
                gifMgr.Clear();
                Debug.Log("[SimpleStartDemo] On saved, origin save path: " + path);
                ProGifTexture2DPlayer_Popphy.LinkImage = path;
                AnimationController.CapturingState = "0";
                CameraScript.StopCamera();

                SceneManager.LoadScene("PreviewCapture");

                if (saveToNative)
                {
#if SDEV_MobileMedia
						// Copy the newly created GIF file from internal path to external, i.e. Android/iOS device gallery
                        string nativeSavePath = MobileMedia.CopyMedia(path, folderName, System.IO.Path.GetFileNameWithoutExtension(path), ".gif", true);
                        /*MobileMedia.SaveBytes(System.IO.File.ReadAllBytes(path), "YourGifFolderName", "YourGifFileName", ".gif", true);*/


                        if (deleteOriginGif) System.IO.File.Delete(path);

                        Debug.Log("Native Save Path(Andorid Only): " + nativeSavePath);
#if UNITY_EDITOR
                        Application.OpenURL(System.IO.Path.GetDirectoryName(nativeSavePath));
#endif

#endif
                }
                else
                {
#if UNITY_EDITOR
                    gifMgr.Clear();
                    //Application.OpenURL(System.IO.Path.GetDirectoryName(path));
#endif
                }
            },
            optionalGifFileName
            );
    }
}