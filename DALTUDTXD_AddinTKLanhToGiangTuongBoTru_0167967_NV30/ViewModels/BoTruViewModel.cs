using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Autodesk.Revit.DB;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Commands;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Models;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels
{
     public class BoTruViewModel : BaseViewModel
    {
        // ── Thông số đầu vào ─────────────────────────────────────────────────

        private double _chieuDaiTuong = 5.0;
        public double ChieuDaiTuong
        {
            get => _chieuDaiTuong;
            set => SetField(ref _chieuDaiTuong, value);
        }
        private double _chieuCaoTuong = 3.6;
        public double ChieuCaoTuong
        {
            get => _chieuCaoTuong;
            set => SetField(ref _chieuCaoTuong, value);
        }

        private double _khoangCachBoTruToiDa = 4.0;
        public double KhoangCachBoTruToiDa
        {
            get => _khoangCachBoTruToiDa;
            set => SetField(ref _khoangCachBoTruToiDa, value);
        }

        // ── Kích thước bổ trụ ────────────────────────────────────────────────

        public List<string> DanhSachKichThuoc { get; } = new()
        {
            "220x220", "220x330", "330x330"
        };

        private string _kichThuocBoTru = "220x220";
        public string KichThuocBoTru
        {
            get => _kichThuocBoTru;
            set => SetField(ref _kichThuocBoTru, value);
        }

        // ── Danh sách tường ──────────────────────────────────────────────────

        public ObservableCollection<TuongBoTruItem> DanhSachTuong { get; } = new();
        // ── Kết quả ──────────────────────────────────────────────────────────

        private int _tongSoBoTru;
        public int TongSoBoTru
        {
            get => _tongSoBoTru;
            private set => SetField(ref _tongSoBoTru, value);
        }

        private string _tongTheTichBT = "0.000 m³";
        public string TongTheTichBT
        {
            get => _tongTheTichBT;
            private set => SetField(ref _tongTheTichBT, value);
        }

        // ── Commands ─────────────────────────────────────────────────────────

        public ICommand LayDuLieuRevitCommand { get; }
        public ICommand TinhToanCommand { get; }

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

                var walls = new FilteredElementCollector(doc)
                    .OfClass(typeof(Wall))
                    .Cast<Wall>();

                const double FeetToMeter = 0.3048;

                foreach (Wall wall in walls)
                {
                    double daiM = Math.Round(
                        (wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)?.AsDouble() ?? 0) * FeetToMeter, 2);
                    double caoM = Math.Round(
                        (wall.get_Parameter(BuiltInParameter.WALL_USER_HEIGHT_PARAM)?.AsDouble() ?? 0) * FeetToMeter, 2);

                    if (daiM <= 0 || caoM <= 0) continue;

                    string mark = wall.get_Parameter(BuiltInParameter.ALL_MODEL_MARK)?.AsString() ?? string.Empty;
                    string ten = string.IsNullOrWhiteSpace(mark) ? $"Tường [{wall.Id}]" : mark;

                    DanhSachTuong.Add(new TuongBoTruItem
                    {
                        TenTuong = ten,
                        ChieuDai = daiM,
                        ChieuCao = caoM
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

    }
}
