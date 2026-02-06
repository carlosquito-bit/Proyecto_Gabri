using System.Text.RegularExpressions;
using System.Windows;

namespace Proyecto_Gabri
{
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtnRegistrar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = txtNombre.Text?.Trim();
            var email = txtEmail.Text?.Trim();
            var usuario = txtUsuarioRegistro.Text?.Trim();
            var clave = pwdContrasenaRegistro.Password;
            var confirmar = pwdConfirmarRegistro.Password;

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Rellena todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmail.Focus();
                return;
            }

            if (string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Introduce la contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                pwdContrasenaRegistro.Focus();
                return;
            }

            if (clave != confirmar)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                pwdConfirmarRegistro.Focus();
                return;
            }

            // Aquí integrar lógica real de registro (API, base de datos, etc.)
            MessageBox.Show("Registro correcto. Ya puedes iniciar sesión.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnVolverIniciar_Click(object sender, RoutedEventArgs e)
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
            try
            {
                // comprobación básica
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch
            {
                return false;
            }
        }
    }
}