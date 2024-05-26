using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Extensions.Pack;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V2
{
    internal static class AddToDoV2Extension
    {
        internal static void AddToDoV2(this IServiceCollection services)
        {
            services.AddHttpCallHandler();
        }
    }
    
    
    public class ToDoV2
    {
        private readonly IHttpCallHandler _httpCallHandler;
        
        public ToDoV2(IHttpCallHandler httpCallHandler)
        {
            _httpCallHandler = httpCallHandler;
        }
        
        
        public Task<Todo> GetAllToDosAsync()
        {
            return _httpCallHandler.CallAsync<Todo>(HttpMethod.Get, $"api/v2.0/todos", null, CancellationToken.None);
        }
    }
}