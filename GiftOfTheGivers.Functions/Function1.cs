using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GiftOfTheGivers.Functions
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;

        public Function1(ILogger<Function1> logger)
        {
            _logger = logger;
        }

        [Function("GenerateTaxCertificate")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")]
            HttpRequest req)
        {
            _logger.LogInformation(
                "Gift of the Givers tax certificate function was triggered.");

            // Example values received from the donation system.
            string donationReference =
                req.Query["donationReference"].FirstOrDefault()
                ?? "DON-DEMO-2026-001";

            string donorName =
                req.Query["donorName"].FirstOrDefault()
                ?? "Demo Donor";

            string amountText =
                req.Query["amount"].FirstOrDefault()
                ?? "100.00";

            if (!decimal.TryParse(amountText, out decimal amount))
            {
                return new BadRequestObjectResult(new
                {
                    success = false,
                    message = "Amount must be a valid number."
                });
            }

            // Dummy certificate number for the assignment prototype.
            string certificateNumber =
                $"DUMMY-TAX-{DateTime.UtcNow:yyyyMMddHHmmss}";

            var response = new
            {
                success = true,
                certificateNumber,
                donationReference,
                donorName,
                amount,
                currency = "ZAR",
                dateIssued = DateTime.UtcNow,
                message = "Dummy tax certificate generated successfully."
            };

            _logger.LogInformation(
                "Dummy tax certificate {CertificateNumber} generated for donation {DonationReference}.",
                certificateNumber,
                donationReference);

            return new OkObjectResult(response);
        }
    }
}
