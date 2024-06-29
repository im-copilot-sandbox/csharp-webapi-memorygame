package com.example.app_java.Controllers;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import com.example.app_java.Models.Game;
import com.example.app_java.Models.Leaderboard;
import com.example.app_java.Services.GameDataService;

import java.util.List;
import java.util.concurrent.CompletableFuture;

@RestController
@RequestMapping("/")
public class RoutesController {

    private final GameDataService gameDataService;

    @Autowired
    public RoutesController(GameDataService gameDataService) {
        this.gameDataService = gameDataService;
    }

    // Route to fetch data from the JSON file
    @GetMapping("greeting")
    public ResponseEntity<String> greeting() {
        return ResponseEntity.ok("Hello, fart!");
    }

    // POST /game - Save game data
    @PostMapping("game")
    public CompletableFuture<ResponseEntity<?>> postGame(@RequestBody Game game) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Validation checks for game data
                if (game == null) {
                    return ResponseEntity.badRequest().body("Game data is required.");
                }

                if (game.getHandle() == null || game.getHandle().isEmpty()) {
                    return ResponseEntity.badRequest().body("Game handle is required.");
                }

                // Ensure the number of cards is even
                if (game.getCards() == null || game.getCards().size() % 2 != 0) {
                    return ResponseEntity.badRequest().body("The number of cards must be even.");
                }

                game.getCards().forEach(card -> {
                    if (card.getType() == null || card.getType().isEmpty()) {
                        throw new IllegalArgumentException("Each card must have a valid type.");
                    }
                });

                // Save game data asynchronously
                gameDataService.saveGameAsync(game, game.getHandle());
                return ResponseEntity.ok().build();
            } catch (Exception e) {
                return ResponseEntity.badRequest().body(e.getMessage());
            }
        });
    }

    // GET /game/handle - Retrieve game data by handle
    @GetMapping("game/{handle}")
    public CompletableFuture<ResponseEntity<?>> getGame(@PathVariable String handle) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Validate input
                if (handle == null || handle.trim().isEmpty()) {
                    return ResponseEntity.badRequest().body("Handle is required.");
                }

                // Retrieve game data asynchronously
                var game = gameDataService.retrieveGameAsync(handle);
                if (game.isEmpty()) {
                    return ResponseEntity.notFound().build();
                }
                return ResponseEntity.ok(game.get());
            } catch (Exception e) {
                return ResponseEntity.badRequest().body(e.getMessage());
            }
        });
    }

    // POST /leaderboard - Save leaderboard entry
    @PostMapping("leaderboard")
    public CompletableFuture<ResponseEntity<?>> postLeaderboard(@RequestBody Leaderboard entry) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Validate leaderboard entry
                if (entry == null || entry.getHandle() == null || entry.getHandle().isEmpty()
                        || entry.getScore() <= 0) {
                    return ResponseEntity.badRequest().body("Invalid leaderboard entry.");
                }

                // Check if game data for the handle exists
                var gameData = gameDataService.retrieveGameAsync(entry.getHandle());
                if (gameData.isEmpty()) {
                    return ResponseEntity.badRequest().body("Invalid handle. No game data found.");
                }

                // Save leaderboard entry asynchronously
                gameDataService.saveLeaderboardEntryAsync(entry.getHandle(), entry.getScore());
                return ResponseEntity.ok("Leaderboard entry saved successfully.");
            } catch (Exception e) {
                return ResponseEntity.badRequest().body(e.getMessage());
            }
        });
    }

    // GET /leaderboard - Retrieve top ten leaderboard entries
    @GetMapping("leaderboard")
    public CompletableFuture<ResponseEntity<List<Leaderboard>>> getLeaderboard() {
        return CompletableFuture.supplyAsync(() -> {
            try {
                // Retrieve leaderboard data asynchronously
                var leaderboard = gameDataService.retrieveLeaderboardAsync();
                // Validate the leaderboard data is not null or empty
                if (leaderboard.isEmpty()) {
                    System.out.println("empty");
                    return ResponseEntity.notFound().build();
                }

                // Sort and take the top ten entries
                var topTen = leaderboard.stream().sorted((l1, l2) -> Integer.compare(l2.getScore(), l1.getScore()))
                        .limit(10).toList();
                return ResponseEntity.ok(topTen);
            } catch (Exception e) {
                System.out.println("Exception: " + e.getMessage());
                return ResponseEntity.badRequest().body(null);
            }
        });
    }
}