namespace Puzzle.Lib.Assistant.Extensions;

/// <summary>
/// Provides extension methods for calculating prices and discounts.
/// </summary>
public static class FinanceExtensions
{
    /// <summary>
    /// Calculates the price with VAT included for the given price and VAT rate.
    /// </summary>
    /// <param name="price">The original price to calculate the new price from.</param>
    /// <param name="vatRate">The VAT rate to apply to the price.</param>
    /// <returns>The new price with VAT included, rounded up to the nearest integer.</returns>
    public static int ToCalculateVatPrice(this decimal price, int vatRate)
    {
        if (Math.Sign(price) != 1 || Math.Sign(vatRate) != 1)
            return default;

        return (int)Math.Ceiling(price + price * vatRate / 100);
    }

    /// <summary>
    /// Calculates the discount rate for the given main price and sales price.
    /// </summary>
    /// <param name="mainPrice">The original price to calculate the discount from.</param>
    /// <param name="salesPrice">The discounted price.</param>
    /// <returns>The discount rate as a percentage, rounded to two decimal places.</returns>
    public static int ToDiscountRate(this decimal mainPrice, decimal salesPrice)
    {
        if (Math.Sign(mainPrice) != 1 || Math.Sign(salesPrice) != 1)
            return default;

        decimal discountRate = Math.Round((mainPrice - salesPrice) / mainPrice * 100, 2);
        if (default(int) > discountRate)
            return default;

        return (int)discountRate;
    }
}
