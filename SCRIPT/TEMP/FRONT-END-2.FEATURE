@Front-End @DEBUG
Feature: Navigate To Hacker News
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I SHOULD BE ABLE TO NAVIGATE TO A WEBSITE PASSED AS A PARAMETER


    Scenario:   [01/02] Navigate To Website
        When    I RANDOMLY EITHER PASS OR FAIL THE STEP
        And     I NAVIGATE TO "https://news.ycombinator.com/"
        Then    I CONFIRM THAT THE PAGE TITLE CONTAINS "Hacker News"

    Scenario:   [02/02] Take A Screenshot
        Then    I TAKE A SCREENSHOT
        And     I RANDOMLY EITHER PASS OR FAIL THE STEP
