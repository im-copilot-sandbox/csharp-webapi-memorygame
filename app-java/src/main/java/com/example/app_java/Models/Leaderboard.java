package com.example.app_java.Models;

import com.fasterxml.jackson.annotation.JsonProperty;
import java.time.LocalDateTime;

public class Leaderboard {
    @JsonProperty("handle")
    private final String handle;

    @JsonProperty("score")
    private final int score;

    @JsonProperty("last_played")
    private final LocalDateTime lastPlayed;

    public Leaderboard(String handle, int score, LocalDateTime lastPlayed) {
        this.handle = handle;
        this.score = score;
        this.lastPlayed = lastPlayed;
    }

    // Getters
    public String getHandle() {
        return handle;
    }

    public int getScore() {
        return score;
    }

    public LocalDateTime getLastPlayed() {
        return lastPlayed;
    }
}