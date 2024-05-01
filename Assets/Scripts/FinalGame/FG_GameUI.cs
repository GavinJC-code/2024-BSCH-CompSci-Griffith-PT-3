using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //using TMPro namespace

public class FG_GameUI : MonoBehaviour
{
    public TMP_Text scoreText;


    public FG_GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<FG_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + gameManager.score;
    }
}