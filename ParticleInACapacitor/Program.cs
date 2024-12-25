namespace ParticleInACapacitor;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.Write("Введите значение U: ");
            double u = double.Parse(Console.ReadLine());

            var simulation = new ElectronSimulation(u);
            if (simulation.Simulate())
            {
                Console.WriteLine($"Минимальное U для удержания электрона: {BinarySearch.FindThresholdVoltage():F5}");
                Console.WriteLine($"Время пролета t: {simulation.FinalTime:F2} сек");
                Console.WriteLine($"Скорость v: {simulation.FinalVelocity:F2} м/с");

                GraphPlotter.PlotAndSaveGraphs(
                    simulation.Times,
                    simulation.Positions,
                    simulation.Velocities,
                    simulation.Accelerations,
                    simulation.XCoords);

                Console.WriteLine("Графики сохранены в директории с кодом.");
            }
            else
            {
                Console.WriteLine("Электрон не удерживается при данном напряжении.");
            }
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Введите корректное числовое значение U.");
        }
    }
}