﻿global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Http;
global using Microsoft.AspNetCore.HttpLogging;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Logging;
global using NpgsqlTypes;
global using Puzzle.Lib.SeriLog.ColumnWriters;
global using Puzzle.Lib.SeriLog.Constants;
global using Puzzle.Lib.SeriLog.LoggerConfigurations;
global using Puzzle.Lib.SeriLog.Middlewares;
global using Serilog;
global using Serilog.Context;
global using Serilog.Events;
global using Serilog.Filters;
global using Serilog.Formatting.Elasticsearch;
global using Serilog.Sinks.Elasticsearch;
global using Serilog.Sinks.MicrosoftTeams;
global using Serilog.Sinks.MSSqlServer;
global using Serilog.Sinks.PostgreSQL;
global using System.Collections.ObjectModel;
global using System.Data;
