using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldCat : MonoBehaviour
{
    public playerID thisPlayer = playerID.none;
    public fieldPos fieldPos;

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
			catImage.color = Color.white;
            panelIm.color = KitKatToe.players[pID].color;

            thisPlayer = pID;
            return true;
		}
        return false;
	}
}
