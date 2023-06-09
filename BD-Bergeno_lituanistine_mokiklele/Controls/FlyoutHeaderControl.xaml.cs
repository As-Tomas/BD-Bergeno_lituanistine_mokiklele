namespace BD_Bergeno_lituanistine_mokiklele.Controls;

public partial class FlyoutHeaderControl : StackLayout {
    public FlyoutHeaderControl() {
        InitializeComponent();

        if (App.UserDetails != null) {
            lblUserName.Text = App.UserDetails.FirstName + " " + App.UserDetails.LastName;
            lblUserEmail.Text = App.UserDetails.Email;
            lblUserRole.Text = App.UserDetails.Role;
        }
    }
}