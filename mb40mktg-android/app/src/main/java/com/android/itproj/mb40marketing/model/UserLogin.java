package com.android.itproj.mb40marketing.model;

import lombok.Getter;
import lombok.RequiredArgsConstructor;
import lombok.ToString;

@ToString
@RequiredArgsConstructor(staticName = "initialize")
public class UserLogin {

    @Getter
    private final String username;

    @Getter
    private final String password;
}
