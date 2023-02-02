@DEBUG
Feature: Fixed Order Of Assertions
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I EXPECT THAT NONE OF THE STEPS IN THIS SEQUENCE ARE SKIPPED



    Scenario:   [01/05] IGNORE
        Given   I IGNORE THE STEP
        When    I IGNORE THE STEP
        Then    I IGNORE THE STEP


    Scenario:   [02/05] INCONCLUSIVE
        Given   I SET THE STEP INCONCLUSIVE
        When    I SET THE STEP INCONCLUSIVE
        Then    I SET THE STEP INCONCLUSIVE


    Scenario:   [03/05] PASS
        Given   I PASS THE STEP
        When    I PASS THE STEP
        Then    I PASS THE STEP


    Scenario:   [04/05] WARN
        Given   I RAISE A WARNING FOR THE STEP
        When    I RAISE A WARNING FOR THE STEP
        Then    I RAISE A WARNING FOR THE STEP


    Scenario:   [05/05] FAIL
        Given   I FAIL THE STEP
        When    I FAIL THE STEP
        Then    I FAIL THE STEP
