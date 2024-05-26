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
    public record Notes
    {
        public string Key { get; init; } = string.Empty;
        public string Hint { get; init; } = string.Empty;
    }
}