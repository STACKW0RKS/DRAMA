@Front-End
Feature: Navigate To Website And Take Screenshot
    AS A FRONT-END TEST AUTOMATION ENGINEER
    I SHOULD BE ABLE TO NAVIGATE TO A WEBSITE PASSED AS A PARAMETER


    Scenario Template:   [01/01] Navigate To Website And Take Screenshot
        When    I NAVIGATE TO "<WEBSITE>"
        Then    I CONFIRM THAT THE PAGE TITLE CONTAINS "<TITLE>"
        And     I TAKE A SCREENSHOT

        Examples: 
            | WEBSITE                       | TITLE         |
            | https://github.com/           | GitHub        |
            | https://news.ycombinator.com/ | Hacker News   |
