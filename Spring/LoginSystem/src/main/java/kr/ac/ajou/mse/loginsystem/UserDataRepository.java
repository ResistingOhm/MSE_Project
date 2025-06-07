package kr.ac.ajou.mse.loginsystem;

import java.util.UUID;

import org.springframework.data.jpa.repository.JpaRepository;

public interface UserDataRepository extends JpaRepository<UserData, UUID>{

    UserData findByIdAndHashedpassword(String id, byte[] hashed_password);
    boolean existsById(String id);
}
