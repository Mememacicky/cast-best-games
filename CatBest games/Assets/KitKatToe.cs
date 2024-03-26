using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

	public void fieldClick(int position)
	{
        Debug.Log((fieldPos)position);
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