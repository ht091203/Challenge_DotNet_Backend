<<<<<<< Updated upstream
﻿using System.Reflection;
using Application;
=======
﻿using Application;
using Application.Settings;
using Asp.Versioning.ApiExplorer;
using Common.Application.Configurations;
using Common.Application.Settings;
using Common.Loggers.Interfaces;
using Common.Loggers.SeriLog;
//using DotNetTraining.Application.Settings;y

>>>>>>> Stashed changes
using DotNetTraining.AutoMappers;
using DotNetTraining.Repositories;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 1L * 1024 * 1024 * 1024; // Set limit to 1GB
});
// đăng ký AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
var application = new Startup(builder, xmlPath, Assembly.GetExecutingAssembly());
application.Start();
