using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class KitKatToe : MonoBehaviour
{
	public GameObject TL;
	public GameObject TC;
	public GameObject TR;
	public GameObject ML;
	public GameObject MC;
	public GameObject MR;
	public GameObject BL;
	public GameObject BC;
	public GameObject BR;

	public GameObject EngamePanel;
	public TextMeshProUGUI endTitle;

	public playerID nextPlayer = playerID.none;

	public Sprite playerXimg;
	public Color colorX = Color.red;
	public Sprite playerOimg;
	public Color colorO = Color.blue;

	public Sprite playerNoneImg;
	public Color colorNone = new Color(0.6395372f, 0.8335454f, 0.8943396f);

	public static Dictionary<playerID, Player> players;
	public static Dictionary<fieldPos, GameObject> fields = new Dictionary<fieldPos, GameObject>();

	// Start is called before the first frame update
	void Start()
	{
		if (fields == null)
			fields = new Dictionary<fieldPos, GameObject>();
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
		nextPlayer = playerID.X;

		players.Add(playerID.X, new Player() { ID = playerID.X, color = colorX, image = playerXimg });
		players.Add(playerID.O, new Player() { ID = playerID.O, color = colorO, image = playerOimg });
		players.Add(playerID.none, new Player() { ID = playerID.none, color = colorNone, image = playerNoneImg });

		EngamePanel.SetActive(false);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void fieldClick(int position)
	{
		bool success = fields[(fieldPos) position].GetComponent<FieldCat>().PlayerChange(nextPlayer);

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
			playerID? winner = checkEndgame();
			if (winner != null)
				showEndgame((playerID) winner);
		}
	}

	public playerID? checkEndgame()
	{
		playerID[,] currentStatus = new playerID[3,3];
		currentStatus[0,0] = fields[fieldPos.TopLeft].GetComponent<FieldCat>().thisPlayer;
		currentStatus[0,1] = fields[fieldPos.TopCenter].GetComponent<FieldCat>().thisPlayer;
		currentStatus[0,2] = fields[fieldPos.TopRight].GetComponent<FieldCat>().thisPlayer;
		currentStatus[1,0] = fields[fieldPos.MiddleLeft].GetComponent<FieldCat>().thisPlayer;
		currentStatus[1,1] = fields[fieldPos.MiddleCenter].GetComponent<FieldCat>().thisPlayer;
		currentStatus[1,2] = fields[fieldPos.MiddleRight].GetComponent<FieldCat>().thisPlayer;
		currentStatus[2,0] = fields[fieldPos.BottomLeft].GetComponent<FieldCat>().thisPlayer;
		currentStatus[2,1] = fields[fieldPos.BottomCenter].GetComponent<FieldCat>().thisPlayer;
		currentStatus[2,2] = fields[fieldPos.BottomRight].GetComponent<FieldCat>().thisPlayer;

		// Check rows
		for(int i = 0; i < 3; i++)
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
		int filledFIelds = 0;
		foreach(playerID fld in currentStatus)
		{
			if (fld != playerID.none)
				filledFIelds++;
		}
		if(filledFIelds == 9)
			return playerID.none;
		return null;
	}

	public void showEndgame(playerID winner)
	{
		EngamePanel.SetActive(true);
		Debug.Log("Winner: "+winner.ToString());
		endTitle.text = "Winner: " + winner.ToString();
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

public struct Player
{
	public playerID ID;
	public Sprite image;
	public Color color;
}