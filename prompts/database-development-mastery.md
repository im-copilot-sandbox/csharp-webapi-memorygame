# Database Development Mastery Workshop

In this workshop we will be exploring various datbase related patterns and tasks and see
how Copilot can help us.

This workshop consists of the following modules:

-  **Module 1: Introduction to Database Design**
-  **Module 2: SQL vs NoSQL Databases**
-  **Module 3: Designing and Optimizing Schemas**
-  **Module 4: Writing and Optimizing Queries**
-  **Module 5: Creating DB code from exisiting C# code**
-  **Module 6: Querying the DB with copilot**
-  **Module 7: Using copilot to implement Entity Framework**


## How to Use This Guide

Each module builds on the previous one, so it is important to complete them in order.

<details>
<summary><strong>Show More</strong></summary>
<blockquote>

Each module contains a series of prompts that you can use to interact with Copilot. The
prompts are meant to guide you through the workshop and help you understand how Copilot
can assist you in building a REST API backend.

Please take a moment to familiarize yourself with the different conventions used in this
guide.

### Prompts

```markdown
This is a prompt
```

Prompts are meant to be typed into the Copilot prompt bar. They are used to guide Copilot
to generate code or provide information.

```markdown
This is a prompt with a #file:filename.py directive
```

The prompts with a **#** directive need to be typed directly into the Copilot prompt bar.
For all other prompts, you can copy and paste them into the prompt bar.

### Hints

> This is a hint or a tip

Throughout the guide, you will find hints and tips to provide more insights into getting
the most out of Copilot. Feel free to experiment with the hints and tips to see how they
can help you.

### Expected Output

<details>
<summary><strong>View Response</strong></summary>
<blockquote>

Code suggestions or sample outputs from Copilot can be viewed here for reference\. You can
expand and collapse the content as needed by clicking the _View Response_ header\.

</blockquote>
</details>

Copilot will generate code based on the prompts you provide. Since there are many ways to
solving a coding challenge the suggested code may vary slightly based on the context
provided by the prompt, your previous interactions with Copilot, and the comments in the
codebase.

To help you along, we have provided some possible outputs for some of the prompts. This
will give you an idea of what to expect when you run the prompt. We encourage you to
experiment with the prompts and see how Copilot can help you.

### Checkpoints

Throughout the workshop, you will find checkpoints that summarize the code you have
generated so far. These checkpoints are meant to help you keep track of your progress and
ensure that you have completed the tasks correctly.

If your experimentations with Copilot lead you to a different solution, feel free to use
the code in the checkpoints as a reference to ensure you are on the right track.

### Skeleton Project

We've provided you with a skeleton project to help you get up and running.

Each file in the skeleton project contains comments which will guide you through the

</blockquote>
</details>
<!-- /include -->

## Module 1: Introduction to Database Design

In this module, we will explore the basics of database design and how to create a database
schema. We'll use Copilot to explore different database concepts and see how it can help
us generate the necessary code.

### Database Basics

First let's start with the basics.

```markdown
Give me a brief overview of database design
```

<details>
<summary><strong>View Response</strong></summary>

Database design is a critical process in the development of database systems that ensures
data is stored efficiently and can be retrieved and manipulated effectively. It involves
defining the structure of a database, including its tables, fields, relationships,
indexes, and constraints, to support the data requirements of an application or system.
Here's a brief overview of the key aspects of database design:

### 1. Requirements Analysis

-  **Understanding Data Needs**: Identifying the types of data to be stored and the
   relationships between different data entities.
-  **Use Cases and Queries**: Understanding how the data will be accessed and manipulated,
   including common queries and transactions.

### 2. Data Modeling

-  **Entity-Relationship Diagrams (ERDs)**: Visual representations of the data entities,
   their attributes, and relationships.
-  **Normalization**: Process of organizing data to reduce redundancy and improve data
   integrity. Involves dividing large tables into smaller, related tables and defining
   relationships among them.

### 3. Schema Definition

-  **Tables and Columns**: Defining the tables (or collections in NoSQL databases) and
   specifying the columns (or fields) along with their data types.
-  **Primary and Foreign Keys**: Identifying unique identifiers for each table (primary
   keys) and establishing relationships between tables through foreign keys.

### 4. Constraints and Indexes

-  **Constraints**: Rules that enforce data integrity and consistency, such as NOT NULL,
   UNIQUE, CHECK, and referential integrity constraints.
-  **Indexes**: Structures that improve the speed of data retrieval operations on a
   database table at the cost of additional writes and storage space.

### 5. Optimization

-  **Performance Tuning**: Adjusting the schema and queries to improve performance,
   including optimizing indexes and query patterns.
-  **Scalability Considerations**: Designing the database to handle growth, either by
   scaling up (more powerful server) or scaling out (distributed database systems).

### 6. Security and Compliance

-  **Access Controls**: Defining who can access what data and what operations they can
   perform.
-  **Data Protection**: Ensuring data is encrypted and secure, both at rest and in
   transit.

### 7. Documentation and Maintenance

-  **Documentation**: Keeping detailed documentation of the database schema and any
   changes made over time.
-  **Regular Reviews**: Periodically reviewing the database design to accommodate new
   requirements or improve performance and scalability.

Effective database design is foundational to building applications that are performant,
reliable, and scalable. It requires a thorough understanding of both the data being
managed and the ways in which it will be used, as well as ongoing maintenance to adapt to
changing requirements and technologies.

</details>

```markdown
what are some common types of databases?
```

<details>
<summary><strong>View Response</strong></summary>

Databases are essential for storing, retrieving, managing, and manipulating data. They
come in various types to serve different needs, ranging from storing simple lists to
managing complex, interconnected data across distributed systems. Here are some common
types of databases:

### 1. Relational Databases (RDBMS)

-  **Description**: Store data in tables with rows and columns, where each row represents
   a record and each column represents a data field. Relational databases use Structured
   Query Language (SQL) for data manipulation and queries.
-  **Examples**: PostgreSQL, MySQL, Oracle, SQL Server.

### 2. NoSQL Databases

-  **Description**: Designed to handle a wide variety of data models, including document,
   key-value, wide-column, and graph formats. They are known for their flexibility,
   scalability, and performance in handling large volumes of unstructured data.
-  **Examples**:
   -  **Document**: MongoDB, CouchDB.
   -  **Key-Value**: Redis, DynamoDB.
   -  **Wide-Column**: Cassandra, HBase.
   -  **Graph**: Neo4j, Amazon Neptune.

### 3. In-Memory Databases

-  **Description**: Store data in a computer's main memory (RAM) instead of on disk, which
   allows for faster data retrieval and manipulation. They are often used for applications
   requiring high-speed transactions and real-time analytics.
-  **Examples**: Redis, Memcached.

### 4. Distributed Databases

-  **Description**: Consist of a collection of databases that are spread across multiple
   physical locations, interconnected via a network. They are designed to provide high
   availability, fault tolerance, and scalability.
-  **Examples**: Cassandra.

### 5. Object-Oriented Databases

-  **Description**: Store data in the form of objects, as used in object-oriented
   programming. These databases are designed to support the storage of complex data
   structures and relationships.
-  **Examples**: db4o, ObjectDB.

### 6. Time Series Databases

-  **Description**: Optimized for storing and querying sequences of data points over time.
   They are commonly used in monitoring systems, financial markets, and IoT applications
   for tracking changes over time.
-  **Examples**: InfluxDB, TimescaleDB.

### 7. Graph Databases

-  **Description**: Designed to store data whose relations are best represented as a
   graph. They are ideal for applications that require the exploration of relationships
   between data points, such as social networks or recommendation systems.
-  **Examples**: Neo4j, Amazon Neptune.

### 8. Data Warehouses

-  **Description**: Specialized relational databases optimized for query and analysis
   rather than transaction processing. They are used for storing large volumes of
   historical data and enabling business intelligence and analytics.
-  **Examples**: Amazon Redshift, Google BigQuery, Snowflake.

Each type of database has its strengths and use cases, and the choice of database often
depends on the specific requirements of an application, including the nature of the data,
the scale of the system, and the types of queries that need to be supported.

</details>

```markdown
What are some common use cases for each type?
```

<details>
<summary><strong>View Response</strong></summary>

Each type of database is designed to serve specific use cases based on its data model,
performance characteristics, and scalability features. Here are common use cases for each
type of database:

### 1. Relational Databases (RDBMS)

-  **Use Cases**:
   -  Traditional business applications (CRM, ERP)
   -  E-commerce platforms
   -  Any system requiring complex transactions and relational data integrity

### 2. NoSQL Databases

-  **Document**:
   -  Content management systems
   -  Catalogs and e-commerce applications
-  **Key-Value**:
   -  Session storage for web applications
   -  Caching systems
-  **Wide-Column**:
   -  Real-time analytics and monitoring
   -  High-scale industrial IoT applications
-  **Graph**:
   -  Social networks
   -  Recommendation engines

### 3. In-Memory Databases

-  **Use Cases**:
   -  Real-time analytics and processing
   -  Caching for web applications
   -  Session stores

### 4. Distributed Databases

-  **Use Cases**:
   -  Applications requiring global distribution and high availability
   -  Large-scale online transaction processing (OLTP) systems
   -  Systems requiring horizontal scalability

### 5. Object-Oriented Databases

-  **Use Cases**:
   -  Applications with complex data models, such as CAD/CAM
   -  Multimedia databases
   -  Systems requiring tight integration with object-oriented programming languages

### 6. Time Series Databases

-  **Use Cases**:
   -  Monitoring and IoT sensor data
   -  Financial trading and stock market data
   -  Metrics and analytics for DevOps

### 7. Graph Databases

-  **Use Cases**:
   -  Network and IT operations (network topology)
   -  Fraud detection and prevention
   -  Knowledge graphs and semantic search

### 8. Data Warehouses

-  **Use Cases**:
   -  Business intelligence (BI) and reporting
   -  Data analytics and historical analysis
   -  Large-scale data storage and querying for decision support

Each database type excels in scenarios that match its inherent strengths, such as the
ability to handle specific data models, performance and scalability requirements, and the
complexity of data relationships. The choice of database often depends on the specific
needs of the application, including the nature of the data, the expected scale, and the
types of operations that will be performed on the data.

</details>

One of the key decisions in database design is choosing the right database for your
application. Let's explore how you can make that decision.

```markdown
how do I select the right database for my application?
```

<details>
<summary><strong>View Response</strong></summary>

Selecting the right database for your application involves considering several factors.
Here's a step-by-step plan to guide you through the process:

1. **Define Application Requirements:**

   -  **Data Structure:** Determine if your data is structured, semi-structured, or
      unstructured.
   -  **Data Volume:** Estimate the size of your data now and in the future.
   -  **Transactions:** Understand the nature of the transactions (Read-heavy,
      write-heavy, or balanced).
   -  **Consistency:** Decide on the level of consistency required (strong, eventual,
      etc.).
   -  **Availability:** Determine if your application needs to be available without
      interruption.
   -  **Latency:** Identify acceptable response times for data retrieval and storage.

2. **Analyze Database Types:**

   -  **Relational Databases (SQL):** Best for structured data with complex queries and
      transactions. Examples: PostgreSQL, MySQL, Oracle.
   -  **NoSQL Databases:** Good for semi-structured or unstructured data, scalability, and
      flexibility. Types include:
      -  **Document Stores:** MongoDB, Couchbase.
      -  **Key-Value Stores:** Redis, DynamoDB.
      -  **Wide-Column Stores:** Cassandra, HBase.
      -  **Graph Databases:** Neo4j, Amazon Neptune.

3. **Consider Scalability:**

   -  **Vertical Scaling:** Can the database handle increased load by adding more
      resources to a single node?
   -  **Horizontal Scaling:** Can it scale out across multiple nodes?

4. **Evaluate Performance:**

   -  **Benchmarking:** Look for benchmarks comparing database performance for use cases
      similar to yours.
   -  **Testing:** If possible, conduct your own tests with real-world scenarios.

5. **Assess Maturity and Community Support:**

   -  **Maturity:** A mature database is likely to have fewer bugs and better stability.
   -  **Community:** A strong community can provide support, plugins, and tools.

6. **Review Licensing and Costs:**

   -  **Open Source vs. Commercial:** Determine if an open-source database meets your
      needs or if a commercial one offers critical features.
   -  **Operational Costs:** Consider not just the licensing costs but also the
      operational costs including hardware, maintenance, and required expertise.

7. **Security Features:**

   -  **Encryption:** Does it support data encryption at rest and in transit?
   -  **Access Control:** Evaluate the mechanisms for controlling access to the data.

8. **Compliance and Data Governance:**

   -  **Regulatory Compliance:** Ensure the database can help you comply with relevant
      regulations (GDPR, HIPAA, etc.).
   -  **Data Governance:** Look for features that help with data quality, auditing, and
      lineage.

9. **Backup and Recovery:**

   -  **Backup Features:** Assess the ease and flexibility of backup procedures.
   -  **Recovery Solutions:** Ensure there are robust mechanisms for data recovery.

10.   **Future-proofing:**
      -  **Flexibility:** Choose a database that can adapt to future changes in technology
         and your application's needs.
      -  **Ecosystem:** Consider if the database integrates well with other tools and
         services you use or plan to use.

After considering these factors, narrow down your options and potentially select a primary
and a secondary database if your application requires a polyglot persistence strategy
(using multiple databases for different needs).

</details>

```markdown
what are some popular sql databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
what about nosql ones?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

We'll look at SQL vs NoSQL databases in the next module.

```markdown
Outline the steps involved in designing a database. Highlight the importance of each step
in the overall design process.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
Explain key concepts in database design such as entities, relationships, primary keys, and
foreign keys.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Copilot can also help us pick the right tools for working with databases.

```markdown
Identify some common tools used in database design and describe how they assist in
creating efficient database schemas.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Only care about open-source tools?

> Copilot remembers the context from previous prompts, so you can build on that to refine
> your queries.

```markdown
Only list open source tools
```

Hopefully by now we see how Copilot knows alot about databases.

In later modules we will look at how it can help us write queries and optimize our
database schemas.

## Module 2: SQL vs NoSQL Databases

From the previous module, we learned about the different types of databases and some of
the common use cases for each type.

In this module, we will explore two of the most popular database categories: SQL
(relational) and NoSQL (non-relational) databases. We'll compare their key
characteristics, use cases, and advantages and disadvantages.

```markdown
Describe the differences between relational and NoSQL databases. Provide examples of use
cases for each.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

### SQL Databases

```markdown
What are the key characteristics of SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
list some use cases that are ideal for SQL databases
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
how about some use cases that are not ideal for SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Or, another way of asking the same question:

```markdown
List scenarios where SQL databases may be preferred over NoSQL databases.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
what are some considerations when moving between SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Copilot also knows alot about specific SQL databases including their unique features and
best practices.

```markdown
list key features and differences between PostgreSQL and MySQL including use cases where
one may be preferred over the other.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
Guide me through the process of setting up a PostgreSQL database.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Copilot can also help us when migrating from one database to another.

```markdown
How can I migrate data from MySQL to PostgreSQL?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Let's look at more advanced topics.

```markdown
Explain the concept of transactions in SQL databases and show how to implement a simple
transaction in PostgreSQL.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

How about scaling our database?

```markdown
What are some common strategies for scaling PostgreSQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

And let's not forget MySQL.

```markdown
What are some common strategies for scaling MySQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

### NoSQL Databases

```markdown
what are some popular NoSQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

> Copilot can help us learn new concepts at the level and rate that we are comfortable
> with. Simply tell it what you want to learn and how deep you want to go.

```markdown
how can I learn more about NoSQL databases if I'm new to the topic but experienced in SQL
databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
Suggest a NoSQL database that is relatively easy to learn.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
What are some commone design patterns for NoSQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Finding ourselves moving between different NoSQL databases?

```markdown
What are the key differences between MongoDB and Cassandra?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Or,

```markdown
What should I consider when migrationg from MongoDB to DynamoDB?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

In this module we looked at the differences between SQL and NoSQL. Feel free to experiment
with Copilot to learn more about other kinds of databases.

Next, we will look at how to design and optimize schemas for both types of databases.

## Module 3: Designing and Optimizing Schemas

```markdown
Explain the differences in data modeling approaches between SQL and NoSQL databases with
examples.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

### SQL Databases

```markdown
what is data normalization and why is it important in SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
What are some common normalization forms in SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
What are some some challenges and pitfalls to avoid when normalizing SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
What is data denormalization and when is it appropriate to use in SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Need to optimize your SQL database schema for particular usage patterns?

```markdown
How can I optimize a SQL database schema for read-heavy workloads?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

And the flip side:

```markdown
How can I optimize a SQL database schema for write-heavy workloads?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
What are some common indexing strategies for SQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

Let's get started with designing a schema.

```markdown
Analyze a simple case study to identify entities, relationships, and key attributes for a
given scenario for SQL database for an online retail store.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

ADD excercises here

### NoSQL Databases

```markdown
What are some common data modeling patterns in NoSQL databases?
```

<details>
<summary><strong>View Response</strong></summary>

</details>

## Module 4: Writing and Optimizing Queries

```markdown
teach me the basics of SQL language
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
Demonstrate how to perform CRUD operations on the product collection in MongoDB.
```

<details>
<summary><strong>View Response</strong></summary>

</details>

```markdown
Compare how to perform joins and aggregations in SQL databases (using JOINs) versus NoSQL
databases (like MongoDB's aggregation pipeline).
```

<details>
<summary><strong>View Response</strong></summary>

</details>

## Module 5: Creating DB code from exisiting C# code

Open a new chat and follow the prompts

(attach the model files)


```Generate SQL DDL scripts mapping the properties of Card, Game and Leaderboard classes, create db columns for each class property. For the Game class, create another table with one to many mapping from Cards property to the related table for card class. 
```

```
Generate SQL DDL scripts mapping the properties of json files in data folder. 

```

```
create a sql with a total of 200 records of mock data for all those tables. the data should be random but consistent
```

(in case it don't generate a loop for 200 records)
```
can you add a loop to those to make it 200 records?
```


## Module 6: Querying the DB with copilot
Open db-queries.sql

```
-- Select all data from the leaderboard table ordered by score in descending order
```

```
-- Select all data from the leaderboard table ordered by last played in ascending order

```

```
-- Select all data from the leaderboard table ordered by score in descending order and last played in ascending order

```

```
-- Select the top 10 data from the leaderboard table ordered by score in descending order

```

```
-- Select the top 5 scores from the leaderboard table that were played after a specific date

```

```
-- Select the top 5 scores from the leaderboard table that were played before a specific date

```

```
-- Select the top 5 scores from the leaderboard table that were played after a specific date and have a score greater than a specific value

```


```
-- create a trigger that updates the last played date when a new score is inserted

```

```
-- create a trigger that updates the last played date when a score is updated

```

```
-- insert a new score into the leaderboard table without specifying the last played date

```

```
-- update the score of an existing player in the leaderboard table without specifying the last played date
```

```-- Make LastPlayed accept nulls
```
```-- create a stored procedure for the insert above
```
```-- create a stored procedure for the update above
```
```-- alter the procedure above return the id of the inserted row
```
```-- alter the procedure above return the id of the updated row
```

```
-- call the InsertScore procedure above
```

```
-- select the average score from the leaderboard table
```

```
-- create a function to select the average score from the leaderboard table
```

```-- select all cards
```

```-- select all cards that are flipped
```

```-- select all cards for a specific game, 
```

```-- include all data from the Cards table and order by type in ascending order
```

```-- select all games with more than 10 turns taken
```

```
-- create an idex on the games table for the column Handle
```

```
-- create an index on the cards table for the column Type  
```

```
-- create an index on the gamecards table for the column GameId
```

```
-- create an index on the leaderboard table for the column Handle and order by LastPlayed in descending order
```

```
-- create an index on the leaderboard table for the column Score
```

```
-- create an index on the leaderboard table for the column LastPlayed
```

```
-- create an index on the leaderboard table for the column LastPlayed and order by Score in descending order
```

```
-- create an index on the leaderboard table for the column LastPlayed and order by Handle in ascending order
```

## Module 7: Using copilot to implement Entity Framework

At copilot chat:
```
based on the database defined at db/db-setup.sql modify Services/GameData.cs to use the database instead of current json files. Use Entity Framework Core.
```

Copilot will provide several steps to setup install and use entity framework. Follow the steps, or select the appropriate files and click for copilot to do it.

In one of the test runs I got a The EF Context class being created on the root folder instead of app folder, so I needed to move it.

Also, in another pass there was a reference to a missing property, clicking on "Fix using copilot" resolved it (but I needed to manually copy the code as it was not able to apply in another file). I needed to do it with different properties. In most instances it fixed it, in a couple I was able to fix with the information it provided.


## Module 8: Using copilot with NoSQL

Make sure you have mongodb setup on the environment

At chat, make sure to attach the entire app as context and type:
```
migrate this app to use mongodb instead of sql server
```

There will be a lot of steps, I was able to have a copilot open all the files and do the edits, except for the models where it provided one example change that needed to be replicated (I did manually in 4 files), I fixed a few issues with "Fix using copilot" menu.

In my case I wanted more details in one of the steps: 
```
can you detail what to do for step 7: Update Controllers: Update your controllers to use the new service methods. Using existing RoutesController.
```

It referenced an unexistent class, LeaderboardData, I asked:
```
LeaderboardData doesn't exist, create for me.
```

After that app is running on MongoDB

```
Create a new file inserting 200 records on MongoDB for Cards, Games, Leaderboards. Do it using the MongoDBCollection.
```

Open the create file. add new method and start coding with comments:
 
``` 
            // Select all data from the leaderboard table ordered by score in descending order
```

```
            // Select all data from the leaderboard ordered by last played in ascending order
```

```
            // Select all data from the leaderboard ordered by score in descending order and last played in ascending order
```


```
            // Select all data from the leaderboard where the score is greater than 500
```


```
            // Select all data from the leaderboard where the score is greater than 500 and the last played date is greater than or equal to 2020-01-01
```


```
            // Select the top 10 data from the leaderboard ordered by score in descending order
```


```
            // Select the top 5 scores from the leaderboard that were played after a specific date

```


```
            // Select the top 5 scores from the leaderboard that were played before a specific date
```


```
            // Select the top 5 scores from the leaderboard that were played after a specific date and have a score greater than a specific value
```


```
            // Select the top 5 scores from the leaderboard that were played before a specific date and have a score greater than a specific value
```


```
            //Select the top 5 scores from the leaderboard that were played after a specific date and have a score greater than a specific value, ordered by last played date in ascending order
```


```
            // insert a new score into the leaderboard without specifying the last played date
```


```
            // insert a new score into the leaderboard with a specific last played date
```


```
            // update the score of an existing entry in the leaderboard
```


```
            // update the last played date of an existing entry in the leaderboard
```

```
            // delete an entry from the leaderboard
```

```
            // delete all entries from the leaderboard
```

```
            // delete all entries from the leaderboard where the score is less than 500
```

```
            // delete all entries from the leaderboard where the last played date is before a specific date
```

```
            // select the average score from the leaderboard
```

```
            // select the average score from the leaderboard for entries with a score greater than 500    
```

```
            // select all cards
```

```
            // select all games
```

```
            // select all cards that are flipped
```

```
            // select all cards for a specific game
```

```
            // select all games for a specific player
```

```
            // select all data from the Cards and order by type in ascending order
```

```
            // select all games with more than 10 turns taken
```

```
            // create an index on the games for the field Handle
```

```
            // create a compound index on the games for the fields Handle and TurnsTaken
```

```
            // create a unique index on the games for the field Handle
```

```
            // create a text index on the games for the field Handle
```

```
            // create a hashed index on the games for the field Handle
```

```
            // create a sparse index on the games for the field Handle
```

```
            // create an index on the cards for the field Id
```

```
            // create a unique index on the cards for the field Id
```

```
            // create a compound index on the cards for the fields Id and Flipped
```

```
            // create an index for cards  field Type in ascending order
```

```
            // create an index on the leaderboard for Handle and order by LastPlayed in descending order
```

```
            // create an index on the leaderboard for Score in descending order
```

```
            // create an index on the leaderboard for LastPlayed
```

```
            // create an index for leaderboard for LastPlayed and order by Score in descending order
```

```
            // create an index for leaderboard for  LastPlayed and order by Handle in ascending order
```
