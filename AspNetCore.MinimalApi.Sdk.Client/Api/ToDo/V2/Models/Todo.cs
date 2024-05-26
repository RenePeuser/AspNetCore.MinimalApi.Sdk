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

namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V2
{
    public record Todo(
        int Id,
        string? Title,
        DateOnly? DueBy = null,
        bool IsComplete = false);
}