using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerSpawn1;
    [SerializeField] private GameObject playerSpawn2;
    private GameObject player1;
    private GameObject player2;
    private int p1Score = 0;
    private int p2Score = 0;
    private UIM UIM = null;

    // Start is called before the first frame update
    void Start()
    {

        UIM = GameObject.Find("Canvas").GetComponent<UIM>();
        StartRound();

    }

    // Update is called once per frame
    void Update()
    {

        if (player1 == null)
        {

            Destroy(player2.gameObject);
            p1Score++;
            StartRound();

        }

        if (player2 == null)
        {

            Destroy(player1.gameObject);
            p2Score++;
            StartRound();

        }

    }

    public void StartRound()
    {

        

        UIM.UpdateScore(p1Score, p2Score);

        player1 = Instantiate(player, playerSpawn1.transform.position, Quaternion.identity);
        player1.GetComponent<PlayerBase>().SetPlayer(1);

        player2 = Instantiate(player, playerSpawn2.transform.position, Quaternion.identity);
        player2.GetComponent<PlayerBase>().SetPlayer(2);

        UIM.UpdateLives(3, 3);

    }

}
