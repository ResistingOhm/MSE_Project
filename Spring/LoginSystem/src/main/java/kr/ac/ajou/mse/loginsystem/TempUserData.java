package kr.ac.ajou.mse.loginsystem;

import lombok.AllArgsConstructor;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@AllArgsConstructor
@ToString
public class TempUserData {
    private String name;
    private String id;
    private String pw;
}
