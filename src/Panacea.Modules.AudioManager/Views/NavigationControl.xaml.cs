using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Panacea.Modules.AudioManager.Views
{
    /// <summary>
    /// Interaction logic for NavigationControl.xaml
    /// </summary>
    public partial class NavigationControl : UserControl
    {
        public NavigationControl()
        {
            InitializeComponent();
            popup.CustomPopupPlacementCallback = new CustomPopupPlacementCallback(PlacePopup);
        }

        public CustomPopupPlacement[] PlacePopup(Size popupSize,
                                           Size targetSize,
                                           Point offset)
        {
            var p = VolumeButton.PointToScreen(new Point(0, 0));
            var placement1 =
               new CustomPopupPlacement(new Point(p.X + VolumeButton.ActualWidth / 2 - popupSize.Width / 2, p.Y - popupSize.Height), PopupPrimaryAxis.Vertical);

            var placement2 =
                new CustomPopupPlacement(new Point(10, 20), PopupPrimaryAxis.Horizontal);

            var ttplaces =
                    new CustomPopupPlacement[] { placement1 };
            return ttplaces;
        }

        private void Popup_Opened(object sender, EventArgs e)
        {
            VolUpButton.Focus();
        }
    }
}
