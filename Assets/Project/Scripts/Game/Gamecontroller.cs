using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gamecontroller : MonoBehaviour
{
    [Header("Game")]
    public Player player;

    [Header("UI")]
    public Text healthText;
    public Text ammoText;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "Health:" + player.Health;      
        ammoText.text = "Ammo: " +player.Ammo;
    }
}
