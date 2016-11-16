using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ScreenEffect : MonoBehaviour {

	[SerializeField] private Shader currentShader;
	[SerializeField] [Range(0,1)]private float greyScaleAmount = 0.0f;
	Material currentMaterial;


	bool isLeaving = false;

	//Checkes if we can use the image effect
	void OnEnable()
	{
		Distance.OnLeavingCombatArea += StartEffect;
		Distance.OnEnteringCombatArea += StartEffect;

		if(!SystemInfo.supportsImageEffects)
		{
			enabled = false;
			return;
		}
		if(!currentShader && !currentShader.isSupported)
		{
			enabled = false;
		}
	}

	//Create a material if the script doesn't find one
	Material material
	{
		get
		{
			if(currentMaterial == null)
			{
				currentMaterial = new Material(currentShader);
				currentMaterial.hideFlags = HideFlags.HideAndDontSave;
			}
			return currentMaterial;
		}
	}

	void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if(currentShader != null)
		{
			material.SetFloat("_Amount", greyScaleAmount);
			Graphics.Blit(src, dst, material);
		}
		else
		{
			Graphics.Blit(src, dst);
		}
	}

	public void StartEffect(bool _isLeaving)
	{
		StartCoroutine(ChangeGreyScale());
		isLeaving = _isLeaving;
	}

	IEnumerator ChangeGreyScale()
	{
		while(true)
		{
			greyScaleAmount = Mathf.Clamp(greyScaleAmount, 0f,1f);

			if(isLeaving)
			{	
				if(greyScaleAmount < 1)
					greyScaleAmount += 0.001f * Time.deltaTime;
			}
			if(!isLeaving)
			{
				if(greyScaleAmount != 0)
					greyScaleAmount -= 0.001f * Time.deltaTime;
			}
			yield return new WaitForFixedUpdate();
		}
	}

	void OnDisable()
	{
		Distance.OnLeavingCombatArea -= StartEffect;
		Distance.OnEnteringCombatArea -= StartEffect;
		StopAllCoroutines();

		if(currentMaterial)
		{
			DestroyImmediate(currentMaterial);
		}
	}

}
