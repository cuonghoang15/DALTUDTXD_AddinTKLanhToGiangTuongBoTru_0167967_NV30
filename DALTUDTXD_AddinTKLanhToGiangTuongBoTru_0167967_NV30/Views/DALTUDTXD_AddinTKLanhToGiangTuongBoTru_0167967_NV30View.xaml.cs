using System.Windows;
using DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.ViewModels;

namespace DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30.Views
{
    /// <summary>
    /// Code-behind cho cửa sổ chính.
    /// DataContext được gán là MainViewModel — mỗi Tab con bind đến sub-ViewModel tương ứng.
    /// </summary>
    public partial class DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30View : Window
    {
        public DALTUDTXD_AddinTKLanhToGiangTuongBoTru_0167967_NV30View()
        {
            InitializeComponent();
            // Gán MainViewModel làm DataContext của toàn bộ Window
            DataContext = new MainViewModel();
        }

    }
}
