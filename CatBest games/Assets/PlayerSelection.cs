using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelection : MonoBehaviour
{
	public Player currentState;
	public Image CatCharacter;
	public Image ColorPanel;

	[Space(20)]
	[Header("DONT'T TOUCH THESE:")]
	// Internal, but Unity doesn't like internal
	public PlayerSettings settings;
	public int characterID;
	public int colorID;

	public void UpdateAppearance()
	{
		currentState.image = settings.cats[characterID];
		currentState.color = settings.colors[colorID];
		CatCharacter.sprite = currentState.image;
		ColorPanel.color = currentState.color;
		settings.validateSelection();
	}

	public void NextCharacter(bool reverse) {
		characterID += reverse ? -1 : 1;
		if (characterID >= settings.cats.Count) characterID = 0;
		else if (characterID <= -1) characterID = settings.cats.Count - 1;
		UpdateAppearance();
	}
	public void NextColor(bool reverse)
	{
		colorID += reverse ? -1 : 1;
		if (colorID >= settings.cats.Count) colorID = 0;
		else if (colorID <= -1) colorID = settings.cats.Count - 1;
		UpdateAppearance();
	}
}
