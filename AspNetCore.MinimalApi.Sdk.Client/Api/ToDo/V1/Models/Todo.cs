using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Extensions.Pack;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V1
{
    public record Todo
    {
        public int Id { get; init; }
        public string? Title { get; init; }
        public Notes Notes { get; init; } = new Notes();
        public DateOnly? DueBy { get; init; }
        public bool IsComplete { get; init; }
    }
}