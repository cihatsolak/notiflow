﻿global using Bogus;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.Extensions.DependencyInjection;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Customers;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Devices;
global using Notiflow.Backoffice.Application.Interfaces.Repositories.Tenants;
global using Notiflow.Backoffice.Application.Interfaces.UnitOfWorks;
global using Notiflow.Backoffice.Domain.Entities.Customers;
global using Notiflow.Backoffice.Domain.Entities.Devices;
global using Notiflow.Backoffice.Domain.Entities.Histories;
global using Notiflow.Backoffice.Domain.Enums;
global using Notiflow.Backoffice.Persistence.Contexts;
global using Notiflow.Backoffice.Persistence.Repositories.Customers;
global using Notiflow.Backoffice.Persistence.Repositories.Devices;
global using Notiflow.Backoffice.Persistence.Seeds;
global using Notiflow.Backoffice.Persistence.UnitOfWorks;
global using Puzzle.Lib.Database.Concrete;
global using Puzzle.Lib.Database.IOC;
global using Puzzle.Lib.Entities.Entities.Base;
global using Puzzle.Lib.Entities.Entities.Historical;
global using Puzzle.Lib.Entities.Entities.HistoricalSoftDelete;
global using System.Diagnostics;
global using System.Reflection;

