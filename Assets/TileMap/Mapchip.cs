using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapchip : MonoBehaviour
{
	[SerializeField]
	private Image _image;

	/// <summary>
	/// Initialize 
	/// </summary>
	public void Initialize (Sprite sprite)
	{
		_image.sprite = sprite;
	}
}
