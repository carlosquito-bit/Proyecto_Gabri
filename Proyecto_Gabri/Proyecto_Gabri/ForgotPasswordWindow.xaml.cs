using System.Text.RegularExpressions;
using System.Windows;

namespace Proyecto_Gabri
{
    public partial class ForgotPasswordWindow : Window
    {
        public ForgotPasswordWindow()
        {
            InitializeComponent();
        }

        private void BtnEnviar_Click(object sender, RoutedEventArgs e)
        {
            var email = txtEmailForgot.Text?.Trim();

            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Introduce el correo electrónico.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmailForgot.Focus();
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Correo no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmailForgot.Focus();
                return;
            }

            // Aquí integrar lógica real (envío de correo, API, etc.)
            MessageBox.Show("Si el correo existe en nuestro sistema, recibirás instrucciones para recuperar la contraseña.", "Enviado", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var MainWindow = new MainWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                MainWindow.Show();
                Application.Current.MainWindow = MainWindow;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir ventana de registro:\n{ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}