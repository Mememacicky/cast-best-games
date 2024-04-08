using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCat : MonoBehaviour
{
    public playerID thisPlayer = playerID.none;
    public fieldPos fieldPos;

    public static Color catIn = Color.white;
    public static Color catOut = new Color(0.3f, 0.3f, 0.3f, 0.6f);

    // Start is called before the first frame update
    void Start()
    {
        
    }

	public bool PlayerChange(playerID pID)
	{
        if(thisPlayer == playerID.none)
        {
			Image catImage = transform.GetChild(0).GetComponent<Image>();
			Image panelIm = gameObject.GetComponent<Image>();

            catImage.sprite = KitKatToe.players[pID].image;
			catImage.color = catIn;
            panelIm.color = KitKatToe.players[pID].color;

            thisPlayer = pID;
            return true;
		}
        return false;
	}

	public void ResetField()
	{
		thisPlayer = playerID.none;

		Image catImage = transform.GetChild(0).GetComponent<Image>();
		Image panelIm = gameObject.GetComponent<Image>();

		catImage.sprite = KitKatToe.players[playerID.none].image;
		catImage.color = catOut;
		panelIm.color = KitKatToe.players[playerID.none].color;
	}
}
