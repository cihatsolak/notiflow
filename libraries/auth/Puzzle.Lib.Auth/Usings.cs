﻿global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.AspNetCore.Builder;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.DependencyInjection.Extensions;
global using Microsoft.IdentityModel.Tokens;
global using Puzzle.Lib.Auth.Infrastructure.Exceptions;
global using Puzzle.Lib.Auth.Infrastructure.Settings;
global using Puzzle.Lib.Auth.Services;
global using System.Data;
global using System.IdentityModel.Tokens.Jwt;
global using System.Runtime.Serialization;
global using System.Security.Claims;
global using System.Security.Cryptography;
global using System.Text;