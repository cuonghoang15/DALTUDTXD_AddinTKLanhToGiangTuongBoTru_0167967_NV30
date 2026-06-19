namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels
{
    /// <summary>
    /// ViewModel chính — được gán làm DataContext của Window.
    /// Mỗi Tab con nhận ViewModel riêng thông qua các property này.
    /// </summary>
    public class MainViewModel : BaseViewModel
    {
        public LanhToViewModel LanhTo { get; } = new LanhToViewModel();
        public GiangTuongViewModel GiangTuong { get; } = new GiangTuongViewModel();
        public BoTruViewModel BoTru { get; } = new BoTruViewModel();
    }
}
