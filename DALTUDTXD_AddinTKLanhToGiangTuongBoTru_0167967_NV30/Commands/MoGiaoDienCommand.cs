using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Views;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Commands
{
    /// <summary>
    /// IExternalCommand — được Revit gọi khi người dùng click nút trên Ribbon.
    /// Lưu UIApplication/Document vào RevitContext rồi mở cửa sổ WPF.
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class MoGiaoDienCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Lưu context Revit để các ViewModel dùng
                RevitContext.UIApp = commandData.Application;
                RevitContext.Doc = commandData.Application.ActiveUIDocument.Document;

                // Mở cửa sổ WPF với MainViewModel
                var view = new DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30View();
                view.ShowDialog();

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }
        }
    }
}
