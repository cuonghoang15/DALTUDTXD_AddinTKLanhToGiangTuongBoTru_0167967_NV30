using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
