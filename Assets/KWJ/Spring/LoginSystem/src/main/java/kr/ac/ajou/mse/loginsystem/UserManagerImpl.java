package kr.ac.ajou.mse.loginsystem;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserManagerImpl implements IUserManager{

    @Autowired
    private UserDataRepository repo;

    @Override
    public UserData addUser(String name, String id, String password) throws NoSuchAlgorithmException {
        if (name == null || id == null || password == null) {
            return null;
        }

        if (repo.existsById(id)) return null;

        byte[] hashed_password = hashingPassword(password);

        UUID uuid = UUID.randomUUID();

        return repo.save(new UserData(uuid, name, id, hashed_password));
    }

    public UserData login(String id, String password) throws NoSuchAlgorithmException {
        if (id == null || password == null) {
            return null;
        }

        byte[] hashed_password = hashingPassword(password);

        return repo.findByIdAndHashedpassword(id, hashed_password);
    }
    
    private byte[] hashingPassword(String password) throws NoSuchAlgorithmException {
        MessageDigest md = MessageDigest.getInstance("SHA-256");	// SHA-256 해시함수를 사용 
        byte[] hashed_password = password.getBytes();
		for(int i = 0; i < 500; i++) {
			String temp = password;	 
			md.update(temp.getBytes());						
			hashed_password = md.digest();							
		}
        return hashed_password;
    }
    @Override
    public List<UserData> fetchAll() {
        return repo.findAll();
    }

}
