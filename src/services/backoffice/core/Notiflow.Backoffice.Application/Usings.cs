﻿global using AutoMapper;
global using FluentValidation;
global using MediatR;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Notiflow.Backoffice.Application.Constants;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Add;
global using Notiflow.Backoffice.Application.Features.Commands.Notifications.SendSingle;
global using Notiflow.Backoffice.Application.Features.Commands.TextMessages.SendSingle;
global using Notiflow.Backoffice.Application.Features.Queries.Devices.GetDeviceById;
global using Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetTextMessageHistoryById;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.TextMessageHistories;
global using Notiflow.Backoffice.Application.Interfaces.Services;
global using Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;
global using Notiflow.Backoffice.Application.Mappers;
global using Notiflow.Backoffice.Application.Models;
global using Notiflow.Backoffice.Application.Models.Huawei;
global using Notiflow.Backoffice.Application.Pipelines;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Devices;
global using Notiflow.Backoffice.Domain.Entities.Histories;
global using Notiflow.Backoffice.Domain.Enums;
global using Notiflow.Common.Caching;
global using Notiflow.Common.MessageBroker.Events.Notifications;
global using Notiflow.Common.MessageBroker.Events.TextMessage;
global using Notiflow.Common.Services;
global using Notiflow.Common.Settings;
global using Puzzle.Lib.Assistant.Extensions;
global using Puzzle.Lib.Cache.Services.Cache;
global using Puzzle.Lib.Database.Interfaces;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Validation.IOC;
global using Puzzle.Lib.Validation.RuleBuilders;
global using System.Diagnostics;
global using System.Reflection;
global using System.Text.Json.Serialization;
global using IPublishEndpoint = MassTransit.IPublishEndpoint;
