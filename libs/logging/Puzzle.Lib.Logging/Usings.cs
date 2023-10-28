﻿global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.HttpLogging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using NpgsqlTypes;
global using Puzzle.Lib.Logging.Infrastructure;
global using Puzzle.Lib.Logging.SeriLog.ColumnWriters;
global using Puzzle.Lib.Logging.SeriLog.Configurations;
global using Puzzle.Lib.Logging.SeriLog.Enrichers;
global using Serilog;
global using Serilog.Configuration;
global using Serilog.Core;
global using Serilog.Events;
global using Serilog.Exceptions;
global using Serilog.Filters;
global using Serilog.Formatting.Elasticsearch;
global using Serilog.Sinks.Elasticsearch;
global using Serilog.Sinks.MicrosoftTeams;
global using Serilog.Sinks.MSSqlServer;
global using Serilog.Sinks.PostgreSQL;
global using System.Collections.ObjectModel;
global using System.Data;
global using System.Reflection;
