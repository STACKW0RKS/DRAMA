# DRAMA
Full Stack Test Automation Framework

</br>

## PROJECT MAP

| PROJECT   | DESCRIPTION                                                                   |
|----------:|:------------------------------------------------------------------------------|
| DRAMA     | Main project from which the NuGet package is built.                           |
| SPOTLIGHT | Unit tests for DRAMA.                                                         |
| SCRIPT    | Sample DRAMA implementation project, part one: feature files.                 |
| REHEARSAL | Sample DRAMA implementation project, part two: step definitions, test engine. |

</br>

## FAQ
Q: Where to download Playwright images from, for my solution which integrates DRAMA?

A: The Docker image tag lists can be found at the following locations:

> Ubuntu: `https://mcr.microsoft.com/v2/playwright/tags/list`

> Ubuntu With .NET: `https://mcr.microsoft.com/v2/playwright/dotnet/tags/list`

</br>

Q: How do I run my tests in a CI/CD pipeline?

A: Either use a PowerShell script or a Bash script, along the lines of the examples below:

```powershell
# set the environment variable for the results path, which the framework will look for at runtime
[Environment]::SetEnvironmentVariable("DRAMA_RESULTS_PATH", $PSScriptRoot)
${RESULTS_PATH} = [Environment]::GetEnvironmentVariable("DRAMA_RESULTS_PATH")
Write-Host "[DEBUG] Results Path: ${RESULTS_PATH}" -ForegroundColor Yellow

# set the environment variable for the test run name, which the framework will look for at runtime
${TEST_RUN_NAME} = (Get-Date -Format "yyyy-MM-dd_HH-mm-ss")

if (![string]::IsNullOrEmpty([Environment]::GetEnvironmentVariable("DRAMA_TEST_RUN_NAME")))
{
    ${TEST_RUN_NAME} = [Environment]::GetEnvironmentVariable("DRAMA_TEST_RUN_NAME")
}

else
{
    [Environment]::SetEnvironmentVariable("DRAMA_TEST_RUN_NAME", "${TEST_RUN_NAME}")
}

Write-Host "[DEBUG] Test Run Name: ${TEST_RUN_NAME}" -ForegroundColor Yellow

# create a variable that points to the directory where the test run results will be stored
# this directory needs to be known at test run start time, so that it can be passed to the "dotnet test" command
# both the framework and MSTest artefacts will be generated at this path
${TEST_RUN_RESULTS_PATH} = [IO.Path]::Combine("${RESULTS_PATH}", "DRAMA_RESULTS", "${TEST_RUN_NAME}")
Write-Host "[DEBUG] Test Run Results Path: ${TEST_RUN_RESULTS_PATH}" -ForegroundColor Yellow

# set the environment variable for the test run filter, which the framework will look for at runtime
${TEST_CATEGORIES} = "TestCategory!=DEBUG&TestCategory!=DEMO&TestCategory!=WIP"

if (![string]::IsNullOrEmpty([Environment]::GetEnvironmentVariable("DRAMA_TEST_CATEGORIES")))
{
    ${TEST_CATEGORIES} = [Environment]::GetEnvironmentVariable("DRAMA_TEST_CATEGORIES")
}

else
{
    [Environment]::SetEnvironmentVariable("DRAMA_TEST_CATEGORIES", "${TEST_CATEGORIES}")
}

Write-Host "[DEBUG] Test Run Categories: ${TEST_CATEGORIES}" -ForegroundColor Yellow

# if an "DRAMA_PROFILE" environment variable exists, log its value to the terminal
if (![string]::IsNullOrEmpty([Environment]::GetEnvironmentVariable("DRAMA_PROFILE")))
{
    ${PROFILE} = [Environment]::GetEnvironmentVariable("DRAMA_PROFILE")
    Write-Host "[DEBUG] Test Run Profile: ${PROFILE}" -ForegroundColor Yellow
}

else { Write-Host "[DEBUG] Test Run Profile: Default" -ForegroundColor Yellow }

# start the test run
dotnet test `
    --filter "${TEST_CATEGORIES}" `
    --logger "console;verbosity=detailed" `
    --logger "trx;LogFileName=${TEST_RUN_NAME}.trx" `
    --logger "html;LogFileName=${TEST_RUN_NAME}.html" `
    --results-directory "${TEST_RUN_RESULTS_PATH}"
```

```bash
# set the environment variable for the results path, which the framework will look for at runtime
export DRAMA_RESULTS_PATH=${PWD}
echo "[DEBUG] Results Path: ${DRAMA_RESULTS_PATH}"

# set the environment variable for the test run name, which the framework will look for at runtime
TEST_RUN_NAME=$(date "+%Y-%m-%d_%H-%M-%S")

if [ ! -z "$DRAMA_TEST_RUN_NAME" ]
then
    TEST_RUN_NAME="$DRAMA_TEST_RUN_NAME"
else
    export DRAMA_TEST_RUN_NAME="$TEST_RUN_NAME"
fi

echo "[DEBUG] Test Run Name: ${DRAMA_TEST_RUN_NAME}"

# create a variable that points to the directory where the test run results will be stored
# this directory needs to be known at test run start time, so that it can be passed to the "dotnet test" command
# both the framework and MSTest artefacts will be generated at this path
export TEST_RUN_RESULTS_PATH="${DRAMA_RESULTS_PATH}/DRAMA_RESULTS/${DRAMA_TEST_RUN_NAME}"
echo "[DEBUG] Test Run Results Path: ${TEST_RUN_RESULTS_PATH}"

# set the environment variable for the test run filter, which the framework will look for at runtime
TEST_CATEGORIES="TestCategory!=DEBUG&TestCategory!=DEMO&TestCategory!=WIP"

if [ ! -z "$DRAMA_TEST_CATEGORIES" ]
then
    TEST_CATEGORIES="$DRAMA_TEST_CATEGORIES"
else
    export DRAMA_TEST_CATEGORIES="$TEST_CATEGORIES"
fi

echo "[DEBUG] Test Run Categories: ${TEST_CATEGORIES}"

# if an "DRAMA_PROFILE" environment variable exists, log its value to the terminal
if [ ! -z "$DRAMA_PROFILE" ]
then
    echo "[DEBUG] Test Run Profile: ${DRAMA_PROFILE}"
else
    echo "[DEBUG] Test Run Profile: Default"
fi

# start the test run
dotnet test \
    --filter "${TEST_CATEGORIES}" \
    --logger "console;verbosity=detailed" \
    --logger "trx;LogFileName=${DRAMA_TEST_RUN_NAME}.trx" \
    --logger "html;LogFileName=${DRAMA_TEST_RUN_NAME}.html" \
    --results-directory "${TEST_RUN_RESULTS_PATH}"
```

</br>

Q: How do I write a Docker file, for my solution which integrates DRAMA?

A: Create a file called `DOCKERFILE`, which will contain the image definition. Also create a file called `.DOCKERIGNORE`, which will be identical to the `.GITIGNORE` file, except for the addition of excluding Git files, more specifically the `.git` directory. Assuming a test automation solution called CSyphus, an example Docker file looks like the following:

```dockerfile
#################################################################################################### STAGE 1

# add the Playwright base image, based on Ubuntu 22.04 LTS (Jammy Jellyfish)
# https://mcr.microsoft.com/v2/playwright/tags/list
# optionally, with .NET 6 included
# https://mcr.microsoft.com/v2/playwright/dotnet/tags/list
FROM mcr.microsoft.com/playwright:v1.32.0-jammy-amd64 AS dependency-resolver

# https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script
RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 7.0.201

# add "dotnet" to the system path
# https://github.com/dotnet/sdk/issues/9911
RUN export PATH="$PATH:$HOME/.dotnet"

# disable .NET CLI Tools telemetry
# https://aka.ms/dotnet-cli-telemetry
ENV DOTNET_CLI_TELEMETRY_OPTOUT true

# display .NET information
RUN dotnet --info

#################################################################################################### STAGE 2

# start second stage after all dependencies have been resolved
FROM dependency-resolver AS solution-file-handler

# set the working directory
WORKDIR /CSyphus

# copy the solution files to the working directory
COPY . .

#################################################################################################### STAGE 3

# start third stage after all solution files are in place
FROM solution-file-handler

# restore NuGet packages
RUN dotnet restore

# clean solution, if this is needed
RUN dotnet clean

# build solution
RUN dotnet build

# create persistent volume for accessing the test results
VOLUME /DRAMA_RESULTS

# # # set the test run profile
#
#	ENV DRAMA_PROFILE "CI/CD Pipeline"
#
#	for running the automated tests against more than one environment
#	this variable needs to be set by passing it as a parameter to the `docker run` command
#	when starting the test run via the build pipeline
#	instead of setting it directly in this docker file
#
#	e.g.: docker run -e DRAMA_PROFILE="CI/CD Pipeline" --name CSyphus -v "$PWD/test-artefacts":"/CSyphus/DRAMA_RESULTS" testware/csyphus:latest

# set container entry point and overridable parameters
CMD ["bash", "csyphus.sh"]
```

</br>

Q: How do I set up my solution which integrates DRAMA to run via a GitLab pipeline?

A: Create a `.GITLAB-CI.YML` configuration file along the lines of the following:

```yaml
stages:
    - execute

# https://mcr.microsoft.com/v2/playwright/tags/list
# https://mcr.microsoft.com/v2/playwright/dotnet/tags/list
image: mcr.microsoft.com/playwright:v1.31.1-focal

run-tests:
    stage: execute

    rules:
        - if: $CI_COMMIT_BRANCH == "main" && $CI_PIPELINE_SOURCE != "push"

    tags:
        - SONIC # explicitly specify runner by name

    script:
        - curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --version 7.0.201 # https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-install-script
        - export PATH="$PATH:$HOME/.dotnet" # https://github.com/dotnet/sdk/issues/9911
        - export DOTNET_CLI_TELEMETRY_OPTOUT=true # https://aka.ms/dotnet-cli-telemetry
        - dotnet --info
        - dotnet build
        - DRAMA_RESULTS_PATH=${PWD}
        - |
            echo "[DEBUG] DRAMA Results Path: ${DRAMA_RESULTS_PATH}"
        - DRAMA_TEST_RUN_NAME=$(date "+%Y-%m-%d_%H-%M-%S")
        - |
            echo "[DEBUG] DRAMA Test Run Name: ${DRAMA_TEST_RUN_NAME}"
        - TEST_RUN_RESULTS_PATH="${DRAMA_RESULTS_PATH}/DRAMA_RESULTS/${DRAMA_TEST_RUN_NAME}"
        - |
            echo "[DEBUG] DRAMA Test Run Results Path: ${TEST_RUN_RESULTS_PATH}"
        - EXIT_CODE=0
        - >
            dotnet test
            --filter "TestCategory!=DEBUG&TestCategory!=DEMO"
            --logger "trx;LogFileName=${DRAMA_TEST_RUN_NAME}.trx"
            --logger "html;LogFileName=${DRAMA_TEST_RUN_NAME}.html"
            --logger "junit;LogFileName=${DRAMA_TEST_RUN_NAME}.junit"
            --results-directory "${TEST_RUN_RESULTS_PATH}"
            || EXIT_CODE=$?
        - |
            echo "[DEBUG] DRAMA Test Run Exit Code: ${EXIT_CODE}"
        - OUTPUT_TEST_RESULTS="${PWD}/RESULTS"
        - if [ -d "${OUTPUT_TEST_RESULTS}" ]; then rm -rf "${OUTPUT_TEST_RESULTS}"; fi
        - LATEST_RESULTS="$(ls -td ${PWD}/DRAMA_RESULTS/* | head -1)"
        - |
            echo "[DEBUG] Latest Set Of Test Results: ${LATEST_RESULTS}"
        - mv "${LATEST_RESULTS}" "${OUTPUT_TEST_RESULTS}"
        - rm -rf "${PWD}/DRAMA_RESULTS"
        - exit $EXIT_CODE

    variables:
        DOTNET_CLI_TELEMETRY_OPTOUT: "true"

    artifacts:
        name: RESULTS
        when: always
        paths:
            - ./RESULTS/*.*
        reports:
            junit:
                - ./RESULTS/*.junit
```

</br>

Q: How to generate test artefacts which are compatible with GitLab?

A: Sadly, GitLab only supports test artefacts in JUnit format, so the following dependency can be added to the project which generates test artefacts: https://github.com/spekt/junit.testlogger. Following this, the `dotnet test` command can be executed with a parameter for a `junit` logger, e.g. `dotnet test --logger "junit;LogFileName=results.junit"`.

</br>

Q: How to install Playwright locally, for development purposes?

A: Follow the steps below:

1. install PowerShell by executing `dotnet tool install --global PowerShell`
2. build the solution and navigate to the directory which contains both `playwright.ps1` and `Microsoft.Playwright.dll` (e.g. `.\bin\Debug\net8.0`)
3. open a PowerShell terminal and execute `pwsh playwright.ps1 install`

</br>

Q: How do I clean up the build artefacts, for debugging purposes?

A: Use one of the following scripts:

```powershell
Get-ChildItem .\ -include bin,obj -Recurse | ForEach-Object ($_) { Remove-Item $_.FullName -Force -Recurse }
```

or

```powershell
gci -include bin,obj -recurse | remove-item -force -recurse
```

</br>
