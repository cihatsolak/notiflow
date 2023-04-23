﻿global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using Microsoft.Extensions.Primitives;
global using Microsoft.IdentityModel.Tokens;
global using Notiflow.Backoffice.Domain.Entities.Users;
global using Notiflow.IdentityServer.Service.Tenants;
global using Notiflow.IdentityServer.Service.Tokens;
global using Puzzle.Lib.Auth.Infrastructure.Extensions;
global using Puzzle.Lib.Auth.Infrastructure.Settings;
global using Puzzle.Lib.Auth.Models;
global using Puzzle.Lib.Response.Models;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;