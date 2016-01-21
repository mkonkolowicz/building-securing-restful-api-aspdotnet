﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Web;

namespace ExpenseTracker.WebClient.Helpers
{
    public static class ExpenseTrackerHttpClient
    {

        public static HttpClient GetClient(string requestedVersion = null)
        {
           
            HttpClient client = new HttpClient();

            var token = (HttpContext.Current.User.Identity as ClaimsIdentity).FindFirst("access_token");
            if (token != null)
            {
                client.SetBearerToken(token.Value);
            }

            client.BaseAddress = new Uri(ExpenseTrackerConstants.ExpenseTrackerAPI);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (requestedVersion != null)
            {
                // through a custom request header
                //client.DefaultRequestHeaders.Add("api-version", requestedVersion);

                // through content negotiation
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/vnd.expensetrackerapi.v" 
                        + requestedVersion + "+json"));
            }


            return client;
        }
    }
}