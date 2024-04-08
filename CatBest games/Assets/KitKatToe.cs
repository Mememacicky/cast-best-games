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

    public playerID nextPlayer = playerID.none;

    public Sprite playerX;
    public Color colorX = Color.red;
    public Sprite playerO;
    public Color colorO = Color.blue;

    public Dictionary<fieldPos, GameObject> fields = new Dictionary<fieldPos, GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        if (fields == null)
        {
            fields = new Dictionary<fieldPos, GameObject>();
		}
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
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void fieldClick(int position)
	{
        Debug.Log((fieldPos)position);
        Image renderer = fields[(fieldPos)position].transform.GetChild(0).GetComponent<Image>();
        Image fieldIm = fields[(fieldPos)position].GetComponent<Image>();

		switch (nextPlayer)
        {
            case playerID.X:
                renderer.sprite = playerX;
                nextPlayer = playerID.O;
                fieldIm.color = colorX;
                break;
            case playerID.O:
                renderer.sprite = playerO;
                nextPlayer = playerID.X;
				fieldIm.color = colorO;
				break;
            default:
                Debug.LogError("Invalid next player");
                nextPlayer = playerID.X;
                break;
		}
        renderer.color = Color.white;
		//fields[(fieldPos)position].GetComponentInChildren<Image>().gameObject.SetActive(false);
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