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
    private IUserManager manager;

    @PostConstruct
    private void initSetting() throws IOException, NoSuchAlgorithmException {
        manager.addUser("Keun", "id", "password");
        manager.addUser("Kim", "id2", "pass");
        manager.addUser("Lee", "id3", "word");
    }

    @PostMapping(value = "/add", produces = "application/json", consumes = "application/json")
    public ProducedUserData addUserInput(@RequestBody TempUserData tud) throws NoSuchAlgorithmException {        
        UserData result = manager.addUser(tud.getName(), tud.getId(), tud.getPw());
        ProducedUserData pud = new ProducedUserData(result);
        return pud;
    }

    @GetMapping(value = "/login", produces = "application/json")
    public ProducedUserData tryLogin(@RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {
        UserData u = manager.login(id, password);
        ProducedUserData pud = new ProducedUserData(u);
        return pud;
    }
    
    
    //Fetch user.
    @GetMapping(value = "/fetch/all", produces = "application/json")
    public List<UserData> fetchAllUser() {

        return manager.fetchAll();
    }

}
