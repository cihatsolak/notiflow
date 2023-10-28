﻿global using MediatR;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Mvc.Authorization;
global using Notiflow.Backoffice.API;
global using Notiflow.Backoffice.API.Infrastructure;
global using Notiflow.Backoffice.Application;
global using Notiflow.Backoffice.Application.AuthorizationRequirements;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Add;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.DataTable;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Update;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateBlocking;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.UpdateEmail;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.UpdatePhoneNumber;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Add;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.DataTable;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Delete;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Update;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.UpdateToken;
global using Notiflow.Backoffice.Application.Features.Commands.Emails.Send;
global using Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;
global using Notiflow.Backoffice.Application.Features.Commands.TextMessages.Datatable;
global using Notiflow.Backoffice.Application.Features.Commands.TextMessages.Send;
global using Notiflow.Backoffice.Application.Features.Queries.Customers.GetById;
global using Notiflow.Backoffice.Application.Features.Queries.Devices.GetById;
global using Notiflow.Backoffice.Application.Features.Queries.Emails.GetById;
global using Notiflow.Backoffice.Application.Features.Queries.Notifications.GetById;
global using Notiflow.Backoffice.Application.Features.Queries.TextMessageHistories.GetById;
global using Notiflow.Backoffice.Application.Models.Common;
global using Notiflow.Backoffice.Infrastructure;
global using Notiflow.Backoffice.Infrastructure.Filters;
global using Notiflow.Backoffice.Persistence;
global using Notiflow.Common.Extensions;
global using Puzzle.Lib.Auth;
global using Puzzle.Lib.Auth.Infrastructure;
global using Puzzle.Lib.Database;
global using Puzzle.Lib.Documentation;
global using Puzzle.Lib.Documentation.Settings;
global using Puzzle.Lib.HealthCheck;
global using Puzzle.Lib.HealthCheck.Checks;
global using Puzzle.Lib.Host;
global using Puzzle.Lib.Localize;
global using Puzzle.Lib.Logging.Builders;
global using Puzzle.Lib.Logging.Infrastructure;
global using Puzzle.Lib.Performance;
global using Puzzle.Lib.Response.Controllers;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Security;
global using Puzzle.Lib.Version;
global using Puzzle.Lib.Version.Infrastructure;
