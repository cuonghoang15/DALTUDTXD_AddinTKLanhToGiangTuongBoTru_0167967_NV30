using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Commands;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Models;
namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels
{
   public class GiangTuongViewModel : BaseViewModel
    {
        // ── Thông số đầu vào ─────────────────────────────────────────────────

        private double _khoangCachGiang = 4.0;
        public double KhoangCachGiang
        {
            get => _khoangCachGiang;
            set => SetField(ref _khoangCachGiang, value);
        }

        private double _chieuCaoGiang = 150;
        public double ChieuCaoGiang
        {
            get => _chieuCaoGiang;
            set => SetField(ref _chieuCaoGiang, value);
        }

        private double _chieuRongGiang = 200;
        public double ChieuRongGiang
        {
            get => _chieuRongGiang;
            set => SetField(ref _chieuRongGiang, value);
        }
        // ── Quy tắc bố trí ───────────────────────────────────────────────────

        private bool _giangMoi4m = true;
        public bool GiangMoi4m
        {
            get => _giangMoi4m;
            set { if (SetField(ref _giangMoi4m, value) && value) KhoangCachGiang = 4.0; }
        }

        private bool _giangDinhTuong;
        public bool GiangDinhTuong
        {
            get => _giangDinhTuong;
            set => SetField(ref _giangDinhTuong, value);
        }

        private bool _giangTuyChinh;
        public bool GiangTuyChinh
        {
            get => _giangTuyChinh;
            set => SetField(ref _giangTuyChinh, value);
        }
        // ── Danh sách tường ──────────────────────────────────────────────────

        public ObservableCollection<TuongGiangItem> DanhSachTuong { get; } = new();

        // ── Kết quả ──────────────────────────────────────────────────────────

        private int _tongSoGiang;
        public int TongSoGiang
        {
            get => _tongSoGiang;
            private set => SetField(ref _tongSoGiang, value);
        }

        private string _tongChieuDaiGiang = "0.00 m";
        public string TongChieuDaiGiang
        {
            get => _tongChieuDaiGiang;
            private set => SetField(ref _tongChieuDaiGiang, value);
        }
        

        // ── Lấy dữ liệu từ Revit API ─────────────────────────────────────────

        private void LayDuLieuRevit()
        {
            var doc = RevitContext.Doc;
            if (doc == null)
            {
                MessageBox.Show("Không tìm thấy Document Revit!", "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                DanhSachTuong.Clear();

                // Lấy tất cả tường (Wall) trong mô hình
                var walls = new FilteredElementCollector(doc)
                    .OfClass(typeof(Wall))
                    .Cast<Wall>();

                const double FeetToMeter = 0.3048;

                foreach (Wall wall in walls)
                {
                    // Chiều dài tường
                    double daiF = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)
                                      ?.AsDouble() ?? 0.0;
                    // Chiều cao tường
                    double caoF = wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM)
                                      ?.AsDouble()
                               ?? wall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET)
                                      ?.AsDouble()
                               ?? 0.0;

                    double daiM = Math.Round(daiF * FeetToMeter, 2);
                    double caoM = Math.Round(caoF * FeetToMeter, 2);

                    if (daiM <= 0 || caoM <= 0) continue;

                    // Tên tường: Mark nếu có, không thì dùng ElementId
                    string mark = wall.get_Parameter(BuiltInParameter.ALL_MODEL_MARK)
                                      ?.AsString() ?? string.Empty;
                    string ten = string.IsNullOrWhiteSpace(mark)
                                  ? $"Tường [{wall.Id}]"
                                  : mark;

                    DanhSachTuong.Add(new TuongGiangItem
                    {
                        TenTuong = ten,
                        DaiTuong = daiM,
                        CaoTuong = caoM
                    });
                }

                if (DanhSachTuong.Count == 0)
                    MessageBox.Show("Không tìm thấy tường nào trong mô hình.",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show($"Đã lấy {DanhSachTuong.Count} tường từ Revit.",
                        "Lấy dữ liệu Revit", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi Revit API:\n" + ex.Message, "Lỗi",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // ── Tính toán ────────────────────────────────────────────────────────

        private void TinhToan()
        {
            int tongGiang = 0;
            double tongChieuDai = 0;

            foreach (var tuong in DanhSachTuong)
            {
                int soGiang = GiangDinhTuong
                    ? 1
                    : (int)Math.Ceiling(tuong.CaoTuong / KhoangCachGiang);

                tuong.SoGiang = soGiang;
                tongGiang += soGiang;
                tongChieuDai += soGiang * tuong.DaiTuong;
            }

            TongSoGiang = tongGiang;
            TongChieuDaiGiang = $"{tongChieuDai:F2} m";

            var bk = DanhSachTuong.ToList();
            DanhSachTuong.Clear();
            foreach (var item in bk) DanhSachTuong.Add(item);
        }

    }
}
