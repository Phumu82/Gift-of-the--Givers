namespace GiftOfTheGivers.Helpers;

public static class DonationHelper
{
    public static string FormatDonationAmount(decimal amount)
    {
        return $"R {amount:N2}";
    }

    public static string GenerateDonationSummary(
        string donorName,
        string donationReference,
        decimal amount)
    {
        return $"{donorName} - Donation {donationReference}: {FormatDonationAmount(amount)}";
    }
}