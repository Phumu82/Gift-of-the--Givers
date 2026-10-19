using System.Net.Http;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace GiftOfTheGivers.Web.Services
{
    public class AzureFunctionTaxCertificateService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public AzureFunctionTaxCertificateService(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string?> GenerateTaxCertificateAsync(
            string donationReference,
            string donorName,
            decimal amount)
        {
            var baseUrl = _configuration["AzureFunctions:TaxCertificateUrl"];

            var url =
                $"{baseUrl}?donationReference={Uri.EscapeDataString(donationReference)}" +
                $"&donorName={Uri.EscapeDataString(donorName)}" +
                $"&amount={amount}";

            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}