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

    private float hp;
    private float atk;
    private float def;
    private float lck;
    private float spd;
    private int lvl;
}
