using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.SkiaSharp;

namespace ParticleInACapacitor;

public static class GraphPlotter
{
    public static void PlotAndSaveGraphs(
        List<double> times,
        List<double> positions,
        List<double> velocities,
        List<double> accelerations,
        List<double> xCoords)
    {
        PlotGraph(times, positions, "y(t)", "Time (s)", "Position (m)", "y_t.png");
        PlotGraph(times, velocities, "v_y(t)", "Time (s)", "Velocity (m/s)", "v_y_t.png");
        PlotGraph(times, accelerations, "a_y(t)", "Time (s)", "Acceleration (m/s²)", "a_y_t.png");
        PlotGraph(xCoords, positions, "y(x)", "X Position (m)", "Y Position (m)", "y_x.png");
    }

    private static void PlotGraph(
        List<double> xData,
        List<double> yData,
        string title,
        string xLabel,
        string yLabel,
        string fileName)
    {
        var plotModel = new PlotModel { Title = title };

        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = xLabel });
        plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = yLabel });

        var lineSeries = new LineSeries();
        for (int i = 0; i < xData.Count; i++)
        {
            lineSeries.Points.Add(new DataPoint(xData[i], yData[i]));
        }

        plotModel.Series.Add(lineSeries);

        string basePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        string outputPath = Path.Combine(basePath, fileName);
        
        using (var stream = File.Create(outputPath))
        {
            var exporter = new PngExporter { Width = 600, Height = 400};
            exporter.Export(plotModel, stream);
        }
    }
}