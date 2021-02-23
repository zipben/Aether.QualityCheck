﻿using Aether.ExternalAccessClients.Interfaces;
using Aether.Models;
using APILogger.Interfaces;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RockLib.OAuth;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Aether.Models.NotificationServiceEmailBody;

namespace Aether.ExternalAccessClients
{
    public class NotificationServiceClient : INotificationServiceClient
    {
        private readonly IHttpClientWrapper _httpClient;
        private readonly Auth0AuthParams _auth0Auth;
        private readonly IApiLogger _apiLogger;
        private readonly NotificationServiceSettings _settings;
        private const string AUTHURL = "https://sso.authrock.com/oauth/token";

        public NotificationServiceClient(IHttpClientWrapper httpClient, IOptions<NotificationServiceSettings> config, IApiLogger apiLogger)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _settings = config.Value;
            _httpClient.SetBaseURI(_settings.BaseUrl);
            _auth0Auth = new Auth0AuthParams(_settings.ClientID, _settings.ClientSecret, _settings.Audience, AUTHURL);
            _apiLogger = apiLogger;
        }

        public async Task<bool> TryPostRequestAsync(EmailRootObject emailObj)
        {
            _apiLogger.LogInfo($"{nameof(NotificationServiceClient)}:{nameof(TryPostRequestAsync)}");

            using StringContent emailRequestContent = GenerateEmailRequestBody(emailObj);

            try
            {
                using var response = await _httpClient.PostAsync(_auth0Auth, _settings.EndPoint, emailRequestContent);
                return response.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                _apiLogger.LogError($"{nameof(NotificationServiceClient)}:{nameof(TryPostRequestAsync)}", exception: e);

                throw new HttpRequestException($"Email failed to send: {e.Message}");
            }
        }

        private StringContent GenerateEmailRequestBody(EmailRootObject emailObject)
        {
            if (emailObject == null)
                throw new ArgumentNullException(nameof(emailObject));

            _apiLogger.LogInfo($"{nameof(NotificationServiceClient)}:{nameof(GenerateEmailRequestBody)}", emailObject);
            try
            {
                var stringContent = JsonConvert.SerializeObject(emailObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                return new StringContent(stringContent);
            }
            catch (Exception e)
            {
                _apiLogger.LogError($"{nameof(NotificationServiceClient)}:{nameof(GenerateEmailRequestBody)}", exception: e);
                throw new JsonSerializationException("Encountered an exception while trying to serialize request model into string content");
            }
        }
    }
}