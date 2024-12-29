namespace ParticleInACapacitor;

// Класс BinarySearch выполняет двоичный поиск минимального напряжения U, при котором электрон не успеет вылететь из конденсатора.
public static class BinarySearch
{
    public static double FindThresholdVoltage()
    {
        double left = 2; // Нижняя граница поиска.
        double right = 100; // Верхняя граница поиска.
        
        while (right - left > 0.00001) // Условие с заданной точностью.
        {
            double mid = (left + right) / 2; // Среднее значение между левым и правым.
            var simulation = new ElectronSimulation(mid); // Создаем симуляцию с текущим напряжением mid.
            if (simulation.Simulate()) // Если электрон удерживается в конденсаторе.
                right = mid; // Сдвигаем верхнюю границу.
            else
                left = mid; // Сдвигаем нижнюю границу.
        }

        return right; // Возвращаем минимальное напряжение.
    }
}