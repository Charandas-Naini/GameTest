using System;
using System.Collections.Generic;

[Serializable]
public class SaveData
{
    public int rows;
    public int cols;
    public ScoreData scoreData = new ScoreData();

    public List<CardData> cards = new List<CardData>();
}

[Serializable]
public struct ScoreData
{
    public int score;
    public int matches;
    public int turns;
}

