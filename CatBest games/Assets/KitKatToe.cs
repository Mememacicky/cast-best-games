using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

	public playerID nextPlayer = playerID.none;

	public Sprite playerXimg;
	public Color colorX = Color.red;
	public Sprite playerOimg;
	public Color colorO = Color.blue;

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
		}
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