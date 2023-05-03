using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHighlight : MonoBehaviour
{
    [SerializeField] private List<Renderer> renderers;
    [SerializeField] private Color color = Color.white;
    private List<Material> mat;

    private void Awake()
    {
        mat = new List<Material>();
        foreach (var renderer in renderers)
        {
            mat.AddRange(new List<Material>(renderer.materials));
        }
    }

    public void ToggleHighlight(bool val)
    {
        if (val)
        {
            foreach (var material in mat)
            {
                material.EnableKeyword("_EMISSION"); // enable the emissions
                material.SetColor("_EmissionColor", color);
            }
        }
        else
        {
            foreach (var material in mat)
            {
                material.DisableKeyword("_EMISSION");
            }
        }
    }
}
