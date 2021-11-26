using ApiMapping.Domain.Constants;
using ApiMapping.Domain.Interfaces.Services;
using ExampleProjectsCommomResources.Domain.Models.ApplicationModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ExampleProjectsCommomResources.Infraestructure.Repositories.Rest.Base
{
    public class BaseRestRepository
    {
        protected string CompleteEndpoint { get; private set; }
        private readonly HttpClient _httpClient;
        private readonly IApiMappingCustomRequestContextService _apiMappingCustomRequestContextService;

        public BaseRestRepository(HttpClient httpClient, IApiMappingCustomRequestContextService apiMappingCustomRequestContextService)
        {
            _httpClient = httpClient;
            _apiMappingCustomRequestContextService = apiMappingCustomRequestContextService;
        }

        public async Task<Result<TResponse>> GetAsync<TResponse>(string endpoint)
        {
            return await this.SendRequestAsync<TResponse>(
                httpMethod: HttpMethod.Get,
                endpoint: endpoint,
                requestBody: null);
        }

        public async Task<Result<TResponse>> PostAsync<TResponse>(string endpoint, StringContent requestBody = null)
        {
            return await this.SendRequestAsync<TResponse>(
                httpMethod: HttpMethod.Post,
                endpoint: endpoint,
                requestBody: requestBody);
        }

        public async Task<Result<TResponse>> PutAsync<TResponse>(string endpoint, StringContent requestBody = null)
        {
            return await this.SendRequestAsync<TResponse>(
                httpMethod: HttpMethod.Put,
                endpoint: endpoint,
                requestBody: requestBody);
        }

        public async Task<Result<TResponse>> DeleteAsync<TResponse>(string endpoint, StringContent requestBody = null)
        {
            return await this.SendRequestAsync<TResponse>(
                httpMethod: HttpMethod.Delete,
                endpoint: endpoint,
                requestBody: requestBody);
        }

        private async Task<Result<TResponse>> SendRequestAsync<TResponse>(
            HttpMethod httpMethod,
            string endpoint,
            StringContent requestBody = null)
        {
            CompleteEndpoint = $"{_httpClient.BaseAddress}{endpoint}";

            try
            {
                var requestMessage = new HttpRequestMessage(httpMethod, endpoint);
                if (requestBody != null)
                {
                    requestMessage.Content = requestBody;
                    requestMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }

                UpdateRequestHeadersWithDependencies();

                var response = await _httpClient.SendAsync(requestMessage);

                return await HandleResponseAsync<TResponse>(response);
            }
            catch (Exception exception)
            {
                var error = new Error(exception);

                return Result<TResponse>.Fail(error);
            }
        }

        private void UpdateRequestHeadersWithDependencies()
        {
            var consumerApiResourceNameHeaderKey = _apiMappingCustomRequestContextService.GetHeaderValue(HeadersConstants.ConsumerApiResourceNameHeaderKey);
            if(!string.IsNullOrWhiteSpace(consumerApiResourceNameHeaderKey))
                _httpClient.DefaultRequestHeaders.Add(HeadersConstants.ConsumerApiResourceNameHeaderKey, consumerApiResourceNameHeaderKey);

            var consumedApiResourceNameHeaderKey = _apiMappingCustomRequestContextService.GetHeaderValue(HeadersConstants.ConsumedApiResourceNameHeaderKey);
            if (!string.IsNullOrWhiteSpace(consumerApiResourceNameHeaderKey))
                _httpClient.DefaultRequestHeaders.Add(HeadersConstants.ConsumedApiResourceNameHeaderKey, consumedApiResourceNameHeaderKey);
        }

        private async Task<Result<TResponse>> HandleResponseAsync<TResponse>(HttpResponseMessage response)
        {
            var contentString = await ReadContentAsync(response);

            if (response.IsSuccessStatusCode)
            {
                return await HandleSuccessResponseAsync<TResponse>(contentString);
            }
            else
            {
                return await HandleErrorResponseAsync<TResponse>(contentString);
            }
        }

        private async Task<Result<TResponse>> HandleErrorResponseAsync<TResponse>(string contentString)
        {
            var error = new Error($"Error in endpoint: {CompleteEndpoint}", contentString);

            return Result<TResponse>.Fail(error);
        }

        private async Task<Result<TResponse>> HandleSuccessResponseAsync<TResponse>(string contentString)
        {
            var deserializedResponse = JsonConvert.DeserializeObject<TResponse>(contentString);
            return await Task.FromResult(Result<TResponse>.Success(deserializedResponse));
        }

        private async Task<string> ReadContentAsync(HttpResponseMessage response)
        {
            if (response.Content == null)
            {
                return string.Empty;
            }

            return await response.Content.ReadAsStringAsync();
        }

        private void AddCustomHeader(string key, string value)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                _httpClient.DefaultRequestHeaders.Add(key, value);
            }
        }

        private IDictionary<string, string> GetAllRequestHeaders(params HttpHeaders[] headersList) => headersList.SelectMany(header => header).ToDictionary(header => header.Key, header => string.Join(';', header.Value));
    }
}
