﻿global using AutoMapper;
global using FluentValidation;
global using MediatR;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Notiflow.Backoffice.Application.Constants;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Insert;
global using Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;
global using Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Tenants;
global using Notiflow.Backoffice.Application.Interfaces.Services;
global using Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;
global using Notiflow.Backoffice.Application.Mappers;
global using Notiflow.Backoffice.Application.Models;
global using Notiflow.Backoffice.Application.Pipelines;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Devices;
global using Notiflow.Backoffice.Domain.Entities.Tenants;
global using Notiflow.Backoffice.Domain.Enums;
global using Puzzle.Lib.Database.Interfaces;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Validation.IOC;
global using Puzzle.Lib.Validation.RuleBuilders;
global using System.Diagnostics;
global using System.Reflection;
global using System.Text.Json.Serialization;
