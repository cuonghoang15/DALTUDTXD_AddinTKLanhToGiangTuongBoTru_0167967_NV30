namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Models
{
    /// <summary>
    /// Dữ liệu một cửa (dùng trong danh sách lanh tô)
    /// </summary>
    public class CuaItem
    {
        public string TenCua { get; set; } = string.Empty;
        /// <summary>Chiều rộng cửa (m)</summary>
        public double RongCua { get; set; }
        /// <summary>Chiều dài lanh tô = rộng cửa + 2 × chiều dài gối (m)</summary>
        public double DaiLanhTo { get; set; }
    }
}
