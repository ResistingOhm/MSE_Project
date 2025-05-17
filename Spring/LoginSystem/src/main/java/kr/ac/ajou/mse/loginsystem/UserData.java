package kr.ac.ajou.mse.loginsystem;

import java.util.UUID;

import com.fasterxml.jackson.annotation.JsonIgnoreProperties;

import jakarta.persistence.CascadeType;
import jakarta.persistence.Column;
import jakarta.persistence.Entity;
import jakarta.persistence.FetchType;
import jakarta.persistence.Id;
import jakarta.persistence.JoinColumn;
import jakarta.persistence.OneToOne;
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

    @OneToOne(mappedBy = "owner", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    @JoinColumn(name = "scoredata_id")
    @JsonIgnoreProperties({"owner"})
    private ScoreData highscore;

    

    public UserData(UUID uuid, String name, String id, byte[] hashed_password) {
        this.uuid = uuid;
        this.name = name;
        this.id = id;
        this.hashedpassword = hashed_password;
        setHighscore(new ScoreData(-1, -1, new PlayerStat(0,0,0,0,0,0),0,0));
    }

    public void setHighscore(ScoreData s) {
        if(s!=null) {
            s.setOwner(this);
            this.highscore = s;
        }
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
