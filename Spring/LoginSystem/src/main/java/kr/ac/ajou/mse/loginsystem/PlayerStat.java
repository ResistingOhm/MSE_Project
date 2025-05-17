package kr.ac.ajou.mse.loginsystem;

import jakarta.persistence.Embeddable;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@ToString
@Embeddable
public class PlayerStat {

    private int hp;
    private int atk;
    private int def;
    private int lck;
    private int spd;
    private int lvl;
}
