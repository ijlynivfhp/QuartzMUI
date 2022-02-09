﻿using Microsoft.AspNetCore.Antiforgery;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GZY.Quartz.MUI.Extensions
{
    public static class HttpClientFactoryExtension
    {
        public static async Task<string> HttpSendAsync(this IHttpClientFactory httpClientFactory, HttpMethod method, string url, Dictionary<string, string> headers = null)
        {
            var client = httpClientFactory.CreateClient();
            var content = new StringContent("");
            // content.Headers.ContentType = new MediaTypeHeaderValue(contentType);
            var request = new HttpRequestMessage(method, url)
            {
                Content = content
            };
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            try
            {
                HttpResponseMessage httpResponseMessage = await client.SendAsync(request);

                var result = await httpResponseMessage.Content
                    .ReadAsStringAsync();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return ex.Message;
            }
        }
    }
}
