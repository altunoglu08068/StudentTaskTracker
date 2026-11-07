# StudentTaskTracker

A console-based task and team management application designed for student teams, built with C# and .NET.

## Overview
StudentTaskTracker helps student project teams organize tasks, assign responsibilities, and monitor workflow status across clear lifecycle stages.

## Features
- Add and list team members, including their name, email, and role.
- Create tasks with a title, description, and due date.
- List all tasks and see their status and assigned member.
- Assign a task to a team member and list tasks assigned to a specific member.
- Update a task's status: `Backlog`, `InProgress`, `InReview`, or `Done`.
- Validate positive numeric IDs and status selections in the interactive menu.

## Project Structure
- `Models/`: Core domain models representing business entities.
  - `TaskStatus.cs`: Enum defining task stages (`Backlog`, `InProgress`, `InReview`, `Done`).
  - `TeamMember.cs`: Model representing student team members.
  - `ProjectTask.cs`: Model representing assigned tasks with deadlines and status.
- `Services/`: Business logic for managing team members and tasks.
- `Program.cs`: Interactive console menu and user input handling.

## Tech Stack
- **Language:** C#
- **Platform:** .NET 10 (Console Application)
- **Architecture:** Layered Architecture (Separation of Concerns)

## Current Status
- [x] Initial repository setup
- [x] Domain models implementation (`TaskStatus`, `TeamMember`, `ProjectTask`)
- [x] Task and member management services (`TaskManager`)
- [x] Interactive console interface
- [ ] Database persistence (SQLite / EF Core)

## How to Run Locally
Requires the .NET 10 SDK.

```bash
dotnet build
dotnet run
```

All tasks and team members are currently stored in memory and are cleared when the application exits.