namespace PearlCalculatorCP.ViewModels
{
    public interface IWindowViewModelScale
    {
        double WindowScale { get; set; }
    }

    public static class WindowViewModelScaleExtension
    {
        public static void ApplyScale(this IWindowViewModelScale vm)
        {
            if (AppRuntimeSettings.Settings.TryGetValue("scale", out var s))
                vm.WindowScale = (double)s;
        }
    }
}