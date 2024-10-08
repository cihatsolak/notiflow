﻿global using AutoMapper;
global using FluentValidation;
global using MassTransit;
global using MediatR;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.EntityFrameworkCore.Storage;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Notiflow.Backoffice.Application.AuthorizationRequirements;
global using Notiflow.Backoffice.Application.ClaimsTransformations;
global using Notiflow.Backoffice.Application.Consumers;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;
global using Notiflow.Backoffice.Application.Features.Commands.Emails;
global using Notiflow.Backoffice.Application.Features.Commands.Notifications;
global using Notiflow.Backoffice.Application.Features.Commands.TextMessages;
global using Notiflow.Backoffice.Application.Features.Notifications;
global using Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;
global using Notiflow.Backoffice.Application.Interfaces.Repositories;
global using Notiflow.Backoffice.Application.Interfaces.Services;
global using Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;
global using Notiflow.Backoffice.Application.Mappers;
global using Notiflow.Backoffice.Application.Models.Common;
global using Notiflow.Backoffice.Application.Models.Emails;
global using Notiflow.Backoffice.Application.Models.Notifications;
global using Notiflow.Backoffice.Application.Pipelines;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Devices;
global using Notiflow.Backoffice.Domain.Entities.Histories;
global using Notiflow.Backoffice.Domain.Enums;
global using Notiflow.Common.Caching;
global using Notiflow.Common.Exceptions;
global using Notiflow.Common.Extensions;
global using Notiflow.Common.Localize;
global using Notiflow.Common.MessageBroker;
global using Notiflow.Common.MessageBroker.Events;
global using Notiflow.Common.MessageBroker.Events.Emails;
global using Notiflow.Common.MessageBroker.Events.Notifications;
global using Notiflow.Common.MessageBroker.Events.TextMessage;
global using Notiflow.Common.Settings;
global using Puzzle.Lib.Assistant.Extensions;
global using Puzzle.Lib.Cache;
global using Puzzle.Lib.Cache.Infrastructure;
global using Puzzle.Lib.Cache.Services.Cache;
global using Puzzle.Lib.Database.Interfaces;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Validation;
global using Puzzle.Lib.Validation.RuleBuilders;
global using System.Diagnostics;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text.Json.Serialization;
global using IPublishEndpoint = MassTransit.IPublishEndpoint;
