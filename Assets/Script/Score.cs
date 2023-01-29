using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{

    private int collectibles = 0;

    //textmesh pro
    [SerializeField] private TextMeshProUGUI collectiblesScoreText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectibles"))
        {
            Destroy(collision.gameObject);
            collectibles++;
            collectiblesScoreText.text = "Score: " + collectibles;
        }
    }


}
