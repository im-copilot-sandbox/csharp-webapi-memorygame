package com.example.app_java.Models;

import com.fasterxml.jackson.annotation.JsonProperty;

import java.time.LocalDateTime;

public class Leaderboard {
    @JsonProperty("handle")
    private String handle; // The handle of the player

    @JsonProperty("score")
    private int score; // The score achieved by the player

    @JsonProperty("last_played")
    private LocalDateTime lastPlayed; // The date and time when the game was completed

    // Default constructor for JSON deserialization
    public Leaderboard() {
    }

    // Constructor with parameters
    public Leaderboard(String handle, int score, LocalDateTime lastPlayed) {
        this.handle = handle;
        this.score = score;
        this.lastPlayed = lastPlayed;
    }

    // Getters and setters
    public String getHandle() {
        return handle;
    }

    public void setHandle(String handle) {
        this.handle = handle;
    }

    public int getScore() {
        return score;
    }

    public void setScore(int score) {
        this.score = score;
    }

    public LocalDateTime getLastPlayed() {
        return lastPlayed;
    }

    public void setLastPlayed(LocalDateTime lastPlayed) {
        this.lastPlayed = lastPlayed;
    }
}