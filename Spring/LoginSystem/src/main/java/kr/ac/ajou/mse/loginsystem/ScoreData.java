package kr.ac.ajou.mse.loginsystem;

import com.fasterxml.jackson.annotation.JsonIncludeProperties;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Embedded;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.GenerationType;
import jakarta.persistence.Id;
import jakarta.persistence.OneToOne;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Entity
public class ScoreData {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "scoredata_id")
    private Long id;
    
    private long score;
    private int enemynum;
    private int gamelevel;
    @Embedded
    private PlayerStat playerstat;

    private int min;
    private int sec;

    private int likenum;

    @OneToOne(fetch = FetchType.LAZY, cascade = CascadeType.PERSIST)
    @JsonIncludeProperties({"uuid", "name"})
    private UserData owner;

    public ScoreData(long score, int enemynum, int gamelevel, PlayerStat playerstat, int min, int sec) {
        this.score = score;
        this.enemynum = enemynum;
        this.gamelevel = gamelevel;
        this.playerstat = playerstat;
        this.min = min;
        this.sec = sec;
    }

    public ScoreData(Long id, long score, int enemynum, int gamelevel, PlayerStat playerstat, int min, int sec) {
        this.id = id;
        this.score = score;
        this.enemynum = enemynum;
        this.gamelevel = gamelevel;
        this.playerstat = playerstat;
        this.min = min;
        this.sec = sec;
    }

    public int likeit() {
        return ++likenum;
    }

}
