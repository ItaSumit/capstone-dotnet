namespace Capstone.Flight.Services;

public interface IPricingStrategy
{
    double getPrice(double basePrice);
}

public class HikedPricingStrategy : IPricingStrategy
{
    public double getPrice(double basePrice)
    {
        return basePrice * 1.5;
    }
}

public class NormalPricingStrategy : IPricingStrategy
{
    public double getPrice(double basePrice)
    {
        return basePrice;
    }
}