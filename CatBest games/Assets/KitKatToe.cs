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
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void fieldClick(int position)
	{
        Debug.Log((fieldPos)position);
        fields[(fieldPos)position].GetComponentInChildren<Image>().gameObject.SetActive(false);
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