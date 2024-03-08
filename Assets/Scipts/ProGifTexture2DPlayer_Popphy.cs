
using UnityEngine;
using UnityEngine.UI;

public class ProGifTexture2DPlayer_Popphy : MonoBehaviour
{
    public ProGifPlayerTexture2D m_ProGifPlayerTexture2D;

    public RawImage m_RawImage;

    public static string LinkImage;

    void Start ()
    {
        SocketManagerScript.FilePathPhoto = LinkImage;
        // Use gif Player Component directly: -----------------------------------------------------
        m_ProGifPlayerTexture2D.Play(LinkImage, false);
        m_ProGifPlayerTexture2D.OnTexture2DCallback = (texture2D) =>
        {
            // get and display the decoded texture here:
            m_RawImage.texture = texture2D;

            // set the texture to other texture fields of the shader
            //m_Renderer.material.SetTexture("_MetallicGlossMap", texture2D);
        };
    }
}
