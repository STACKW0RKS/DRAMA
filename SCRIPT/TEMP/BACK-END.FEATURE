@Back-End @DEBUG
Feature: Write To And Read From A SQLite Database
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I SHOULD BE ABLE TO WRITE TO AND READ FROM A SQLITE DATABASE

    Background:
        Given   THE SQLITE DATABASE DOES NOT EXIST

    Scenario Template: [01/01] Write And Read Data To A Database
        Given   I BOOTSTRAP AND CONNECT TO THE DATABASE
        And     I CONFIRM THAT THE CONNECTION IS ALIVE
        When    I CREATE A "ENTRIES" TABLE
        And     I INSERT "<LANGUAGE>" AND "<ENTRY>" INTO THE "ENTRIES" TABLE
        Then    I CONFIRM THAT A ROW HAS BEEN CREATED IN THE "ENTRIES" TABLE
        And     I CLOSE THE DATABASE CONNECTION

        Examples:

            | LANGUAGE  | ENTRY          |
            | English   | Hello World!   |
            | French    | Bonjour Monde! |
            | German    | Hallo Welt!    |
            | Italian   | Ciao Mondo!    |
            | Spanish   | ¡Hola Mundo!   |
