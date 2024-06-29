package com.example.app_java.Models;

import com.fasterxml.jackson.annotation.JsonProperty;
import java.util.List;

public class Game {
    @JsonProperty("handle")
    private final String handle;

    @JsonProperty("turns_taken")
    private final int turnsTaken;

    @JsonProperty("time_taken")
    private final int timeTaken;

    @JsonProperty("cards")
    private final List<Card> cards;

    public Game(String handle, int turnsTaken, int timeTaken, List<Card> cards) {
        this.handle = handle;
        this.turnsTaken = turnsTaken;
        this.timeTaken = timeTaken;
        this.cards = cards;
    }

    // Getters
    public String getHandle() {
        return handle;
    }

    public int getTurnsTaken() {
        return turnsTaken;
    }

    public int getTimeTaken() {
        return timeTaken;
    }

    public List<Card> getCards() {
        return cards;
    }
}