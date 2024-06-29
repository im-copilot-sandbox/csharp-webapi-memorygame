package com.example.app_java.Models;

import com.fasterxml.jackson.annotation.JsonProperty;

public class Card {
    // Represents a card in the memory game.

    @JsonProperty("type")
    private String type; // Type of the card, i.e., image, color, number, etc.

    @JsonProperty("flipped")
    private Boolean flipped; // Indicates whether the card is hidden or flipped.

    // Constructor
    public Card() {
    }

    // Getter and setter for type
    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    // Getter and setter for flipped
    public Boolean getFlipped() {
        return flipped;
    }

    public void setFlipped(Boolean flipped) {
        this.flipped = flipped;
    }
}