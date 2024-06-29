#!/bin/bash

# Create target directories in app-java
mkdir -p ./app-java/src/main/java/app/Controllers
mkdir -p ./app-java/src/main/java/app/Models
mkdir -p ./app-java/src/main/java/app/Services
mkdir -p ./app-java/data

# Copy and rename .cs files to .java in Controllers
for file in ./app/Controllers/*.cs; do
    cp "$file" "./app-java/src/main/java/app/Controllers/$(basename "${file%.cs}.java")"
done

# Copy and rename .cs files to .java in Models
for file in ./app/Models/*.cs; do
    cp "$file" "./app-java/src/main/java/app/Models/$(basename "${file%.cs}.java")"
done

# Copy and rename .cs files to .java in Services
for file in ./app/Services/*.cs; do
    cp "$file" "./app-java/src/main/java/app/Services/$(basename "${file%.cs}.java")"
done

# Copy JSON data files
cp ./app/data/*.json ./app-java/data/