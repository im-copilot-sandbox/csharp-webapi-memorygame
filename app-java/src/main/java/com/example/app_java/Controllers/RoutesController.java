package com.example.app_java.Controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.example.app_java.Models.Game;
import com.example.app_java.Models.Leaderboard;
import com.example.app_java.Services.GameData;

import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.stream.Collectors;

@RestController
@RequestMapping("/")
public class RoutesController {

    // Route to fetch data from the JSON file
    @GetMapping("greeting")
    public ResponseEntity<String> greeting() {
        return ResponseEntity.ok("Hello, World!");
    }

    // POST /game - Save game data
    @PostMapping("game")
    public CompletableFuture<ResponseEntity<?>> postGame(@RequestBody Game game) {
        // Validation checks for game data
        if (game == null) {
            return CompletableFuture.completedFuture(ResponseEntity.badRequest().body("Game data is required."));
        }

        if (game.getHandle() == null || game.getHandle().isEmpty()) {
            return CompletableFuture.completedFuture(ResponseEntity.badRequest().body("Game handle is required."));
        }

        // Ensure the number of cards is even
        if (game.getCards() == null || game.getCards().size() % 2 != 0) {
            return CompletableFuture
                    .completedFuture(ResponseEntity.badRequest().body("The number of cards must be even."));
        }

        for (var card : game.getCards()) {
            if (card.getType() == null || card.getType().isEmpty()) {
                return CompletableFuture
                        .completedFuture(ResponseEntity.badRequest().body("Each card must have a valid type."));
            }
        }

        // Save game data asynchronously
        return GameData.saveGameAsync(game, game.getHandle())
                .thenApply(v -> ResponseEntity.ok().build());
    }

    // GET /game/handle - Retrieve game data by handle
    @GetMapping("game/{handle}")
    public CompletableFuture<ResponseEntity<?>> getGame(@PathVariable String handle) {
        // Validate input
        if (handle == null || handle.trim().isEmpty()) {
            return CompletableFuture.completedFuture(ResponseEntity.badRequest().body("Handle is required."));
        }

        // Retrieve game data asynchronously
        return GameData.retrieveGameAsync(handle)
                .thenApply(game -> {
                    if (game == null) {
                        return ResponseEntity.notFound().build();
                    }
                    return ResponseEntity.ok(game);
                });
    }

    // POST /leaderboard - Save leaderboard entry
    @PostMapping("leaderboard")
    public CompletableFuture<ResponseEntity<?>> postLeaderboard(@RequestBody Leaderboard entry) {
        // Validate leaderboard entry
        if (entry == null || entry.getHandle() == null || entry.getHandle().isEmpty() || entry.getScore() <= 0) {
            return CompletableFuture.completedFuture(ResponseEntity.badRequest().body("Invalid leaderboard entry."));
        }

        // Check if game data for the handle exists
        return GameData.retrieveGameAsync(entry.getHandle())
                .thenCompose(gameData -> {
                    if (gameData == null) {
                        return CompletableFuture.completedFuture(
                                ResponseEntity.badRequest().body("Invalid handle. No game data found."));
                    }

                    // Save leaderboard entry asynchronously
                    return GameData.saveLeaderboardEntryAsync(entry.getHandle(), entry.getScore())
                            .thenApply(v -> ResponseEntity.ok("Leaderboard entry saved successfully."));
                });
    }

    // GET /leaderboard - Retrieve top ten leaderboard entries
    @GetMapping("leaderboard")
    public CompletableFuture<ResponseEntity<List<Leaderboard>>> getLeaderboard() {
        // Retrieve leaderboard data asynchronously
        return GameData.retrieveLeaderboardAsync()
                .thenApply(leaderboard -> {
                    // Validate the leaderboard data is not null or empty
                    if (leaderboard == null || leaderboard.isEmpty()) {
                        return ResponseEntity.notFound().build();
                    }

                    // Sort and take the top ten entries
                    var topTen = leaderboard.stream()
                            .sorted((l1, l2) -> Integer.compare(l2.getScore(), l1.getScore()))
                            .limit(10)
                            .collect(Collectors.toList());
                    return ResponseEntity.ok(topTen);
                });
    }
}