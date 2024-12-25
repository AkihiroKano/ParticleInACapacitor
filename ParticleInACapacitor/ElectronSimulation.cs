namespace ParticleInACapacitor;

public class ElectronSimulation
{
    private readonly double _u; // Напряжение

    public List<double> Times { get; private set; } = new();
    public List<double> Positions { get; private set; } = new();
    public List<double> Velocities { get; private set; } = new();
    public List<double> Accelerations { get; private set; } = new();
    public List<double> XCoords { get; private set; } = new();

    public double FinalTime { get; private set; }
    public double FinalVelocity { get; private set; }

    public ElectronSimulation(double u)
    {
        _u = u;
    }

    public bool Simulate()
    {
        Times.Clear();
        Positions.Clear();
        Velocities.Clear();
        Accelerations.Clear();
        XCoords.Clear();

        double d = Constants.Radius2 - Constants.Radius1;
        double aCoef = Constants.ElectronCharge * _u /
                       (Constants.ElectronMass * Math.Log(Constants.Radius2 / Constants.Radius1));
        double pos = d / 2;
        double r = pos + Constants.Radius1;
        double a = aCoef / r;
        double vy = 0;
        double t = 0;
        double dt = Constants.TubeLength / (Constants.InitialVelocity * 1000);

        for (int q = 0; q < 1000; q++)
        {
            Times.Add(t);
            Positions.Add(pos);
            Velocities.Add(vy);
            Accelerations.Add(a);
            XCoords.Add(Constants.InitialVelocity * t);

            // Проверяем остановку электрона
            if (pos <= 0)
            {
                pos = 0;
                break;
            }

            pos = Math.Max(pos - vy * dt - a * Math.Pow(dt, 2) / 2, 0);
            vy += a * dt;
            r = pos + Constants.Radius1;
            a = aCoef / r;
            t += dt;

            // Ограничиваем цикл максимальной длиной трубы
            if (t > Constants.TubeLength / Constants.InitialVelocity)
                break;
        }

        FinalTime = t*100000000;
        FinalVelocity = Math.Sqrt(Math.Pow(Constants.InitialVelocity, 2) + Math.Pow(vy, 2));
        return pos == 0;
    }
}