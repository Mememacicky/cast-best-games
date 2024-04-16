using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
	public Player nullPlayer = new Player() { color = new Color(0.6395372f, 0.8335454f, 0.8943396f) };
	public List<Player> players = new List<Player>();

	// Start is called before the first frame update
	void Start()
    {
        
    }
}
