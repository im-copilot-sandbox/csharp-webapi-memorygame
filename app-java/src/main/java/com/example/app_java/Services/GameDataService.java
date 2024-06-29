package com.example.app_java.Services;

import com.example.app_java.Models.Game;
import com.example.app_java.Models.Leaderboard;
import com.fasterxml.jackson.core.type.TypeReference;
import com.fasterxml.jackson.databind.ObjectMapper;
import com.fasterxml.jackson.datatype.jsr310.JavaTimeModule;

import org.springframework.stereotype.Service;
import org.springframework.util.StringUtils;

import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Service
public class GameDataService {
    private final Path dataDirectory = Paths.get("data");
    private final ObjectMapper objectMapper = new ObjectMapper();

    public GameDataService() throws IOException {
        Files.createDirectories(dataDirectory);
        objectMapper.registerModule(new JavaTimeModule());
    }

    public void saveGameAsync(Game game, String handle) throws IOException {
        String sanitizedHandle = sanitizeHandle(handle);
        Path filePath = dataDirectory.resolve(sanitizedHandle + ".json");
        if (!isPathWithinDirectory(filePath, dataDirectory)) {
            throw new IllegalArgumentException("Invalid file path.");
        }
        String jsonString = objectMapper.writeValueAsString(game);
        Files.writeString(filePath, jsonString);
    }

    public Optional<Game> retrieveGameAsync(String handle) throws IOException {
        String sanitizedHandle = sanitizeHandle(handle);
        Path filePath = dataDirectory.resolve(sanitizedHandle + ".json");
        if (Files.exists(filePath) && isPathWithinDirectory(filePath, dataDirectory)) {
            String jsonString = Files.readString(filePath);
            return Optional.ofNullable(objectMapper.readValue(jsonString, Game.class));
        }
        return Optional.empty();
    }

    public void saveLeaderboardEntryAsync(String handle, int score) throws IOException {
        Path leaderboardFile = dataDirectory.resolve("leaderboard.json");
        List<Leaderboard> leaderboard;
        if (Files.exists(leaderboardFile)) {
            String content = Files.readString(leaderboardFile);
            leaderboard = objectMapper.readValue(content, new TypeReference<List<Leaderboard>>() {
            });
        } else {
            leaderboard = new ArrayList<>();
        }
        leaderboard.removeIf(e -> e.getHandle().equals(handle));
        leaderboard.add(new Leaderboard(handle, score, LocalDateTime.now()));
        String updatedContent = objectMapper.writerWithDefaultPrettyPrinter().writeValueAsString(leaderboard);
        Files.writeString(leaderboardFile, updatedContent);
    }

    public List<Leaderboard> retrieveLeaderboardAsync() throws IOException {
        Path leaderboardFile = dataDirectory.resolve("leaderboard.json");
        if (Files.exists(leaderboardFile)) {
            String jsonString = Files.readString(leaderboardFile);
            return objectMapper.readValue(jsonString, new TypeReference<List<Leaderboard>>() {
            });
        }
        return new ArrayList<>();
    }

    private String sanitizeHandle(String handle) {
        return StringUtils.cleanPath(handle).replace("../", "").replace("..\\", "");
    }

    private boolean isPathWithinDirectory(Path filePath, Path directory) throws IOException {
        Path normalizedFile = filePath.toRealPath().normalize();
        Path normalizedDirectory = directory.toRealPath().normalize();
        return normalizedFile.startsWith(normalizedDirectory);
    }
}
