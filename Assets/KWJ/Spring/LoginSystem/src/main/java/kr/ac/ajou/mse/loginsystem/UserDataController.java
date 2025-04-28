package kr.ac.ajou.mse.loginsystem;

import java.io.IOException;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestParam;
import jakarta.annotation.PostConstruct;
import org.springframework.web.bind.annotation.PostMapping;



@Controller
public class UserDataController {

    @Autowired
    private IUserManager manager;

    @PostConstruct
    private void initSetting() throws IOException {

    }

    //home. No function include
    @GetMapping("/")
    public String home(Model model) {
        return "home";
    }
    
    //add user.
    @GetMapping("/add")
    public String addUserForm(Model model) {
        return "adduser";
    }

    @PostMapping("/add")
    public String addUserInput(@RequestParam String name, @RequestParam String id, @RequestParam String password, Model model) throws NoSuchAlgorithmException {        
        boolean result = manager.addUser(name, id, password);
        String s = "Fail to add user: Id is already using";
        if (result) {
            s = "Successfully add user: " + name;
        }
        model.addAttribute("result", s);
        return "adduserresult";
    }
    
    //remove user.
    @GetMapping("/remove")
    public String removeUserByID(@RequestParam(name = "userId", required = true) String userId, Model model) {

        boolean result = manager.removeUser(userId);
        String s = "Fail to remove user : no id exist";
        if (result) {
            s = "Successfully remove user: " + userId;
        }
        model.addAttribute("result", s);

        return "removeresult";
    }

    @GetMapping("/remove_all")
    public String removeAllUser(Model model) {
        manager.removeAll();
        model.addAttribute("result", "Successfully remove all users");
        return "removeresult";
    }
    
    //Fetch user.
    @GetMapping("/fetch/all")
    public String fetchAllUser(Model model) {
        ArrayList<User> u = manager.fetchAll();
        model.addAttribute("UserList", u);
        return "fetchuser";
    }
    
    @GetMapping("/fetch/userId/{userId}")
    public String fetchUserByID(@PathVariable String userId, Model model) {
        User u = manager.fetchById(userId);
        model.addAttribute("UserList", u);
        return "fetchuser";
    }


}
