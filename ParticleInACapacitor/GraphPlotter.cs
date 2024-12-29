using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace ParticleInACapacitor;

public static class GraphPlotter
    {
        // Метод PlotAndSaveGraphs создает все графики для данных симуляции и сохраняет их в файлы.
        public static void PlotAndSaveGraphs(
            IEnumerable<double> times, // Временные точки t.
            IEnumerable<double> positions, // Позиции y(t).
            IEnumerable<double> velocities, // Скорости v_y(t).
            IEnumerable<double> accelerations, // Ускорения a_y(t).
            IEnumerable<double> xCoords // Координаты x(t).
        )
        {
            // Создаем график зависимости y(t) — положение от времени.
            Plot(times, positions, "y(t)", "Время (с)", "Позиция (м)", "y_t.png");

            // Создаем график зависимости v_y(t) — скорость от времени.
            Plot(times, velocities, "v_y(t)", "Время (с)", "Скорость (м/с)", "v_y_t.png");

            // Создаем график зависимости a_y(t) — ускорение от времени.
            Plot(times, accelerations, "a_y(t)", "Время (с)", "Ускорение (м/с²)", "a_y_t.png");

            // Создаем график зависимости y(x) — положение y от координаты x.
            Plot(xCoords, positions, "y(x)", "Позиция X (м)", "Позиция Y (м)", "y_x.png");
        }

        // Вспомогательный метод Plot создает отдельный график и сохраняет его в файл.
        private static void Plot(
            IEnumerable<double> xData, // Данные по оси X.
            IEnumerable<double> yData, // Данные по оси Y.
            string title, // Заголовок графика.
            string xLabel, // Подпись оси X.
            string yLabel, // Подпись оси Y.
            string fileName // Имя файла для сохранения графика.
        )
        {
            // Создаем объект графика с заданным заголовком.
            var plotModel = new PlotModel { Title = title };

            // Добавляем ось X с подписью.
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel });

            // Добавляем ось Y с подписью.
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel });

            // Создаем серию линий (график).
            var lineSeries = new LineSeries();

            // Заполняем серию точками графика.
            var xArray = xData.ToArray();
            var yArray = yData.ToArray();
            
            for (int i = 0; i < xArray.Length; i++)
            {
                lineSeries.Points.Add(new DataPoint(xArray[i], yArray[i])); // Добавляем точку на график.
            }

            // Добавляем серию на модель графика.
            plotModel.Series.Add(lineSeries);

            // Определяем путь для сохранения файла.
            string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName; // Базовая директория.
            string outputPath = Path.Combine(basePath, fileName); // Полный путь к файлу.

            // Сохраняем график как PNG.
            using (var stream = File.Create(outputPath))
            {
                var exporter = new PngExporter { Width = 600, Height = 400 }; // Задаем параметры изображения.
                exporter.Export(plotModel, stream); // Экспортируем график в файл.
            }
        }
    }