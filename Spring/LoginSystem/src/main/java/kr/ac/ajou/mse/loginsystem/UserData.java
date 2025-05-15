package kr.ac.ajou.mse.loginsystem;

import java.util.UUID;

import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;
import lombok.ToString;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@ToString
public class UserData {

    @Id
    private UUID uuid;  //Unique Value
    private String name;
    @Column(unique = true)
    private String id;  //Unique Value
    private byte[] hashedpassword;

    

    public UserData(String name, String id, byte[] hashed_password) {
        this.name = name;
        this.id = id;
        this.hashedpassword = hashed_password;
    }

    //Comparing between User uses id
    //If same Id, same User.
    //If different Id, diffrent User. (Even they have same name, password, age, job)
    @Override
    public boolean equals(Object obj) {
        if (obj instanceof UserData) {
            UserData u = (UserData)obj;

            return uuid.equals(u.getUuid());
        } else {
            return false;
        }
    }
}
