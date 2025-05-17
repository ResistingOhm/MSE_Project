package kr.ac.ajou.mse.loginsystem;

import lombok.Getter;
import lombok.ToString;

@Getter
@ToString
public class ProducedUserData {
    private String uuid;
    private String name;
    private float highscore;
    private long scoreid;

    public ProducedUserData (UserData u) {
        this.uuid = u.getUuid().toString();
        this.name = u.getName();
        this.highscore = u.getHighscore().getScore();
        this.scoreid = u.getHighscore().getId();
    }
}
