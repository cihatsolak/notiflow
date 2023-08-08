﻿global using FluentValidation;
global using Mapster;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using Microsoft.IdentityModel.Tokens;
global using Notiflow.Common.Caching;
global using Notiflow.Common.Caching.Models;
global using Notiflow.IdentityServer.Core.Entities.Tenants;
global using Notiflow.IdentityServer.Core.Entities.Users;
global using Notiflow.IdentityServer.Data;
global using Notiflow.IdentityServer.Service.Auth;
global using Notiflow.IdentityServer.Service.Models.Auths;
global using Notiflow.IdentityServer.Service.Models.TenantPermissions;
global using Notiflow.IdentityServer.Service.Models.Users;
global using Notiflow.IdentityServer.Service.Observers;
global using Notiflow.IdentityServer.Service.TenantPermissions;
global using Notiflow.IdentityServer.Service.Tenants;
global using Notiflow.IdentityServer.Service.Tokens;
global using Notiflow.IdentityServer.Service.Users;
global using Puzzle.Lib.Assistant.Extensions;
global using Puzzle.Lib.Auth;
global using Puzzle.Lib.Auth.Infrastructure.Extensions;
global using Puzzle.Lib.Auth.Infrastructure.Settings;
global using Puzzle.Lib.Auth.Models;
global using Puzzle.Lib.Auth.Services;
global using Puzzle.Lib.Cache;
global using Puzzle.Lib.Cache.Services.Cache;
global using Puzzle.Lib.Response.Models;
global using Puzzle.Lib.Validation.RuleBuilders;
global using System.IdentityModel.Tokens.Jwt;
global using System.Reflection;
global using System.Runtime.Serialization;
global using System.Security.Claims;
