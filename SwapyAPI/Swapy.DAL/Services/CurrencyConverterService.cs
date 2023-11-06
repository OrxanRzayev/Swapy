using Swapy.BLL.Interfaces;
using RestSharp;
using System.Text.Json;
using System;
using System.Net;

namespace Swapy.BLL.Services
{
    public class CurrencyConverterService : ICurrencyConverterService
    {
        private readonly string api = "https://cdn.jsdelivr.net/gh/fawazahmed0/currency-api@1/latest/currencies/";

        public decimal Convert(string fromCode, string toCode, decimal value)
        {
            var client = new RestClient($"{api}{fromCode.ToLower()}/{toCode.ToLower()}.min.json");
            var request = new RestRequest();
            request.AddHeader("accept", "application/json");
            RestResponse<decimal> response = client.Execute<decimal>(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var jsonString = response.Content;
                var jsonDocument = JsonDocument.Parse(jsonString);

                if (jsonDocument.RootElement.TryGetProperty(toCode.ToLower(), out var rateElement))
                {
                    var rate = rateElement.GetDecimal();
                    var result = rate * value;
                    return result;
                }
                else
                {
                    throw new ArgumentException($"Currency code {toCode} not found in the response.");
                }
            }
            else
            {
                throw new ArgumentException($"Failed to retrieve currency data. Response status code: {response.StatusCode}");
            }
        }
    }
}
