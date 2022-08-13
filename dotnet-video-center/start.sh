#!/bin/bash
rm -rf Application.db Logs
dotnet build 
dotnet ef database update
dotnet run