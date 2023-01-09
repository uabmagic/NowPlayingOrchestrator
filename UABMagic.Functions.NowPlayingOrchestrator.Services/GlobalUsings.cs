global using System.Diagnostics.CodeAnalysis;
global using System.Text;
global using System.Text.Json;
global using Azure.Storage.Queues;
global using Microsoft.Extensions.DependencyInjection;
global using Refit;

global using UABMagic.Functions.NowPlayingOrchestrator.Core.Constants;
global using UABMagic.Functions.NowPlayingOrchestrator.Data.Entities;
global using UABMagic.Functions.NowPlayingOrchestrator.Data.Interfaces;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.GoogleFCM;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.DTOs.Songs;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.Interfaces;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.QueueMessages;
global using UABMagic.Functions.NowPlayingOrchestrator.Services.Utilities.Extensions;
