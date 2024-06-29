package com.example.app_java.Models;

import com.fasterxml.jackson.annotation.JsonProperty;
import java.util.List;

public class Game {
    @JsonProperty("handle")
    private String handle;

    @JsonProperty("turns_taken")
    private int turnsTaken;

    @JsonProperty("time_taken")
    private int timeTaken;

    @JsonProperty("cards")
    private List<Card> cards;

    // Default constructor for JSON deserialization
    public Game() {
    }

    // Constructor with parameters
    public Game(String handle, int turnsTaken, int timeTaken, List<Card> cards) {
        this.handle = handle;
        this.turnsTaken = turnsTaken;
        this.timeTaken = timeTaken;
        this.cards = cards;
    }

    // Getters and setters
    public String getHandle() {
        return handle;
    }

    public void setHandle(String handle) {
        this.handle = handle;
    }

    public int getTurnsTaken() {
        return turnsTaken;
    }

    public void setTurnsTaken(int turnsTaken) {
        this.turnsTaken = turnsTaken;
    }

    public int getTimeTaken() {
        return timeTaken;
    }

    public void setTimeTaken(int timeTaken) {
        this.timeTaken = timeTaken;
    }

    public List<Card> getCards() {
        return cards;
    }

    public void setCards(List<Card> cards) {
        this.cards = cards;
    }
}