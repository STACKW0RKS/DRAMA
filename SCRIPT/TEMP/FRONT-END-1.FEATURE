@Front-End @DEBUG
Feature: Navigate To GitHub
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I SHOULD BE ABLE TO NAVIGATE TO A WEBSITE PASSED AS A PARAMETER


    Scenario:   [01/02] Navigate To Website
        When    I RANDOMLY EITHER PASS OR FAIL THE STEP
        And     I NAVIGATE TO "https://github.com/"
        Then    I CONFIRM THAT THE PAGE TITLE CONTAINS "GitHub"

    Scenario:   [02/02] Take A Screenshot
        Then    I TAKE A SCREENSHOT
        And     I RANDOMLY EITHER PASS OR FAIL THE STEP
