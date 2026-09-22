# StudentTaskTracker

A console-based task and team management application designed for student teams, built with C# and .NET.

## Overview
StudentTaskTracker helps student project teams organize tasks, assign responsibilities, and monitor workflow status across clear lifecycle stages.

## Project Structure
- `Models/`: Core domain models representing business entities.
  - `TaskStatus.cs`: Enum defining task stages (`Backlog`, `InProgress`, `InReview`, `Done`).
  - `TeamMember.cs`: Model representing student team members.
  - `ProjectTask.cs`: Model representing assigned tasks with deadlines and status.
- `Services/`: Business logic layer managing operations (in progress).

## Tech Stack
- **Language:** C#
- **Platform:** .NET 10 (Console Application)
- **Architecture:** Layered Architecture (Separation of Concerns)

## Current Status
- [x] Initial repository setup
- [x] Domain models implementation (`TaskStatus`, `TeamMember`, `ProjectTask`)
- [ ] Task and member management services (`TaskManager`)
- [ ] Interactive console interface
- [ ] Database persistence (SQLite / EF Core)

## How to Run Locally
```bash
dotnet build
dotnet run