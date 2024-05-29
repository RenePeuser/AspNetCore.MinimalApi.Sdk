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

namespace AspNetCore.MinimalApi.Sdk.Client.Api.ToDo.V1
{
    internal static class AddToDoV1Extension
    {
        internal static void AddToDoV1(this IServiceCollection services)
        {
            services.AddHttpCallHandler();
        }
    }
    
    
    public class ToDoV1
    {
        private readonly IHttpCallHandler _httpCallHandler;
        
        public ToDoV1(IHttpCallHandler httpCallHandler)
        {
            _httpCallHandler = httpCallHandler;
        }
        
        
        public Task<Todo> AddToDoAsync(Todo todo)
        {
            return _httpCallHandler.CallAsync<Todo>(HttpMethod.Post, $"api/v1.0/todos", todo, CancellationToken.None);
        }

        
        public Task DeleteToDoByIdAsync(int id)
        {
            return _httpCallHandler.CallAsync(HttpMethod.Delete, $"api/v1.0/todos/{id}", null, CancellationToken.None);
        }

        
        public Task<IEnumerable<Todo>> GetAllToDosAsync()
        {
            return _httpCallHandler.CallAsync<IEnumerable<Todo>>(HttpMethod.Get, $"api/v1.0/todos", null, CancellationToken.None);
        }

        
        public Task<Todo> GetToDoByIdAsync(int id)
        {
            return _httpCallHandler.CallAsync<Todo>(HttpMethod.Get, $"api/v1.0/todos/{id}", null, CancellationToken.None);
        }

        
        public Task<Todo> UpdateToDoByIdAsync(int id, Todo todo)
        {
            return _httpCallHandler.CallAsync<Todo>(HttpMethod.Put, $"api/v1.0/todos/{id}", todo, CancellationToken.None);
        }
    }
}