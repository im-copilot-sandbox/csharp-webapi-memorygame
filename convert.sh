#!/bin/bash

# Define source and destination directories
SOURCE_DIR="./app"
DEST_DIR="./app-java/src/main/java/com/example/app_java"

# Create target directories if they do not exist
mkdir -p "$DEST_DIR/Controllers"
mkdir -p "$DEST_DIR/Models"
mkdir -p "$DEST_DIR/Services"

# Copy and rename files from Controllers, Models, and Services directories
# Changing the extension from .cs to .java
find "$SOURCE_DIR/Controllers" -type f -name "*.cs" | while read FILE; do
    cp "$FILE" "${DEST_DIR}/Controllers/$(basename "${FILE%.cs}.java")"
done

find "$SOURCE_DIR/Models" -type f -name "*.cs" | while read FILE; do
    cp "$FILE" "${DEST_DIR}/Models/$(basename "${FILE%.cs}.java")"
done

find "$SOURCE_DIR/Services" -type f -name "*.cs" | while read FILE; do
    cp "$FILE" "${DEST_DIR}/Services/$(basename "${FILE%.cs}.java")"
done

echo "Files have been copied and renamed successfully."