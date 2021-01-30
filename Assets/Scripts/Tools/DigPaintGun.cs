using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DigPaintGun : MonoBehaviour, IDig
{
    public InputActionReference useAction;
    XRGrabInteractable grabInteractable;

    public int resolution = 512;
    Texture2D baseMap;
    public float brushSize;
    public Texture2D brushTexture;
    Vector2 stored;
    public static Dictionary<Collider, RenderTexture> paintTextures = new Dictionary<Collider, RenderTexture>();

    [SerializeField]
    LayerMask sand;

    [SerializeField]
    bool painting;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        useAction.action.started += Dig; //equivalent to GetKeyDown()
        useAction.action.canceled += StopDigging; //Equivalent to GetKeyUp()

        CreateClearTexture();// clear white texture to draw on
    }

    void Update()
    {
        if(painting)
            Paint();
    }

    void Paint()
    {
        Debug.DrawRay(transform.position, transform.forward * 20f, Color.magenta);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, 5, sand))
        {
            Collider coll = hit.collider;
            if (coll != null)
            {
                if (!paintTextures.ContainsKey(coll)) // if there is already paint on the material, add to that
                {
                    Renderer rend = hit.transform.GetComponent<Renderer>();
                    paintTextures.Add(coll, getWhiteRT());
                    rend.material.SetTexture("Texture2D_b79060968f9d45b6b61c4124d9ddbcdd", paintTextures[coll]);
                }
                if (stored != hit.lightmapCoord) // stop drawing on the same point
                {
                    stored = hit.lightmapCoord;
                    Vector2 pixelUV = hit.lightmapCoord;
                    pixelUV.y *= resolution;
                    pixelUV.x *= resolution;
                    DrawTexture(paintTextures[coll], pixelUV.x, pixelUV.y);
                }
            }
        }
    }

    void DrawTexture(RenderTexture rt, float posX, float posY)
    {

        RenderTexture.active = rt; // activate rendertexture for drawtexture;
        GL.PushMatrix();                       // save matrixes
        GL.LoadPixelMatrix(0, resolution, resolution, 0);      // setup matrix for correct size
        // draw brushtexture
        Rect rect = new Rect(posX - brushTexture.width / brushSize, (rt.height - posY) - brushTexture.height / brushSize, brushTexture.width / (brushSize * 0.5f), brushTexture.height / (brushSize * 0.5f));
        Graphics.DrawTexture(rect, brushTexture);
        GL.PopMatrix();
        RenderTexture.active = null;// turn off rendertexture


    }

    RenderTexture getWhiteRT()
    {
        RenderTexture rt = new RenderTexture(resolution, resolution, 32);
        Graphics.Blit(baseMap, rt);
        return rt;
    }

    void CreateClearTexture()
    {
        baseMap = new Texture2D(1, 1);
        baseMap.SetPixel(0, 0, Color.black);
        baseMap.Apply();
    }

    public void Dig()
    {
        Paint();
    }

    public void DigStart()
    {
        painting = true;
    }

    public void DigEnd()
    {
        painting = false;
    }

    public void Dig(InputAction.CallbackContext context)
    {
        if (grabInteractable.isSelected)
        {
            DigStart();
        }
    }

    public void StopDigging(InputAction.CallbackContext context)
    {
        DigEnd();
    }
}