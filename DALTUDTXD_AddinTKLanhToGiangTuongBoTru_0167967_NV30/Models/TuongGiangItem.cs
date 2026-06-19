namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Models
{
    /// <summary>
    /// Dữ liệu một đoạn tường dùng để tính giằng tường
    /// </summary>
    public class TuongGiangItem
    {
        public string TenTuong { get; set; } = string.Empty;
        /// <summary>Chiều dài tường (m)</summary>
        public double DaiTuong { get; set; }
        /// <summary>Chiều cao tường (m)</summary>
        public double CaoTuong { get; set; }
        /// <summary>Số giằng trên đoạn tường này</summary>
        public int SoGiang { get; set; }
    }
}
