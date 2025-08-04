using CommunityToolkit.Maui.Core;
using System.Diagnostics;

namespace MCT_NoCameraCrash;

public partial class MainPage : ContentPage
{
    static int cameraCheckCount = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    private async void CameraCheckButton_Clicked(object sender, EventArgs e)
    {
        await HasAvailableCameras();
        CameraCheckButton.Text = $"Camera check count: {cameraCheckCount}";
    }

    public static async Task<bool> HasAvailableCameras()
    {
        try
        {
            ICameraProvider? cameraProvider = IPlatformApplication.Current?.Services?.GetRequiredService<ICameraProvider>();
            if (cameraProvider is null)
            {
                return false;
            }

            await cameraProvider.RefreshAvailableCameras(CancellationToken.None);
            if (cameraProvider.AvailableCameras is null)
            {
                return false;
            }

            cameraCheckCount = cameraProvider.AvailableCameras.Count;

            return cameraProvider.AvailableCameras.Count > 0;
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex.Message, nameof(MainPage));
            return false;
        }
    }
}


