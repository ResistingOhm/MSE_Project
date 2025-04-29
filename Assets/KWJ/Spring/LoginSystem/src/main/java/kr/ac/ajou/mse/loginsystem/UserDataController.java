package kr.ac.ajou.mse.loginsystem;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
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

    @PostMapping("/add")
    public String addUserInput(@RequestParam String name, @RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {        
        boolean result = manager.addUser(name, id, password);
        String s = "Fail to add user: Id is already using";
        if (result) {
            s = "Successfully add user: " + name;
        }
        return s;
    }

    @PostMapping(value = "/login", produces = "application/json")
    public User tryLogin(@RequestParam String id, @RequestParam String password) throws NoSuchAlgorithmException {
        User u = manager.login(id, password);

        return u;
    }
    
    
    //remove user.
    @GetMapping("/remove")
    public String removeUserByID(@RequestParam(name = "userId", required = true) String userId) {

        boolean result = manager.removeUser(userId);
        String s = "Fail to remove user : no id exist";
        if (result) {
            s = "Successfully remove user: " + userId;
        }
        return s;
    }

    @GetMapping("/remove_all")
    public String removeAllUser() {
        manager.removeAll();
        return "Successfully remove all users";
    }
    
    //Fetch user.
    @GetMapping(value = "/fetch/all", produces = "application/json")
    public ArrayList<User> fetchAllUser() {

        return manager.fetchAll();
    }
    
    @GetMapping(value = "/fetch/userId/{userId}", produces = "application/json")
    public User fetchUserByID(@PathVariable String userId) {

        return manager.fetchById(userId);
    }

}
