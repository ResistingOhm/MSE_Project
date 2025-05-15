package kr.ac.ajou.mse.loginsystem;

import java.sql.Date;

import jakarta.persistence.Temporal;
import jakarta.persistence.TemporalType;
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
public class ScoreData {

    private long score;
    private int enemynum;
    private PlayerStat playerstat;
    @Temporal(TemporalType.TIMESTAMP)
    private Date time;

}
