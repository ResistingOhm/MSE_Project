package kr.ac.ajou.mse.loginsystem;

import java.security.NoSuchAlgorithmException;
import java.util.List;

public interface IUserManager {
    public UserData addUser(String name, String id, String password) throws NoSuchAlgorithmException;
    public UserData login(String id, String password) throws NoSuchAlgorithmException;
    public List<UserData> fetchAll();
    public boolean isIdExist(String id);
}
