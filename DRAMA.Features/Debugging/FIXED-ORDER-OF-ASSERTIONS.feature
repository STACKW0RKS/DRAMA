﻿@DEBUG
Feature: Fixed Order Of Assertions
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I EXPECT THAT THE "IMPLICIT FAIL" SCENARIO WILL BE SKIPPED
    FOLLOWING THE FAILURE OF THE "EXPLICIT FAIL" SCENARIO
    IF "Stop Feature At First Error" IS SET TO "TRUE" IN THE CONFIGURATION FILE


    # The acceptance criteria for this fixture is that the result of each scenario matches the outcome specified in the name of the scenario, apart from the "IMPLICIT FAIL" scenario (more details below).
    # The "IMPLICIT FAIL" scenario will either fail or be skipped, depending on whether "Stop Feature At First Error" is enabled or not in the configuration file.
    # This fixture only exists for debugging purposes, and some of these tests are expected to fail, so don't include them in a test run which is not for debugging purposes.


    Scenario:   [01/07] IGNORE
        Given   I IGNORE THE STEP
        When    I IGNORE THE STEP
        Then    I IGNORE THE STEP


    Scenario:   [02/07] INCONCLUSIVE
        Given   I SET THE STEP INCONCLUSIVE
        When    I SET THE STEP INCONCLUSIVE
        Then    I SET THE STEP INCONCLUSIVE


    Scenario:   [03/07] EXPLICIT PASS
        Given   I EXPLICITLY PASS THE STEP
        When    I EXPLICITLY PASS THE STEP
        Then    I EXPLICITLY PASS THE STEP


    Scenario:   [04/07] IMPLICIT PASS
        Given   I IMPLICITLY PASS THE STEP
        When    I IMPLICITLY PASS THE STEP
        Then    I IMPLICITLY PASS THE STEP


    Scenario:   [05/07] WARN
        Given   I RAISE A WARNING FOR THE STEP
        When    I RAISE A WARNING FOR THE STEP
        Then    I RAISE A WARNING FOR THE STEP


    Scenario:   [06/07] EXPLICIT FAIL
        Given   I EXPLICITLY FAIL THE STEP
        When    I EXPLICITLY FAIL THE STEP
        Then    I EXPLICITLY FAIL THE STEP


    Scenario:   [07/07] IMPLICIT FAIL
        Given   I IMPLICITLY FAIL THE STEP
        When    I IMPLICITLY FAIL THE STEP
        Then    I IMPLICITLY FAIL THE STEP
