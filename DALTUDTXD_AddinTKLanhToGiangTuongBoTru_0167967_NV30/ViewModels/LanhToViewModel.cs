using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
