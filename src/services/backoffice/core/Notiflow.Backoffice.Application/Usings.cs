﻿global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Notiflow.Backoffice.Application.Interfaces.Services;
global using Notiflow.Backoffice.Application.Models;
global using Notiflow.Backoffice.Application.Pipelines;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Tenants;
global using Puzzle.Lib.Database.Interfaces;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Validation.RuleBuilders;
global using System.Diagnostics;
global using System.Reflection;
global using System.Text.Json.Serialization;
