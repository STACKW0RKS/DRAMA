@DEBUG
Feature: Fixed Order Of Assertions
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I EXPECT THAT NONE OF THE STEPS IN THIS SEQUENCE ARE SKIPPED


    Scenario:   [01/01] Fixed Order Of Assertions
        When    I IGNORE THE STEP
        When    I PASS THE STEP
        When    I WARN THE STEP
        When    I FAIL THE STEP
