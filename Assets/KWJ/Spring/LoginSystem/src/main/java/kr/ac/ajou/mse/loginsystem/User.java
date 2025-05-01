package kr.ac.ajou.mse.loginsystem;

import java.util.UUID;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.Setter;

@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class User {

    private String name;
    private String id;  //Unique Value
    private byte[] hashed_password;
    private UUID uuid;  //Unique Value

    //Comparing between User uses id
    //If same Id, same User.
    //If different Id, diffrent User. (Even they have same name, password, age, job)
    @Override
    public boolean equals(Object obj) {
        if (obj instanceof User) {
            User u = (User)obj;

            return uuid.equals(u.getUuid());
        } else {
            return false;
        }
    }

    public boolean compareIdPassword(String inputId, byte[] inputPassword) {

        if (!id.equals(inputId)) return false;

        return java.util.Arrays.equals(hashed_password, inputPassword);
    }
}
