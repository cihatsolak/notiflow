﻿global using Bogus;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Design;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.EmailHistories;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.NotificationHistories;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.TextMessageHistories;
global using Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Devices;
global using Notiflow.Backoffice.Domain.Entities.Histories;
global using Notiflow.Backoffice.Domain.Enums;
global using Notiflow.Backoffice.Persistence.Contexts;
global using Notiflow.Backoffice.Persistence.Repositories.Customers;
global using Notiflow.Backoffice.Persistence.Repositories.Devices;
global using Notiflow.Backoffice.Persistence.Repositories.EmailHistories;
global using Notiflow.Backoffice.Persistence.Repositories.NotificationHistories;
global using Notiflow.Backoffice.Persistence.Repositories.TextMessageHistories;
global using Notiflow.Backoffice.Persistence.Seeds;
global using Notiflow.Backoffice.Persistence.UnitOfWorks;
global using Puzzle.Lib.Database;
global using Puzzle.Lib.Database.Concrete;
global using Puzzle.Lib.Database.Infrastructure.Settings;
global using Puzzle.Lib.Entities.Entities.Base;
global using Puzzle.Lib.Entities.Entities.Historical;
global using Puzzle.Lib.Entities.Entities.HistoricalSoftDelete;
global using Puzzle.Lib.Entities.Entities.SoftDelete;
global using System.Diagnostics;
global using System.Linq.Dynamic.Core;
global using System.Reflection;
global using System.Security.Claims;
global using System.Text;
