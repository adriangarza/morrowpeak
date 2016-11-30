﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class SpecialFader : MonoBehaviour
{
	public Image FadeImg;
	public float fadeInSpeed = .3f;
	public float fadeOutSpeed = 2.3f;
	public bool sceneStarting = true;

	void Awake()
	{
		FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
	}

	void Update()
	{
		// If the scene is starting...
		if (sceneStarting)
		{
			// ... call the StartScene function.
			StartScene();
		}
	}


	void FadeToClear()
	{
		// Lerp the colour of the image between itself and transparent.
		FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, fadeInSpeed * Time.smoothDeltaTime);
	}


	void FadeToBlack()
	{
		// Lerp the colour of the image between itself and black.
		FadeImg.color = Color.Lerp(FadeImg.color, Color.black, fadeOutSpeed * Time.smoothDeltaTime);
	}

	public void StartScene()
	{
		StartCoroutine("StartSceneRoutine");
	}

	public IEnumerator StartSceneRoutine()
	{
		do
		{
			// Start fading towards clear.
			FadeToClear();
			// If the screen is almost clear...
			if (FadeImg.color.a <= 0.01f)
			{
				// ... set the colour to clear and disable the RawImage.
				FadeImg.color = Color.clear;
				FadeImg.enabled = false;
				// The scene is no longer starting.
				sceneStarting = false;
				yield break;
			}
			else
			{
				yield return null;
			}
		}while (true);


	}

	public void EndScene(string sceneName)
	{
		sceneStarting = false;
		StartCoroutine("EndSceneRoutine", sceneName);
	}

	public IEnumerator EndSceneRoutine(string sceneName)
	{
		// Make sure the RawImage is enabled.
		FadeImg.enabled = true;
		do
		{
			// Start fading towards black.
			FadeToBlack();
			// If the screen is almost black...
			if (FadeImg.color.a >= 0.99f)
			{
				// ... reload the level
				SceneManager.LoadScene(sceneName);
				yield break;
			}
			else
			{
				yield return null;
			}
		}while (true);
	}
} 