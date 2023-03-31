using System;
using System.Collections;
using System.IO;
using UnityEngine;

public enum CaptureBgColors
{
    Gray,
    Green,
    Blue,
    Purple,
    Orange
}

public enum CaptureSize
{
    POT64,
    POT128,
    POT256,
    POT512
}

public class ObjectCapture : MonoBehaviour
{

    [Header("[Common]"), SerializeField]
    private string prefixString;
    [SerializeField]
    private CaptureBgColors bgColors = CaptureBgColors.Gray;
    [SerializeField]
    private CaptureSize captureSize = CaptureSize.POT128;
    [SerializeField]
    private RenderTexture renderTexture;
    
    [Header("[OneShot]"), SerializeField]
    private string objectName;
    
    [Header("[AllShot]"), SerializeField]
    private GameObject[] captureObjects;

    private Camera _captureCamera;
    private uint _currentCount;
    
    /// <summary>
    /// Init
    /// </summary>
    private void Start()
    {
        _captureCamera = Camera.main;
        SetColors();
        SetSize(null);
    }

    /// <summary>
    /// 캡쳐 버튼을 클릭 했을때
    /// </summary>
    public void OnClickCaptureButton()
    {
        InitRenderTexture();
        SetColors();
        StartCoroutine(CaptureImage());
    }

    /// <summary>
    /// All캡쳐 버튼을 클릭 했을때
    /// </summary>
    public void OnClickAllCaptureButton()
    {
        InitRenderTexture();
        SetColors();
        StartCoroutine(AllCaptureImage());
    }

    /// <summary>
    /// 캡쳐
    /// </summary>
    private IEnumerator CaptureImage()
    {
        yield return null;
        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, true);
        RenderTexture.active = renderTexture;
        tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        
        //색상 보정 (캡쳐한 이미지가 어두워 지지 않게)
        Color[] pixels = tex.GetPixels();
        for (int p = 0; p < pixels.Length; p++)
            pixels[p] = pixels[p].gamma;

        tex.SetPixels(pixels);
        tex.Apply();

        yield return null;

        var data = tex.EncodeToPNG();
        string name = $"{prefixString}_{objectName}";
        string extention = ".png";
        string path = Application.persistentDataPath + "/Capture/";
        
        Debug.LogFormat($"Save Path: {path}");

        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        
        File.WriteAllBytes(path + name + extention, data);
        
        yield return null;
    }

    /// <summary>
    /// All 캡쳐
    /// </summary>
    private IEnumerator AllCaptureImage()
    {
        while (_currentCount < captureObjects.Length)
        {
            var currentObj = Instantiate(captureObjects[_currentCount].gameObject);
            
            yield return null;
            
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false, true);
            RenderTexture.active = renderTexture;
            tex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
        
            //색상 보정 (캡쳐한 이미지가 어두워 지지 않게)
            Color[] pixels = tex.GetPixels();
            for (int p = 0; p < pixels.Length; p++)
                pixels[p] = pixels[p].gamma;

            tex.SetPixels(pixels);
            tex.Apply();
            
            yield return null;
            
            // All캡쳐에서는 해당 오브젝트의 이름에 따라 생성한다.
            var data = tex.EncodeToPNG();
            string name = $"{prefixString}_{captureObjects[_currentCount].gameObject.name}";
            string extention = ".png";
            string path = Application.persistentDataPath + "/Capture/";
        
            Debug.LogFormat($"Save Path: {path}");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        
            File.WriteAllBytes(path + name + extention, data);
            yield return null;
            
            DestroyImmediate(currentObj);
            
            _currentCount++;
            yield return null;
        }
    }

    /// <summary>
    /// 저장 이미지 배경 색 변경
    /// </summary>
    private void SetColors()
    {
        switch (bgColors)
        {
            case CaptureBgColors.Gray:
                _captureCamera.backgroundColor = new Color32(180, 180, 180, 255);
                break;
            case CaptureBgColors.Green:
                _captureCamera.backgroundColor = new Color32(91, 188, 97, 255);
                break;
            case CaptureBgColors.Blue:
                _captureCamera.backgroundColor = new Color32(55, 107, 195, 255);
                break;
            case CaptureBgColors.Purple:
                _captureCamera.backgroundColor = new Color32(145, 62, 174, 255);
                break;
            case CaptureBgColors.Orange:
                _captureCamera.backgroundColor = new Color32(217, 111, 6, 255);
                break;
        }
    }

    /// <summary>
    /// 저장 이미지 사이즈 변경
    /// </summary>
    private void SetSize(Action call)
    {
        switch (captureSize)
        {
            case CaptureSize.POT64:
                renderTexture.width = 64;
                renderTexture.height = 64;
                break;
            case CaptureSize.POT128:
                renderTexture.width = 128;
                renderTexture.height = 128;
                break;
            case CaptureSize.POT256:
                renderTexture.width = 256;
                renderTexture.height = 256;
                break;
            case CaptureSize.POT512:
                renderTexture.width = 512;
                renderTexture.height = 512;
                break;
        }
        
        call?.Invoke();
    }

    /// <summary>
    /// RenderTexture 초기화
    /// </summary>
    private void InitRenderTexture()
    {
        renderTexture.Release();
        SetSize(() =>
        {
            renderTexture.Create();
        });
    }
}
