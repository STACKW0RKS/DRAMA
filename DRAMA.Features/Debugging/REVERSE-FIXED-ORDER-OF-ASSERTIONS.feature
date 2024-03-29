﻿@DEBUG
Feature: Reverse Fixed Order Of Assertions
    AS A PROGRAMMER WHO DILIGENTLY DEBUGS THEIR CODE
    I EXPECT THAT ALL BUT THE "IMPLICIT FAIL" SCENARIO ARE SKIPPED
    FOLLOWING THE FAILURE OF THE "IMPLICIT FAIL" SCENARIO
    IF "Stop Feature At First Error" IS SET TO "TRUE" IN THE CONFIGURATION FILE


    # The acceptance criteria for this fixture is that the "IMPLICIT FAIL" scenario will fail.
    # All the other scenarios will either be skipped or match the outcome specified in the name of the scenario, depending on whether "Stop Feature At First Error" is enabled or not in the configuration file.
    # This fixture only exists for debugging purposes, and some of these tests are expected to fail, so don't include them in a test run which is not for debugging purposes.


    Scenario:   [01/07] IMPLICIT FAIL
        Given   I IMPLICITLY FAIL THE STEP
        When    I IMPLICITLY FAIL THE STEP
        Then    I IMPLICITLY FAIL THE STEP


    Scenario:   [02/07] EXPLICIT FAIL
        Given   I EXPLICITLY FAIL THE STEP
        When    I EXPLICITLY FAIL THE STEP
        Then    I EXPLICITLY FAIL THE STEP


    Scenario:   [03/07] WARN
        Given   I RAISE A WARNING FOR THE STEP
        When    I RAISE A WARNING FOR THE STEP
        Then    I RAISE A WARNING FOR THE STEP


    Scenario:   [04/07] IMPLICIT PASS
        Given   I IMPLICITLY PASS THE STEP
        When    I IMPLICITLY PASS THE STEP
        Then    I IMPLICITLY PASS THE STEP


    Scenario:   [05/07] EXPLICIT PASS
        Given   I EXPLICITLY PASS THE STEP
        When    I EXPLICITLY PASS THE STEP
        Then    I EXPLICITLY PASS THE STEP


    Scenario:   [06/07] INCONCLUSIVE
        Given   I SET THE STEP INCONCLUSIVE
        When    I SET THE STEP INCONCLUSIVE
        Then    I SET THE STEP INCONCLUSIVE


    Scenario:   [07/07] IGNORE
        Given   I IGNORE THE STEP
        When    I IGNORE THE STEP
        Then    I IGNORE THE STEP
