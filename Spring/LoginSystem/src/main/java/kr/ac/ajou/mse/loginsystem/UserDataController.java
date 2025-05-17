package kr.ac.ajou.mse.loginsystem;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.List;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import jakarta.annotation.PostConstruct;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;



@RestController
public class UserDataController {

    @Autowired
    private UserManager manager;

    @PostConstruct
    private void initSetting() throws IOException, NoSuchAlgorithmException {
        manager.addUser(new RequestUserData("Keun", "id", "password"));
        manager.addUser(new RequestUserData("Kim", "id2", "pass"));
        manager.addUser(new RequestUserData("Lee", "id3", "word"));
    }

    @PostMapping(value = "/add", produces = "application/json", consumes = "application/json")
    public ProducedUserData addUserInput(@RequestBody RequestUserData rud) throws NoSuchAlgorithmException {        
        UserData result = manager.addUser(rud);
        if (result == null) return null;
        ProducedUserData pud = new ProducedUserData(result);
        return pud;
    }

    @PostMapping(value = "/update/score", produces = "application/json", consumes = "application/json")
    public ScoreData updateScore(@RequestBody ScoreData s) {
        return manager.updateScore(s);
    }
    

    @GetMapping(value = "/login", produces = "application/json")
    public ProducedUserData tryLogin(@RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {
        UserData u = manager.login(id, password);
        if (u == null) return null;
        ProducedUserData pud = new ProducedUserData(u);
        return pud;
    }
    
    
    //Fetch user.
    @GetMapping(value = "/fetch/all/user", produces = "application/json")
    public List<UserData> fetchAllUser() {

        return manager.fetchAllUserData();
    }

    @GetMapping(value = "/fetch/all/score", produces = "application/json")
    public List<ScoreData> fetchAllScore() {

        return manager.fetchAllScoreData();
    }

    @GetMapping("/check/id")
    public boolean isIdExist(@RequestParam String id) {
        return manager.isIdExist(id);
    }

    @GetMapping("/leaderboard")
    public List<ScoreData> fetchLeaderBoard() {
        return manager.fetchAllValuedScoreData();
    }
    
    @GetMapping("/like")
    public int giveLikeToScore(@RequestParam Long id) {
        return manager.giveLike(id);
    }

}
