@DEBUG
Feature: Reverse Fixed Order Of Assertions
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I EXPECT THAT ALL BUT THE FIRST STEP IN THIS SEQUENCE ARE SKIPPED


    Scenario:   [01/01] Reverse Fixed Order Of Assertions
        When    I FAIL THE STEP
        When    I WARN THE STEP
        When    I PASS THE STEP
        When    I IGNORE THE STEP
