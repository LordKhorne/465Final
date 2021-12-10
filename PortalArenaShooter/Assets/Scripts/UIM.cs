using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIM : MonoBehaviour
{

    [SerializeField] private Text P1L2 = null;
    [SerializeField] private Text P1L3 = null;

    [SerializeField] private Text P2L2 = null;
    [SerializeField] private Text P2L3 = null;

    [SerializeField] private Text playerScore1 = null;
    [SerializeField] private Text playerScore2 = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLives(int playerNum, int lives)
    {

        if (lives == 2 && playerNum == 1)
        {

            P1L3.gameObject.SetActive(false);

        }

        if (lives == 2 && playerNum == 2)
        {

            P2L3.gameObject.SetActive(false);

        }

        if (lives == 1 && playerNum == 1)
        {

            P1L2.gameObject.SetActive(false);

        }

        if (lives == 1 && playerNum == 2)
        {

            P2L2.gameObject.SetActive(false);

        }

        else if (lives == 3)
        {

            P1L3.gameObject.SetActive(true);
            P1L2.gameObject.SetActive(true);
            P2L2.gameObject.SetActive(true);
            P2L3.gameObject.SetActive(true);

        }

    }

    public void UpdateScore(int Score1, int Score2)
    {

        playerScore1.text = "Player 1: " + Score1;
        playerScore2.text = "Player 2: " + Score2;

    }
}
