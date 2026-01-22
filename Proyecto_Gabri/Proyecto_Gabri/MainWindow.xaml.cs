using System.Windows;

namespace Proyecto_Gabri
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnIniciarSesion_Click(object sender, RoutedEventArgs e)
        {
            var usuario = txtUsuario.Text?.Trim();
            var clave = pwdContrasena.Password;

            if (string.IsNullOrEmpty(usuario))
            {
                MessageBox.Show("Introduce el usuario.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtUsuario.Focus();
                return;
            }

            if (string.IsNullOrEmpty(clave))
            {
                MessageBox.Show("Introduce la contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                pwdContrasena.Focus();
                return;
            }

            // Aquí puedes integrar la lógica real de autenticación (API, base de datos, etc.)
            // Por ahora simulamos un inicio de sesión correcto si usuario == "admin" y clave == "1234"
            if (usuario == "admin" && clave == "1234")
            {
                MessageBox.Show("Autenticación correcta. Bienvenido.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
                // Navegar a la siguiente ventana o cargar la sesión
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}