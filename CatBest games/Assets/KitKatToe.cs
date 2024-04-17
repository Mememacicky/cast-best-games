using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitKatToe : MonoBehaviour
{
	public FieldCat TL;
	public FieldCat TC;
	public FieldCat TR;
	public FieldCat ML;
	public FieldCat MC;
	public FieldCat MR;
	public FieldCat BL;
	public FieldCat BC;
	public FieldCat BR;

	public playerID nextPlayer = playerID.none;
	public Image nextPlayerPanel;
	public Image nextPlayerIcon;

	public Sprite playerXimg;
	public Color colorX = Color.red;
	public Sprite playerOimg;
	public Color colorO = Color.blue;

	public Sprite playerNoneImg;
	public Color colorNone = new Color(0.6395372f, 0.8335454f, 0.8943396f);

	public static Dictionary<playerID, Player> players;
	public static Dictionary<fieldPos, FieldCat> fields = new Dictionary<fieldPos, FieldCat>();

	public bool pointGame;

	[Space(10)]
	public GameObject EndgamePanel;
	public TextMeshProUGUI endTitle;
	public Animator endCatDancer;
	public Image winnerCat;

	[Header("Points Game")]
	public GameObject pointsGamePanel;
	public int pointsX = 0;
	public int pointsO = 0;
	public GameObject interRoundPanel;
	public TextMeshProUGUI interRoundTitle;
	public Animator interRoundCat;
	public Image roundWinner;
	public TextMeshProUGUI roundID;
	public int round;
	playerID startingPlayer;

	[Header("Player x")]
	public Image pointPanelX;
	public TextMeshProUGUI poinTextX;
	public Image playerIconX;
	[Header("Player o")]
	public Image pointPanelO;
	public TextMeshProUGUI poinTextO;
	public Image playerIconO;

	// Start is called before the first frame update
	void Start()
	{
		if (fields == null)
			fields = new Dictionary<fieldPos, FieldCat>();
		if (players == null)
			players = new Dictionary<playerID, Player>();

		fields.Clear();
		fields.Add(fieldPos.TopLeft, TL);
		fields.Add(fieldPos.TopCenter, TC);
		fields.Add(fieldPos.TopRight, TR);
		fields.Add(fieldPos.MiddleLeft, ML);
		fields.Add(fieldPos.MiddleCenter, MC);
		fields.Add(fieldPos.MiddleRight, MR);
		fields.Add(fieldPos.BottomLeft, BL);
		fields.Add(fieldPos.BottomCenter, BC);
		fields.Add(fieldPos.BottomRight, BR);

		players.Add(playerID.X, new Player() { color = colorX, image = playerXimg });
		players.Add(playerID.O, new Player() { color = colorO, image = playerOimg });
		players.Add(playerID.none, new Player() { color = colorNone, image = playerNoneImg });

		ResetGame(playerID.X);
	}

	public void fieldClick(int position)
	{
		bool success = fields[(fieldPos)position].GetComponent<FieldCat>().PlayerChange(nextPlayer);

		if (success)
		{
			switch (nextPlayer)
			{
				case playerID.X:
					nextPlayer = playerID.O;
					break;
				case playerID.O:
					nextPlayer = playerID.X;
					break;
				default:
					Debug.LogError("Invalid next player");
					nextPlayer = playerID.X;
					break;
			}
			UpdateNextPlayer();
			playerID? winner = checkEndgame();
			if (winner != null && !PlayerSettings.settings.pointGame)
				showEndgame((playerID)winner);
			else if (winner != null && PlayerSettings.settings.pointGame)
				RoundWinner((playerID)winner);
		}
	}

	public playerID? checkEndgame()
	{
		playerID[,] currentStatus = new playerID[3, 3];
		currentStatus[0, 0] = fields[fieldPos.TopLeft].thisPlayer;
		currentStatus[0, 1] = fields[fieldPos.TopCenter].thisPlayer;
		currentStatus[0, 2] = fields[fieldPos.TopRight].thisPlayer;
		currentStatus[1, 0] = fields[fieldPos.MiddleLeft].thisPlayer;
		currentStatus[1, 1] = fields[fieldPos.MiddleCenter].thisPlayer;
		currentStatus[1, 2] = fields[fieldPos.MiddleRight].thisPlayer;
		currentStatus[2, 0] = fields[fieldPos.BottomLeft].thisPlayer;
		currentStatus[2, 1] = fields[fieldPos.BottomCenter].thisPlayer;
		currentStatus[2, 2] = fields[fieldPos.BottomRight].thisPlayer;

		// Check rows
		for (int i = 0; i < 3; i++)
		{
			if (currentStatus[i, 0] != playerID.none && currentStatus[i, 0] == currentStatus[i, 1] && currentStatus[i, 1] == currentStatus[i, 2])
				return currentStatus[i, 0];
		}
		// Check columns
		for (int i = 0; i < 3; i++)
		{
			if (currentStatus[0, i] != playerID.none && currentStatus[0, i] == currentStatus[1, i] && currentStatus[1, i] == currentStatus[2, i])
				return currentStatus[0, i];
		}
		// Check diagonals
		if (currentStatus[1, 1] != playerID.none && currentStatus[0, 0] == currentStatus[1, 1] && currentStatus[1, 1] == currentStatus[2, 2])
			return currentStatus[1, 1];
		if (currentStatus[1, 1] != playerID.none && currentStatus[0, 2] == currentStatus[1, 1] && currentStatus[1, 1] == currentStatus[2, 0])
			return currentStatus[1, 1];
		// Check draw
		int filledFields = 0;
		foreach (playerID fld in currentStatus)
		{
			if (fld != playerID.none)
				filledFields++;
		}
		if (filledFields == 9)
			return playerID.none;
		return null;
	}

	public void showEndgame(playerID winner)
	{
		EndgamePanel.SetActive(true);
		nextPlayerPanel.gameObject.SetActive(false);
		if (winner == playerID.none)
		{ // Draw
			endTitle.text = "It's a DRAW!";
			endCatDancer.SetBool("isDraw", true);
			winnerCat.gameObject.SetActive(false);
		}
		else
		{
			endTitle.text = "Winner:\u00A0\u00A0\u00A0\u00A0<color=#00000000>.</color>";
			endCatDancer.SetBool("isDraw", false);
			winnerCat.gameObject.SetActive(true);
			winnerCat.sprite = players[winner].image;
		}
		EndgamePanel.transform.GetChild(0).GetComponent<Image>().color = players[winner].color;
	}

	public void ResetGame(playerID startingPlayer)
	{
		if (startingPlayer == playerID.none) startingPlayer = playerID.X;
		nextPlayer = startingPlayer;
		this.startingPlayer = startingPlayer;
		EndgamePanel.SetActive(false);
		interRoundPanel.SetActive(false);
		nextPlayerPanel.gameObject.SetActive(true);
		winnerCat.gameObject.SetActive(false);
		roundWinner.gameObject.SetActive(false);
		foreach (FieldCat gameField in fields.Values)
		{
			gameField.ResetField();
		}
		UpdateNextPlayer();
		// Points game
		pointsX = 0;
		playerIconX.sprite = players[playerID.X].image;
		pointPanelX.color = players[playerID.X].color;
		pointsO = 0;
		playerIconO.sprite = players[playerID.O].image;
		pointPanelO.color = players[playerID.O].color;
		pointsGamePanel.SetActive(PlayerSettings.settings.pointGame);
		PointsChange();
		round = 1;
		roundID.text = "Round: " + round;
	}

	public void Rematch(bool swapNextPlayer) => ResetGame(swapNextPlayer ^ (startingPlayer == playerID.O) ? playerID.O : playerID.X);

	public void UpdateNextPlayer()
	{
		nextPlayerPanel.color = players[nextPlayer].color;
		nextPlayerIcon.sprite = players[nextPlayer].image;
	}

	public void DeclareDraw()
	{
		if (!PlayerSettings.settings.pointGame)
			showEndgame(playerID.none);
		else
		{
			switch (true) // Check which player has won and show their respective win
			{
				case true when pointsX > pointsO:
					showEndgame(playerID.X);
					break;
				case true when pointsO > pointsX:
					showEndgame(playerID.O);
					break;
				default:
					showEndgame(playerID.none);
					break;
			}
		}
	}

	// Points game
	public void PointsChange()
	{
		poinTextX.text = $"{pointsX}/{PlayerSettings.settings.winPoints}";
		poinTextO.text = $"{pointsO}/{PlayerSettings.settings.winPoints}";
	}

	public void RoundWinner(playerID winner)
	{
		switch (winner) // Increment player's points (default = draw = no points)
		{
			case playerID.X: pointsX++; break;
			case playerID.O: pointsO++; break;
		}

		// Check if players have reached win points and game has ended
		if (pointsX >= PlayerSettings.settings.winPoints || pointsO >= PlayerSettings.settings.winPoints)
			switch (true) // Check which player has won and show their respective win
			{
				case true when pointsX > pointsO:
					showEndgame(playerID.X);
					break;
				case true when pointsO > pointsX:
					showEndgame(playerID.O);
					break;
				default:
					Debug.LogError("Both have too much points!");
					showEndgame(playerID.none);
					break;
			}
		else // Noone has won yet, continue show roind-intermediary screen
		{
			interRoundPanel.SetActive(true);
			nextPlayerPanel.gameObject.SetActive(false);
			if (winner == playerID.none)
			{ // Draw
				interRoundTitle.text = "It's a DRAW!";
				interRoundCat.SetBool("isDraw", true);
				roundWinner.gameObject.SetActive(false);
			}
			else
			{
				interRoundTitle.text = "Round winner:\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0\u00A0<color=#00000000>.</color>";
				interRoundCat.SetBool("isDraw", false);
				roundWinner.gameObject.SetActive(true);
				roundWinner.sprite = players[winner].image;
			}
			interRoundPanel.transform.GetChild(0).GetComponent<Image>().color = players[winner].color;
		}
		PointsChange();
	}

	public void NextRound()
	{
		round++;
		if (startingPlayer == playerID.none) startingPlayer = playerID.X;
		nextPlayer = (round & 1 ^ (startingPlayer == playerID.O ? 1 : 0)) == 0 ? playerID.O : playerID.X; // Swap next player each round
		EndgamePanel.SetActive(false);
		interRoundPanel.SetActive(false);
		nextPlayerPanel.gameObject.SetActive(true);
		winnerCat.gameObject.SetActive(false);
		roundWinner.gameObject.SetActive(false);
		foreach (FieldCat gameField in fields.Values)
		{
			gameField.ResetField();
		}
		UpdateNextPlayer();
		roundID.text = "Round: " + round;
	}
}

public enum fieldPos
{
	TopLeft,
	TopCenter,
	TopRight,
	MiddleLeft,
	MiddleCenter,
	MiddleRight,
	BottomLeft,
	BottomCenter,
	BottomRight
}

public enum playerID
{
	none,
	X,
	O
}

[Serializable]
public struct Player
{
	public Sprite image;
	public Color color;
}