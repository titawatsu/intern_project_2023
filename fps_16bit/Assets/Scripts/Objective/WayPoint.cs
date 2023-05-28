using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WayPoint : MonoBehaviour
{
    public Image[] images;
    public Transform[] targets;
    public Transform player;
    private Vector3[] targetLocations;
    public float[] distances;
    public float fadeRate = 1.0f;
    public float fadeThreshold = 30f;
    private float targetAlpha = 0f;
    private Coroutine[] fadeCoroutines;

    void Start()
    {
        InitializeImages();
        SetImageAlpha(0f, images.Length);
    }

    private void Update()
    {
        CalculateDistances();

        for (int i = 0; i < images.Length; i++)
        {
            if (distances[i] <= fadeThreshold && fadeCoroutines[i] == null)
            {
                fadeCoroutines[i] = StartCoroutine(FadeIn(i));
            }
            else if (distances[i] > fadeThreshold && fadeCoroutines[i] != null)
            {
                StopCoroutine(fadeCoroutines[i]);
                fadeCoroutines[i] = null;
                SetImageAlpha(0f, i);
            }

            targetLocations[i] = Camera.main.WorldToScreenPoint(targets[i].position);
            images[i].transform.position = targetLocations[i];
        }
    }

    private void InitializeImages()
    {
        targetLocations = new Vector3[images.Length];
        distances = new float[images.Length];
        fadeCoroutines = new Coroutine[images.Length];

        for (int i = 0; i < images.Length; i++)
        {
            fadeCoroutines[i] = null;
            SetImageAlpha(0f, i);
        }
    }

    private void SetImageAlpha(float alpha, int index)
    {
        if (index < 0 || index >= images.Length)
            return;

        Color currentColor = images[index].color;
        currentColor.a = alpha;
        images[index].color = currentColor;
    }

    private void CalculateDistances()
    {
        for (int i = 0; i < targets.Length; i++)
        {
            distances[i] = Vector3.Distance(targets[i].position, player.position);
        }
    }

    IEnumerator FadeIn(int index)
    {
        targetAlpha = 1.0f;
        Color curColor = images[index].color;
        while (Mathf.Abs(curColor.a - targetAlpha) > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, fadeRate * Time.deltaTime);
            images[index].color = curColor;
            yield return null;
        }
    }
}
