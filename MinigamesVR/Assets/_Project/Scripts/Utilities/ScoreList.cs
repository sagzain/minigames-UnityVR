using System;
using System.Collections.Generic;

[Serializable]
public class ScoreList
{
    public List<PlayerScore> scoreList;

    public ScoreList()
    {
        scoreList = new List<PlayerScore>();
    }
}