# Project Database Configuration

This document outlines the steps to configure the database username and password for the Plottar project.

## Prerequisites

Before proceeding, ensure you have the following:

- Access to the project source code.
- PostgreSQL installed on your system.
- Basic understanding of C# and .NET Core.

## Configuration Steps

Follow these steps to configure the database username and password:

1. **Set Database Username and Password:**

   Open your command-line interface (CLI) and navigate to the project directory.

   Run the following commands to set the database username and password using `dotnet user-secrets`:

   ```bash
   dotnet user-secrets set "plottar_db_username" "<DatabaseUsername>"
   dotnet user-secrets set "plottar_db_password" "<DatabasePassword>"
   ```
