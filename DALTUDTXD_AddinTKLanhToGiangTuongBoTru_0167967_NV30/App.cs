using Autodesk.Revit.UI;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30
{
    
    public class App : IExternalApplication
    {
        // Tên hiển thị trên Ribbon
        private const string TabName = "DALTUXD";
        private const string PanelName = "Thiết kế KC";
        private const string ButtonName = "Lanh tô\nGiằng / Bổ trụ";
        private const string ButtonTip = "Mở công cụ thiết kế Lanh tô, Giằng tường và Bổ trụ";

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                // 1. Tạo Tab mới (bỏ qua nếu đã tồn tại)
                try { application.CreateRibbonTab(TabName); }
                catch {  }

                // 2. Tạo Panel trong Tab
                RibbonPanel panel = application.CreateRibbonPanel(TabName, PanelName);

                // 3. Đường dẫn DLL hiện tại
                string dllPath = Assembly.GetExecutingAssembly().Location;

                // 4. Tạo PushButton gọi ExternalCommand
                var btnData = new PushButtonData(
                    name: "btnLanhToGiangBoTru",
                    text: ButtonName,
                    assemblyName: dllPath,
                    className: "DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Commands.MoGiaoDienCommand"
                )
                {
                    ToolTip = ButtonTip,
                    LongDescription =
                        "Tính toán số lượng và khối lượng:\n" +
                        "• Lanh tô trên cửa\n" +
                        "• Giằng tường theo chiều cao\n" +
                        "• Bổ trụ theo chiều dài tường"
                };

                // 5. Gán icon nếu có (file icon 32x32 PNG đặt trong Resources/Icons/)
                try
                {
                    string iconPath = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(dllPath)!,
                        "Resources", "Icons", "icon_32.png");

                    if (System.IO.File.Exists(iconPath))
                        btnData.LargeImage = new BitmapImage(new Uri(iconPath));
                }
                catch {  }

                panel.AddItem(btnData);

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Lỗi khởi động Add-in", ex.Message);
                return Result.Failed;
            }
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
