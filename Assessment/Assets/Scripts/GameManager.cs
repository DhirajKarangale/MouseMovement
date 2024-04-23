using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] Text txtScore;
    [SerializeField] Player player;
    [SerializeField] GameObject objGameOver;

    private int score;
    private List<Item> items;


    protected override void Awake()
    {
        base.Awake();
        items = new List<Item>();
    }


    private void GameOver()
    {
        player.GameOver();
        objGameOver.SetActive(true);
    }


    internal void UpdateScore(Item item)
    {
        items.Remove(item);
        score += item.isRed ? -1 : 1;
        txtScore.text = "Score: " + score.ToString();

        if (items.Count == 0) GameOver();
    }

    internal void AddItem(Item item)
    {
        items.Add(item);
    }
}