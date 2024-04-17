using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
	public GameObject SelectorPanel;
	public KitKatToe GamePanel;

	[Space(15)]
	public Player nullPlayer = new Player() { color = new Color(0.6395372f, 0.8335454f, 0.8943396f) };
	//public List<Player> players = new List<Player>();
	public List<Sprite> cats = new List<Sprite>();
	public List<Color> colors = new List<Color>();

	public PlayerSelection playerXsel;
	public PlayerSelection playerOsel;

	public Button PlayBtn;
	public bool gameValid;

	// Start is called before the first frame update
	void Start()
	{
		playerXsel.settings = this;
		playerXsel.characterID = 0;
		playerXsel.colorID = 0;
		playerXsel.UpdateAppearance();
		playerOsel.settings = this;
		playerOsel.characterID = 1;
		playerOsel.colorID = 1;
		playerOsel.UpdateAppearance();
	}

	public void validateSelection()
	{
		if (playerXsel.colorID == playerOsel.colorID && playerXsel.characterID == playerOsel.characterID)
		{
			PlayBtn.interactable = false;
			gameValid = false;
		}
		else
		{
			PlayBtn.interactable = true;
			gameValid = true;
		}
	}

	public void StartGame()
	{
		if (gameValid)
		{
			SelectorPanel.SetActive(false);
			KitKatToe.players[playerID.X] = playerXsel.currentState;
			KitKatToe.players[playerID.O] = playerOsel.currentState;
			GamePanel.ResetGame();
		}
	}

	public void BackToSelection()
	{
		SelectorPanel.SetActive(true);
	}
}
