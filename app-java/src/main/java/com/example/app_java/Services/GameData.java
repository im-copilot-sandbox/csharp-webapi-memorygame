package com.example.app_java.Services;

import com.example.app_java.Models.Game;
import com.example.app_java.Models.Leaderboard;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;

import java.io.File;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.concurrent.CompletableFuture;
import java.util.stream.Collectors;

public class GameData {

    private static final String DATA_DIRECTORY = "data";
    private static final ObjectMapper objectMapper = new ObjectMapper();

    public static CompletableFuture<Void> saveGameAsync(Game game, String handle) {
        return CompletableFuture.runAsync(() -> {
            try {
                String sanitizedHandle = sanitizeHandle(handle);
                String filePath = Paths.get(DATA_DIRECTORY, sanitizedHandle + ".json").toString();
                if (!isPathWithinDirectory(filePath, DATA_DIRECTORY)) {
                    throw new IllegalStateException("Invalid file path.");
                }
                String jsonString = objectMapper.writeValueAsString(game);
                Files.write(Paths.get(filePath), jsonString.getBytes());
            } catch (IOException e) {
                e.printStackTrace();
            }
        });
    }

    public static CompletableFuture<Game> retrieveGameAsync(String handle) {
        return CompletableFuture.supplyAsync(() -> {
            try {
                String sanitizedHandle = sanitizeHandle(handle);
                String filePath = Paths.get(DATA_DIRECTORY, sanitizedHandle + ".json").toString();
                if (!Files.exists(Paths.get(filePath)) || !isPathWithinDirectory(filePath, DATA_DIRECTORY)) {
                    return null;
                }
                String jsonString = new String(Files.readAllBytes(Paths.get(filePath)));
                return objectMapper.readValue(jsonString, Game.class);
            } catch (IOException e) {
                e.printStackTrace();
                return null;
            }
        });
    }

    public static CompletableFuture<Void> saveLeaderboardEntryAsync(String handle, int score) {
        return CompletableFuture.runAsync(() -> {
            try {
                String leaderboardFile = Paths.get(DATA_DIRECTORY, "leaderboard.json").toString();
                List<Leaderboard> leaderboard;
                if (new File(leaderboardFile).exists()) {
                    String leaderboardContent = new String(Files.readAllBytes(Paths.get(leaderboardFile)));
                    leaderboard = objectMapper.readValue(leaderboardContent, new TypeReference<List<Leaderboard>>() {
                    });
                } else {
                    leaderboard = new ArrayList<>();
                }
                leaderboard.removeIf(e -> e.getHandle().equals(handle));
                leaderboard.add(new Leaderboard(handle, score, LocalDateTime.now()));
                String updatedContent = objectMapper.writerWithDefaultPrettyPrinter().writeValueAsString(leaderboard);
                Files.write(Paths.get(leaderboardFile), updatedContent.getBytes());
            } catch (IOException e) {
                e.printStackTrace();
            }
        });
    }

    public static CompletableFuture<List<Leaderboard>> retrieveLeaderboardAsync() {
        return CompletableFuture.supplyAsync(() -> {
            try {
                String leaderboardFile = Paths.get(DATA_DIRECTORY, "leaderboard.json").toString();
                if (!new File(leaderboardFile).exists()) {
                    return new ArrayList<>();
                }
                String jsonString = new String(Files.readAllBytes(Paths.get(leaderboardFile)));
                return objectMapper.readValue(jsonString, new TypeReference<List<Leaderboard>>() {
                });
            } catch (IOException e) {
                e.printStackTrace();
                return new ArrayList<>();
            }
        });
    }

    private static String sanitizeHandle(String handle) {
        return handle.replaceAll("\\.\\./", "").replaceAll("\\.\\\\", "");
    }

    private static boolean isPathWithinDirectory(String filePath, String directory) {
        try {
            String fullPath = new File(filePath).getCanonicalPath();
            String directoryPath = new File(directory).getCanonicalPath();
            return fullPath.startsWith(directoryPath);
        } catch (IOException e) {
            e.printStackTrace();
            return false;
        }
    }
}