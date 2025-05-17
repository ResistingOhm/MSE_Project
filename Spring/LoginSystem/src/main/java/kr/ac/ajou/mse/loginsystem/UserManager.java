package kr.ac.ajou.mse.loginsystem;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.util.List;
import java.util.UUID;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

@Service
public class UserManager{

    @Autowired
    private UserDataRepository uRepo;
    @Autowired
    private ScoreDataRepository sRepo;

    public UserData addUser(RequestUserData u) throws NoSuchAlgorithmException {
        if (u == null) {
            return null;
        }

        if (uRepo.existsById(u.getId())) return null;

        byte[] hashed_password = hashingPassword(u.getPw());

        UUID uuid = UUID.randomUUID();

        return uRepo.save(new UserData(uuid, u.getName(), u.getId(), hashed_password));
    }

    public UserData login(String id, String password) throws NoSuchAlgorithmException {
        if (id == null || password == null) {
            return null;
        }

        byte[] hashed_password = hashingPassword(password);

        return uRepo.findByIdAndHashedpassword(id, hashed_password);
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

    public List<UserData> fetchAllUserData() {
        return uRepo.findAll();
    }

    public List<ScoreData> fetchAllScoreData() {
        return sRepo.findAll();
    }

    public boolean isIdExist(String id) {
        return uRepo.existsById(id);
    }

    public ScoreData updateScore(ScoreData s) {
        UserData u = sRepo.findById(s.getId()).get().getOwner();
        u.setHighscore(s);
        uRepo.save(u);
        return sRepo.findById(s.getId()).get();

        //return sRepo.save(s);  <- Need to update UserData.
    }

    public List<ScoreData> fetchAllValuedScoreData() {
        return sRepo.findTop100ByScoreGreaterThanOrderByScoreDesc(0);
    }

    public int giveLike(Long id) {
        ScoreData sd = sRepo.findById(id).get();
        sd.likeit();
        sd = sRepo.save(sd);
        return sd.getLikenum();
    }
}
