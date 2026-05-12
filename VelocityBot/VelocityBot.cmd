@echo off

REM =========================
REM VELOCITY BOT CONFIG
REM =========================

set SERVER_SECRET=oXh9uJXXhUynaqjkV6MWfdr53EAcve0qOdJV5b2q70

REM MODE:
REM dev     = rebuild terus
REM release = langsung run

set MODE=dev

REM =========================
REM RUN BOT
REM =========================

if "%MODE%"=="dev" (

    echo Cleaning project...
    rmdir /s /q bin >nul 2>&1
    rmdir /s /q obj >nul 2>&1

    echo Building bot...
    dotnet build

    echo Running bot...
    dotnet run --no-build

) else if "%MODE%"=="release" (

    if exist bin\ (

        echo Running release bot...
        dotnet run --no-build

    ) else (

        echo Build not found, building first...
        dotnet build
        dotnet run --no-build
    )

) else (

    echo Invalid MODE
)

