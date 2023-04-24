using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Math;

namespace Task3;

internal class Program
{
    const int Variant = 12;

    const double XMin = 0;
    const double XMax = Variant;
    const double Step = 0.1 * Variant;

    const char Horizontal = (char)0x2500;
    const char Vertical = (char)0x2502;
    const char Cross = (char)0x253C;

    const int Height = 1080;
    const int Width = 1920;
    const int Margin = 100;

    const int Radius = 2;

    static void Main()
    {
        var values = new List<(double, double)>();
        for (double i = XMin; i <= XMax; i += Step)
        {
            values.Add((i, Function(i)));
        }

        PrintTable(values);
        AnalyzeData(values);
        GenerateGraph(values, Color.Black, Color.Red);
    }

    static double Function(double x)
        => 4 * Sin(Abs(x / 2)) + 9.1 * Cos(x - 1);

    static void PrintTable(List<(double, double)> values)
    {
        Console.WriteLine($"{"",3}{'x',-4}|{'y',5}");
        var delimiter = new StringBuilder()
            .Append(Horizontal, 7)
            .Append(Cross)
            .Append(Horizontal, 9)
            .ToString();

        Console.WriteLine(delimiter);
        foreach (var item in values)
        {
            Console.WriteLine("{0,6:0.00} {1} {2,8:0.0000}", item.Item1, Vertical, item.Item2);
        }
        Console.WriteLine();
    }

    static void AnalyzeData(List<(double, double)> values)
    {
        var query = values.Select(x => x.Item2).Where(x => x < -3 || x > 0.4);
        var count = query.Count();
        var product = query.Aggregate(1.0, (x, y) => x *= y);

        Console.WriteLine("y < -3 or y > 0.4");
        Console.WriteLine($"Count: {count}");
        Console.WriteLine($"Product: {product:F4}");
        Console.WriteLine();
    }

    static void GenerateGraph(List<(double, double)> values, Color gridColor, Color graphColor)
    {
        using var bitmap = new Bitmap(Width, Height);
        using var graphics = Graphics.FromImage(bitmap);
        using var pen = new Pen(Color.White, 1);
        using var font = new Font("Consolas", 12);

        graphics.FillRectangle(pen.Brush, new(0, 0, Width, Height));

        var xCount = values.Count;
        var xBaseLine = 0;

        var yMax = values.Max(x => x.Item2);
        yMax = Ceiling(Abs(yMax) + 1) * Sign(yMax);

        var yMin = values.Min(x => x.Item2);
        yMin = Ceiling(Abs(yMin)) * Sign(yMin);

        var yValStep = 1;
        var yCount = Abs(yMin - yMax);

        #region y Axis
        pen.Color = gridColor;

        var yStep = (Height - 2 * Margin) / yCount;
        var yBaseLine = Margin;

        for (int i = 0; i <= yCount; i++)
        {
            var y = Height - (Margin + (int)Truncate(yStep * i));
            if (i != yCount)
            {
                var yVal = yMin + yValStep * i;
                if (yVal == 0) xBaseLine = y;
                var str = yVal.ToString("F2");
                var strSize = graphics.MeasureString(str, font);
                graphics.DrawLine(pen, new(yBaseLine - 5, y), new(yBaseLine + 5, y));
                graphics.DrawString(str, font, pen.Brush, new PointF(yBaseLine - strSize.Width - 10, y - strSize.Height / 2));
                continue;
            }

            graphics.DrawLine(pen, new(yBaseLine, y), new(yBaseLine - 5, y + 5));
            graphics.DrawLine(pen, new(yBaseLine, y), new(yBaseLine + 5, y + 5));
        }

        graphics.DrawLine(pen, new(yBaseLine, Margin), new(yBaseLine, Margin + (int)Truncate(yStep * yCount)));
        #endregion

        #region x Axis
        var xStep = (double)(Width - 2 * Margin) / xCount;
        PointF? lastPoint = null;

        for (int i = 0; i <= xCount; i++)
        {
            var x = Margin + (int)Truncate(xStep * i);

            #region Arrow
            if (i == xCount)
            {
                graphics.DrawLine(pen, new(x, xBaseLine), new(x - 5, xBaseLine - 5));
                graphics.DrawLine(pen, new(x, xBaseLine), new(x - 5, xBaseLine + 5));
                continue;
            } 
            #endregion

            #region Point
            pen.Color = graphColor;
            var yVal = values[i].Item2;
            var y = xBaseLine - (int)(yStep * yVal);

            var point = new PointF(x, y);
            var halfSize = new Size(Radius, Radius);

            graphics.FillRectangle(pen.Brush, new RectangleF(point - halfSize, 2 * halfSize));

            if (lastPoint != null)
            {
                graphics.DrawLine(pen, lastPoint.Value, point);
            }
            lastPoint = point;
            #endregion

            #region Grid
            pen.Color = gridColor;
            var str = values[i].Item1.ToString("F2");
            var strSize = graphics.MeasureString(str, font);
            graphics.DrawLine(pen, new(x, xBaseLine - 5), new(x, xBaseLine + 5));
            if (i != 0 && strSize.Width < xStep) graphics.DrawString(str, font, pen.Brush, new PointF(x - strSize.Width / 2.0f, xBaseLine + strSize.Height / 2)); 
            #endregion
        }

        graphics.DrawLine(pen, new(Margin, xBaseLine), new(Margin + (int)Truncate(xStep * xCount), xBaseLine));
        #endregion

        bitmap.Save("graph.png", ImageFormat.Png);
    }
}