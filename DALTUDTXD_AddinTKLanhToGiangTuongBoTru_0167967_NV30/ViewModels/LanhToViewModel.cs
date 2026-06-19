using Autodesk.Revit.DB;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Commands;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels
{
    public class LanhToViewModel : BaseViewModel
    {
        // ── Thông số đầu vào ─────────────────────────────────────────────────

        private double _chieuDaiGoi = 0.15;
        /// <summary>Chiều dài gối lanh tô mỗi bên (m)</summary>
        public double ChieuDaiGoi
        {
            get => _chieuDaiGoi;
            set => SetField(ref _chieuDaiGoi, value);
        }

        private double _chieuCaoLanhTo = 0.2;
        public double ChieuCaoLanhTo
        {
            get => _chieuCaoLanhTo;
            set => SetField(ref _chieuCaoLanhTo, value);
        }

        private double _chieuRongTietDien = 0.2;
        public double ChieuRongTietDien
        {
            get => _chieuRongTietDien;
            set => SetField(ref _chieuRongTietDien, value);
        }
        // ── Danh sách cửa ────────────────────────────────────────────────────

        public ObservableCollection<CuaItem> DanhSachCua { get; } = new();

        // ── Kết quả ──────────────────────────────────────────────────────────

        private int _soLuongLanhTo;
        public int SoLuongLanhTo
        {
            get => _soLuongLanhTo;
            private set => SetField(ref _soLuongLanhTo, value);
        }

        private string _tongChieuDai = "0.00 m";
        public string TongChieuDai
        {
            get => _tongChieuDai;
            private set => SetField(ref _tongChieuDai, value);
        }

        // ── Commands ─────────────────────────────────────────────────────────

        public ICommand LayDuLieuRevitCommand { get; }
        public ICommand TinhToanCommand { get; }

        public LanhToViewModel()
        {
            LayDuLieuRevitCommand = new RelayCommand(_ => LayDuLieuRevit());
            TinhToanCommand = new RelayCommand(_ => TinhToan(), _ => DanhSachCua.Count > 0);
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
                DanhSachCua.Clear();

                // Lấy Doors + Windows từ mô hình hiện tại
                var collector = new FilteredElementCollector(doc)
                    .OfClass(typeof(FamilyInstance))
                    .WherePasses(new LogicalOrFilter(
                        new ElementCategoryFilter(BuiltInCategory.OST_Doors),
                        new ElementCategoryFilter(BuiltInCategory.OST_Windows)));

                // Revit lưu đơn vị nội bộ là feet
                const double FeetToMeter = 0.3048;

                foreach (FamilyInstance fi in collector)
                {
                    // Lấy chiều rộng từ symbol (type) của cửa
                    double widthFeet =
                        fi.Symbol.get_Parameter(BuiltInParameter.DOOR_WIDTH)?.AsDouble()
                        ?? fi.Symbol.LookupParameter("Width")?.AsDouble()
                        ?? 0.0;

                    double widthM = Math.Round(widthFeet * FeetToMeter, 3);
                    if (widthM <= 0) continue;

                    string mark = fi.get_Parameter(BuiltInParameter.ALL_MODEL_MARK)?.AsString()
                                  ?? string.Empty;
                    string ten = string.IsNullOrWhiteSpace(mark)
                                  ? fi.Symbol.FamilyName
                                  : mark;

                    DanhSachCua.Add(new CuaItem
                    {
                        TenCua = ten,
                        RongCua = widthM,
                        DaiLanhTo = Math.Round(widthM + 2 * ChieuDaiGoi, 3)
                    });
                }

                if (DanhSachCua.Count == 0)
                    MessageBox.Show("Không tìm thấy cửa nào trong mô hình.",
                        "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                else
                    MessageBox.Show($"Đã lấy {DanhSachCua.Count} cửa từ Revit.",
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
            foreach (var cua in DanhSachCua)
                cua.DaiLanhTo = Math.Round(cua.RongCua + 2 * ChieuDaiGoi, 3);

            SoLuongLanhTo = DanhSachCua.Count;
            TongChieuDai = $"{DanhSachCua.Sum(c => c.DaiLanhTo):F2} m";

            var bk = DanhSachCua.ToList();
            DanhSachCua.Clear();
            foreach (var item in bk) DanhSachCua.Add(item);
        }
    }
}
