#nullable enable
using System;
using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Rendering;
using Avalonia.Skia;
using Avalonia.VisualTree;
using SkiaSharp;

namespace Demo;

public class Win : Window, IRenderer
{
    private static SKColor ColorWhite = new(255, 255, 255);
    private static SKColor ColorBlue = new(0, 0, 255);
    private static SKPaint PaintWhite = new() { Color = ColorWhite };

    public Win()
    {
        Width = 400;
        Height = 200;
        Render();
    }

    private void Render()
    {
        using (IRenderTarget renderTarget = CreateRenderTarget())
        using (IDrawingContextImpl drawingContext = renderTarget.CreateDrawingContext(null))
        using (ISkiaSharpApiLease lease = drawingContext.GetFeature<ISkiaSharpApiLeaseFeature>()!.Lease())
        {
            SKCanvas canvas = lease.SkCanvas;

            // Show white background
            canvas.DrawRect(new SKRect(0, 0, (float)Width, (float)Height), PaintWhite);
            
            canvas.Save();
            canvas.Scale((float)0.1);
            {
                SKPath pathClip = new SKPath();
                pathClip.AddCircle(1000, 1000, 400);
                canvas.ClipPath(pathClip);
            
                int widthImageAntiScale = 2000;
                int heightImageAntiScale = 2000;
                SKImage imageBlueRectangle = CreateSkImageRectangular(widthImageAntiScale, heightImageAntiScale, ColorBlue);
                canvas.DrawImage(imageBlueRectangle, new SKRect(0, 0, widthImageAntiScale, heightImageAntiScale));
            }
            canvas.Restore();
            
            canvas.SaveLayer();
            canvas.Translate(200, 0);
            canvas.Scale((float)0.1);
            {
                SKPath pathClip = new SKPath();
                pathClip.AddCircle(1000, 1000, 400);
                canvas.ClipPath(pathClip);
            
                int widthImageAntiScale = 2000;
                int heightImageAntiScale = 2000;
                SKImage imageBlueRectangle = CreateSkImageRectangular(widthImageAntiScale, heightImageAntiScale, ColorBlue);
                canvas.DrawImage(imageBlueRectangle, new SKRect(0, 0, widthImageAntiScale, heightImageAntiScale));
            }
            canvas.Restore();
        }
    }

    private static SKImage CreateSkImageRectangular(int width, int height, SKColor skColorFill)
    {
        int total = width * height;
        SKColor[] rgskColor = new SKColor[total];
        for (int i = 0; i < total; i++)
            rgskColor[i] = skColorFill;
        SKBitmap bmpk = new SKBitmap(width, height);
        bmpk.Pixels = rgskColor;
        return SKImage.FromBitmap(bmpk);
    }

    public void Dispose() { }
    public void AddDirty(IVisual visual) { }
    public IEnumerable<IVisual> HitTest(Point p, IVisual root, Func<IVisual, bool> filter) => Array.Empty<IVisual>();
    public IVisual? HitTestFirst(Point p, IVisual root, Func<IVisual, bool> filter) => null;
    public void RecalculateChildren(IVisual visual) { }
    public void Resized(Size size) { }
    public void Paint(Rect rect) { }
    public void Start() { }
    public void Stop() { }

    public bool DrawFps { get; set; }
    public bool DrawDirtyRects { get; set; }
    public event EventHandler<SceneInvalidatedEventArgs>? SceneInvalidated;
}
