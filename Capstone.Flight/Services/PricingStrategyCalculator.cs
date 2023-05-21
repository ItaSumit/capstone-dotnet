namespace Capstone.Flight.Services;

public class PricingStrategyCalculator
{
    private readonly HikedPricingStrategy _hikedPricingStrategy;
    private readonly NormalPricingStrategy _normalPricingStrategy;

    public PricingStrategyCalculator(HikedPricingStrategy hikedPricingStrategy, NormalPricingStrategy normalPricingStrategy)
    {
        _hikedPricingStrategy = hikedPricingStrategy;
        _normalPricingStrategy = normalPricingStrategy;
    }

    public double Calculate(float occupancy, double basePrice)
    {
        if (occupancy >= 50)
        {
            return _hikedPricingStrategy.getPrice(basePrice);
        }
        else
        {
            return _normalPricingStrategy.getPrice(basePrice);
        }
    }
    
}