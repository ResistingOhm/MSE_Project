package kr.ac.ajou.mse.loginsystem;

import lombok.AllArgsConstructor;
import lombok.Getter;

@Getter
@AllArgsConstructor
public class RequestUserData {
    private String name;
    private String id;
    private String pw;
}
