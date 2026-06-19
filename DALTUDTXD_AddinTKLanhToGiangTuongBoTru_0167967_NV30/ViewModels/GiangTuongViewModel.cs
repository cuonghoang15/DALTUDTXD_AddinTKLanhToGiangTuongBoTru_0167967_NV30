using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
