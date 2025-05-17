package kr.ac.ajou.mse.loginsystem;

import java.util.List;

import org.springframework.data.jpa.repository.JpaRepository;

public interface ScoreDataRepository extends JpaRepository<ScoreData, Long> {

    List<ScoreData> findTop100ByScoreGreaterThanOrderByScoreDesc(long score);

}
