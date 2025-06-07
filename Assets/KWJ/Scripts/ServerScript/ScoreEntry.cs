using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreEntry : MonoBehaviour
{
    public TMP_Text pos;
    public TMP_Text username;
    public TMP_Text level;
    public TMP_Text score;
    public TMP_Text likenum;

    private LeaderBoardScoreData scoredata;
    private int currentLikeNum;

    public void SetEntry(LeaderBoardScoreData s, int pos)
    {
        this.pos.text = pos.ToString();
        this.username.text = s.owner.name;
        this.level.text = s.gamelevel.ToString();
        this.score.text = s.score.ToString();

        scoredata = s;
        currentLikeNum = s.likenum;
        likenum.text = currentLikeNum.ToString();
    }

    public void OnClickGoodButton()
    {
        NetworkManager.apiManager.LikeScore(scoredata.id);
        //+likenum in client
        currentLikeNum++;
        likenum.text = currentLikeNum.ToString();
    }
}
