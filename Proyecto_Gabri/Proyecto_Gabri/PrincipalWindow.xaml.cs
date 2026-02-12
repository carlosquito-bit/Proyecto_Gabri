using System.Windows;

namespace Proyecto_Gabri
{
    public partial class PrincipalWindow : Window
    {
        public PrincipalWindow()
        {
            InitializeComponent();
        }

        private void BtnLoginRegister_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
                MessageBox.Show($"Error al abrir la ventana de inicio:/registro:\n{ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}