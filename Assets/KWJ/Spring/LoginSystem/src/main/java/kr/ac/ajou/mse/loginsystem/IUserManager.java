package kr.ac.ajou.mse.loginsystem;

import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;

public interface IUserManager {
    public boolean addUser(String name, String id, String password) throws NoSuchAlgorithmException;
    public User login(String id, String password) throws NoSuchAlgorithmException;
    public boolean removeUser(String uuid);
    public boolean removeAll();
    public ArrayList<User> fetchAll();
    public User fetchById(String uuid);
}
