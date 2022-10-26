using Avalonia;
using Avalonia.Controls;
using Avalonia.Rendering;

namespace Demo;

public static class Program
{
    private sealed class AvaloniaRendererFactory : IRendererFactory
    {
        public IRenderer Create(IRenderRoot root, IRenderLoop renderLoop) => root as IRenderer ?? new ImmediateRenderer(root);
    }

    public static void Main()
    {
        AvaloniaLocator.CurrentMutable.Bind<IRendererFactory>().ToConstant(new AvaloniaRendererFactory());

        AppBuilder appBuilder =
            AppBuilder.Configure<Application>()
                .UsePlatformDetect()
                .UseSkia()
                .SetupWithoutStarting();

        appBuilder.Instance!.RunWithMainWindow<Win>();
    }
}