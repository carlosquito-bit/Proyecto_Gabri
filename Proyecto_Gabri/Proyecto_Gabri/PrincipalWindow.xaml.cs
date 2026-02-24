using System.Windows;

namespace Proyecto_Gabri
{
    public partial class PrincipalWindow : Window
    {
        private bool _isAuthenticated = false;

        public PrincipalWindow()
        {
            InitializeComponent();
        }

        // New ctor to indicate whether the user is authenticated
        public PrincipalWindow(bool isAuthenticated) : this()
        {
            _isAuthenticated = isAuthenticated;
            UpdateLoginButtonState();
        }

        private void UpdateLoginButtonState()
        {
            try
            {
                if (_isAuthenticated && btnLoginRegister != null)
                {
                    btnLoginRegister.Content = "Mi Perfil";
                }
            }
            catch
            {
                // ignore
            }
        }

        private void BtnLoginRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // If the user is authenticated, open the Profile window
                if (_isAuthenticated)
                {
                    var profile = new ProfileWindow()
                    {
                        WindowStartupLocation = WindowStartupLocation.CenterScreen
                    };
                    profile.ShowDialog();
                    return;
                }

                var main = new MainWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };
                main.Show();
                Application.Current.MainWindow = main;
                this.Close();
            }
            catch (System.Exception ex)
            {
                // Show full exception details to help debugging
                MessageBox.Show($"Error al abrir la ventana de inicio/registro:\n{ex.ToString()}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAcceptCookies_Click(object sender, RoutedEventArgs e)
        {
            // Hide both the overlay popup and the bottom cookie bar if present
            try
            {
                if (CookiePopupOverlay != null)
                    CookiePopupOverlay.Visibility = Visibility.Collapsed;

                if (CookieBar != null)
                    CookieBar.Visibility = Visibility.Collapsed;
            }
            catch
            {
                // ignore any issues updating visibility
            }
        }

        private void BtnCookiesInfo_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Usamos cookies para personalizar contenido y analizar el tráfico. Puedes cambiar tus preferencias en la configuración.", "Información sobre cookies", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}