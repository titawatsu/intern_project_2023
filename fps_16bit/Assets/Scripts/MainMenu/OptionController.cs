using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using fps_16bit;

namespace fps_16bit
{
    public class OptionController : MonoBehaviour
    {
        public Toggle fullscreenToggle;
        [FormerlySerializedAs("v_syncToggle")] public Toggle vSyncToggle;

        public List<ResolutionList> resolutions = new List<ResolutionList>();

        private int selectedResolution;
        
        public TMP_Text resolutionLabel;

        [SerializeField] private Slider SenSlider;

        public const string mouseSens = "mouseSens";
        
        public PlayerController playerController;
        private void Start()
        {
            fullscreenToggle.isOn = Screen.fullScreen;
            if (QualitySettings.vSyncCount == 0) vSyncToggle.isOn = false;
            else vSyncToggle.isOn = true;

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
            SenSlider.value = PlayerPrefs.GetFloat(mouseSens, 10f);
        }
        
        private void Awake()
        {
            SenSlider.onValueChanged.AddListener(SetMouseSens);
        }
        
        private void SetMouseSens(float value)
        {
            PlayerPrefs.SetFloat(mouseSens, value);
        }
        
        private void OnDisable()
        {
            PlayerPrefs.SetFloat(mouseSens, SenSlider.value);
            
        }

        private void UpdateResolutionLabel()
        {
            resolutionLabel.text = resolutions[selectedResolution].horizontalSize.ToString() + "x" +
                                   resolutions[selectedResolution].verticalSize.ToString();
        }

        private void ApplyGraphics()
        {
            Screen.fullScreen = fullscreenToggle.isOn;
            if (vSyncToggle.isOn) QualitySettings.vSyncCount = 1;
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
}