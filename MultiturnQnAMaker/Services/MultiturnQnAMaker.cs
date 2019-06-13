// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.QnA;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using QnAPrompting.Helpers;

namespace QnA.MultiturnQnAMaker
{
    public class MultiturnQnAMaker : QnAMaker
    {
        public const string FunctionStateName = "functionState";
        private readonly HttpClient _httpClient;
        private readonly QnAMakerEndpoint _endpoint;
        private readonly QnAMakerOptions _options;

        public MultiturnQnAMaker(QnAMakerEndpoint endpoint, HttpClient httpClient, IBotTelemetryClient telemetryClient, bool logPersonalInformation = false) : base(endpoint, null, httpClient, telemetryClient, logPersonalInformation)
        {
            _httpClient = httpClient;
            _endpoint = endpoint;
            _options = new QnAMakerOptions { Top = 3 };
        }

        public async Task<(MultiturnQnAState newState, IEnumerable<Activity> output)> GetAnswersAsync(MultiturnQnAState oldState, ITurnContext turnContext, QnAMakerOptions options, Dictionary<string, string> telemetryProperties, Dictionary<string, double> telemetryMetrics = null)
        {
            Activity outputActivity = null;
            MultiturnQnAState newState = null;

            var query = turnContext.Activity.Text;
            var qnaResult = await QueryQnAServiceAsync(query, oldState);
            var qnaAnswer = qnaResult[0].Answer;
            var prompts = qnaResult[0].Context?.Prompts;

            if (prompts == null || prompts.Length < 1)
            {
                outputActivity = MessageFactory.Text(qnaAnswer);
            }
            else
            {
                // Set bot state only if prompts are found in QnA result
                newState = new MultiturnQnAState
                {
                    PreviousQnaId = qnaResult[0].Id,
                    PreviousUserQuery = query
                };

                outputActivity = CardHelper.GetHeroCard(qnaAnswer, prompts);
            }

            return (newState, new Activity[] { outputActivity });
        }

        public async Task<MultiturnQnAResult[]> QueryQnAServiceAsync(string query, MultiturnQnAState oldState)
        {
            var requestUrl = $"{_endpoint.Host}/knowledgebases/{_endpoint.KnowledgeBaseId}/generateanswer";
            var request = new HttpRequestMessage(HttpMethod.Post, requestUrl);
            var jsonRequest = JsonConvert.SerializeObject(
                new
                {
                    question = query,
                    top = _options.Top,
                    context = oldState,
                    strictFilters = _options.StrictFilters,
                    metadataBoost = _options.MetadataBoost,
                    scoreThreshold = _options.ScoreThreshold,
                }, Formatting.None);

            request.Headers.Add("Authorization", $"EndpointKey {_endpoint.EndpointKey}");
            request.Content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

            var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentString = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<MultiturnQnAResultList>(contentString);

            return result.Answers;
        }
    }
}
