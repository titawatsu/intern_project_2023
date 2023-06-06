using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace fps_16bit
{
    public class TutorialCheck : MonoBehaviour
    {
        [SerializeField] private TMP_Text objectiveTeller;
        [SerializeField] private string objectString;

        private void Start()
        {
            objectiveTeller.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            objectiveTeller.enabled = true;
            objectiveTeller.text = $"{objectString}";
        }

        private void OnTriggerExit(Collider other)
        {
            objectiveTeller.enabled = false;
        }
    }

}