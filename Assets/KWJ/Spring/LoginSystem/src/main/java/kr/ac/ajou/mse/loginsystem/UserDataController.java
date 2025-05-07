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



@RestController
public class UserDataController {

    @Autowired
    private IUserManager manager;

    @PostConstruct
    private void initSetting() throws IOException, NoSuchAlgorithmException {
        manager.addUser("Keun", "id", "password");
        manager.addUser("Kim", "id2", "pass");
        manager.addUser("Lee", "id3", "word");
    }

    @PostMapping(value = "/add", produces = "application/json")
    public UserData addUserInput(@RequestParam String name, @RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {        
        UserData result = manager.addUser(name, id, password);
        return result;
    }

    @GetMapping(value = "/login", produces = "application/json")
    public UserData tryLogin(@RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {
        UserData u = manager.login(id, password);

        return u;
    }
    
    
    //Fetch user.
    @GetMapping(value = "/fetch/all", produces = "application/json")
    public List<UserData> fetchAllUser() {

        return manager.fetchAll();
    }

}
