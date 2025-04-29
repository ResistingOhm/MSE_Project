package kr.ac.ajou.mse.loginsystem;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.ArrayList;
import java.util.UUID;

import org.springframework.stereotype.Service;

@Service
public class UserManagerImpl implements IUserManager{

    private ArrayList<User> users = new ArrayList<User>();

    @Override
    public boolean addUser(String name, String id, String password) throws NoSuchAlgorithmException {
        if (name == null || id == null || password == null) {
            return false;
        }

        for (User u : users) {
            if(id.equals(u.getId())){
             return false;
            } 
        }

        byte[] hashed_password = hashingPassword(password);

        UUID uuid = UUID.randomUUID();

        User u = new User(name, id, hashed_password,uuid);
        users.add(u);
        return true;
    }

    public User login(String id, String password) throws NoSuchAlgorithmException {
        if (id == null || password == null) {
            return null;
        }

        byte[] hashed_password = hashingPassword(password);

        for (User u : users) {
            if(u.compareIdPassword(id, hashed_password)){
             return u;
            } 
        }

        return null;
    }
    
    private byte[] hashingPassword(String password) throws NoSuchAlgorithmException {
        MessageDigest md = MessageDigest.getInstance("SHA-256");	// SHA-256 해시함수를 사용 
        byte[] hashed_password = password.getBytes();
		for(int i = 0; i < 10000; i++) {
			String temp = password;	 
			md.update(temp.getBytes());						
			hashed_password = md.digest();							
		}
        return hashed_password;
    }

    @Override
    public boolean removeUser(String uuid) {
        UUID id = UUID.fromString(uuid);
        for (User u : users) {
            if (u.getUuid().equals(id)) {
                users.remove(u);
                return true;
            }
        }
        return false;
    }

    @Override
    public boolean removeAll() {
        users.clear();
        return true;
    }

    @Override
    public ArrayList<User> fetchAll() {
        return users;
    }

    @Override
    public User fetchById(String uuid) {
        UUID id = UUID.fromString(uuid);
        for (User u : users) {
            if (u.getUuid().equals(id)) {
                return u;
            }
        }
        return null;
    }

}
