using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionController : MonoBehaviour
{
    public Toggle fullscreenToggle, v_syncToggle;

    public List<ResolutionList> resolutions = new List<ResolutionList>();

    private int selectedResolution;
    
    public TMP_Text resolutionLabel;
    private void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
        if (QualitySettings.vSyncCount == 0) v_syncToggle.isOn = false;
        else v_syncToggle.isOn = true;

        bool foundRes = false;

        for (int i = 0; i < resolutions.Count; i++)
        {
            if (Screen.width == resolutions[i].horizontalSize && Screen.height == resolutions[i].verticalSize)
            {
                foundRes = true;
                selectedResolution = i;
                UpdateResolutionLabel();
            }
        }

        if (!foundRes)
        {
            ResolutionList newResolutionList = new ResolutionList();

            newResolutionList.horizontalSize = Screen.width;
            newResolutionList.verticalSize = Screen.height;
            
            resolutions.Add(newResolutionList);
            selectedResolution = resolutions.Count - 1;
            
            UpdateResolutionLabel();
        }
    }

    private void UpdateResolutionLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontalSize.ToString() + "x" +
                               resolutions[selectedResolution].verticalSize.ToString();
    }

    private void ApplyGraphics()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        if (v_syncToggle.isOn) QualitySettings.vSyncCount = 1;
        else QualitySettings.vSyncCount = 0;
        
        Screen.SetResolution(resolutions[selectedResolution].horizontalSize, resolutions[selectedResolution].verticalSize, fullscreenToggle.isOn);
        
    }

    public void ResolutionLeft()
    {
        selectedResolution--;
        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResolutionLabel();
    }
    
    public void ResolutionRight()
    {
        selectedResolution++;
        if (selectedResolution > resolutions.Count - 1)
        {
            selectedResolution = resolutions.Count - 1;
        }

        UpdateResolutionLabel();
    }
}
[System.Serializable]
public class ResolutionList
{
    public int horizontalSize;
    public int verticalSize;
}