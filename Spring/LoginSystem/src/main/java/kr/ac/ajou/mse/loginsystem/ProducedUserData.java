package kr.ac.ajou.mse.loginsystem;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@AllArgsConstructor
@ToString
public class ProducedUserData {
    private String uuid;
    private String name;
    private float highscore;

    public ProducedUserData(UserData u) {
        this.uuid = u.getUuid().toString();
        this.name = u.getName();
        this.highscore = 1;//u.getHighscore().getScore();
    }
}
