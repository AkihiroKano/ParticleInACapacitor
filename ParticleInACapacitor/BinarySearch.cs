namespace ParticleInACapacitor;

public static class BinarySearch
{
    public static double FindThresholdVoltage()
    {
        double left = 2;
        double right = 100;
        while (right - left > 0.00001)
        {
            double mid = (left + right) / 2;
            var simulation = new ElectronSimulation(mid);
            if (simulation.Simulate())
                right = mid;
            else
                left = mid;
        }
        return right;
    }
}