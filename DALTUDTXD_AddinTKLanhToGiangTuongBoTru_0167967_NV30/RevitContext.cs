using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30
{
    /// <summary>
    /// Lưu trữ tham chiếu tĩnh đến Revit UIApplication và Document
    /// để các ViewModel có thể truy cập Revit API mà không cần truyền tham số.
    /// </summary>
    public static class RevitContext
    {
        public static UIApplication? UIApp { get; set; }
        public static Document? Doc { get; set; }
    }
}
