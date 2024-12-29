namespace ParticleInACapacitor;

// Класс ElectronSimulation моделирует движение электрона внутри конденсатора.
public class ElectronSimulation
{
    private readonly double _u; // Напряжение, приложенное к обкладкам.

    public List<double> Times { get; private set; } = new(); // Массив времени.
    
    public List<double> Positions { get; private set; } = new(); // Массив положений y(t).
    
    public List<double> Velocities { get; private set; } = new(); // Массив скоростей vy(t).
    
    public List<double> Accelerations { get; private set; } = new(); // Массив ускорений ay(t).
    
    public List<double> XCoords { get; private set; } = new(); // Массив координат x(t).

    public double FinalTime { get; private set; } // Итоговое время.
    
    public double FinalVelocity { get; private set; } // Итоговая скорость.

    public ElectronSimulation(double u)
    {
        _u = u; // Устанавливаем напряжение для симуляции.
    }

    // Основной метод моделирования движения электрона.
    public bool Simulate()
    {
        Times.Clear();
        Positions.Clear();
        Velocities.Clear();
        Accelerations.Clear();
        XCoords.Clear();

        double d = Constants.Radius2 - Constants.Radius1; // Расстояние между обкладками.
        // Формула для расчета коэффициента ускорения:
        // aCoef = (q * U) / (m * ln(R2 / R1))
        double aCoef = Constants.ElectronCharge * _u /
                       (Constants.ElectronMass * Math.Log(Constants.Radius2 / Constants.Radius1));

        double pos = d / 2; // Начальное положение (по центру между обкладками).
        double r = pos + Constants.Radius1; // Текущее расстояние от центра.
        double a = aCoef / r; // Начальное ускорение.
        double vy = 0; // Начальная скорость по y.
        double t = 0; // Начальное время.
        double dt = Constants.TubeLength / (Constants.InitialVelocity * 1000); // Шаг по времени.

        // Основной цикл моделирования.
        for (int q = 0; q < 1000; q++)
        {
            Times.Add(t); // Сохраняем текущее время.
            Positions.Add(pos); // Сохраняем текущее положение.
            Velocities.Add(vy); // Сохраняем текущую скорость.
            Accelerations.Add(a); // Сохраняем текущее ускорение.
            XCoords.Add(Constants.InitialVelocity * t); // Сохраняем координату x.

            // Проверяем, достиг ли электрон обкладки.
            if (pos <= 0)
            {
                pos = 0; // Если достиг, останавливаем движение.
                break;
            }

            // Обновляем параметры движения электрона:
            pos = Math.Max(pos - vy * dt - a * Math.Pow(dt, 2) / 2, 0);
            vy += a * dt;
            r = pos + Constants.Radius1;
            a = aCoef / r;
            t += dt;

            // Прерываем цикл, если вышли за пределы длины трубы.
            if (t > Constants.TubeLength / Constants.InitialVelocity)
                break;
        }

        // Итоговые значения.
        FinalTime = t * 100000000; // Преобразуем в наносекунды.
        FinalVelocity = Math.Sqrt(Math.Pow(Constants.InitialVelocity, 2) + Math.Pow(vy, 2)); // Итоговая скорость.
        
        return pos == 0; // Возвращает true, если электрон остановился.
    }
}