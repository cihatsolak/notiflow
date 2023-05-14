﻿global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Notiflow.Backoffice.Application;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Add;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.ChangeBlocking;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Delete;
global using Notiflow.Backoffice.Application.Features.Commands.Customers.Update;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.Add;
global using Notiflow.Backoffice.Application.Features.Commands.Devices.GetDeviceById;
global using Notiflow.Backoffice.Application.Features.Commands.Notifications.Send;
global using Notiflow.Backoffice.Application.Features.Commands.Tenants.Add;
global using Notiflow.Backoffice.Application.Features.Queries.Customers.GetCustomerById;
global using Notiflow.Backoffice.Application.Features.Queries.Tenants.GetDetailById;
global using Notiflow.Backoffice.Infrastructure;
global using Notiflow.Backoffice.Infrastructure.Middlewares;
global using Notiflow.Backoffice.Persistence;
global using Puzzle.Lib.Documentation.IOC;
global using Puzzle.Lib.Documentation.Middlewares;
global using Puzzle.Lib.Response.Controllers;
global using Puzzle.Lib.Response.Models;
