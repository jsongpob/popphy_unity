using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    CameraScript CameraScript;

    public static string FilePathFromPlayScene = "";

    public static bool dontStart = true;

    float Delaytime;

    void Awake()
    {
        dontStart = true;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("DontDestory");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        CameraScript = GameObject.FindGameObjectWithTag("CameraController").GetComponent<CameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.V))
        {
            SceneManager.LoadScene("AmericanDinnerFullView");
            StartCoroutine(ReturningOnCamera());
        }
    }

    IEnumerator ReturningOnCamera()
    {
        yield return new WaitForSeconds(4);
        CameraScript.StartCamera();
    }
}
