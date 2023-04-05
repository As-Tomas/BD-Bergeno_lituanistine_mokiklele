using BD_Bergeno_lituanistine_mokiklele.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BD_Bergeno_lituanistine_mokiklele.ViewModels.Dashboard 
    {
    public partial class DashboardPageViewModel : BaseViewModel {
        public DashboardPageViewModel() {
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();
        }
    }
}
