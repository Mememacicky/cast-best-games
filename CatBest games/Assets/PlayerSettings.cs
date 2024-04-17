using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSettings : MonoBehaviour
{
	public GameObject SelectorPanel;
	public KitKatToe GamePanel;
	public GameObject SettingsPanel;

	[Space(15)]
	public Player nullPlayer = new Player() { color = new Color(0.6395372f, 0.8335454f, 0.8943396f) };
	//public List<Player> players = new List<Player>();
	public List<Sprite> cats = new List<Sprite>();
	public List<Color> colors = new List<Color>();

	public PlayerSelection playerXsel;
	public PlayerSelection playerOsel;

	public Button PlayBtn;
	public bool gameValid;

	public TextMeshProUGUI pointSliderValue;

	public static GameSettings settings = new GameSettings() { pointGame = false, winPoints = 3 };

	[Space(10)]
	public UISwitcher.UISwitcher playerSwitch;
	public TextMeshProUGUI playerText;

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
		MainMenu();
		StartingPlayer(null);
	}

	public void MainMenu()
	{
		GamePanel.EndgamePanel.SetActive(false);
		SelectorPanel.SetActive(true);
		SettingsPanel.SetActive(false);
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
		playerSwitch.onColor = playerOsel.currentState.color;
		playerSwitch.offColor = playerXsel.currentState.color;
	}

	public void StartGame()
	{
		if (gameValid)
		{
			SelectorPanel.SetActive(false);
			KitKatToe.players[playerID.X] = playerXsel.currentState;
			KitKatToe.players[playerID.O] = playerOsel.currentState;
			GamePanel.ResetGame(settings.startingPlayer);
		}
	}

	public void QuitGame()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}

	public void OpenSettings(bool close)
	{
		SettingsPanel.SetActive(!close);
	}
	public void PointSettingsUpdate(float value)
	{
		settings.winPoints = (int)Mathf.Round(value);
		pointSliderValue.text = value.ToString();
	}
	public void PointSettingsUpdate(bool enabled)
	{
		settings.pointGame = enabled;
	}

	public void StartingPlayer(bool? player)
	{
		if (player == null)
		{
			settings.startingPlayer = playerID.none;
			playerText.text = "Random";
		}
		if (player == true)
		{
			settings.startingPlayer = playerID.O;
			playerText.text = "Player 2";
		}
		if (player == false)
		{
			settings.startingPlayer = playerID.X;
			playerText.text = "Player 1";
		}
	}
}

public struct GameSettings
{
	public bool pointGame;
	public int winPoints;
	public playerID startingPlayer;
}