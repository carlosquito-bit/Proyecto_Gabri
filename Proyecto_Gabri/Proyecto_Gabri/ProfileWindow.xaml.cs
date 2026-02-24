using System;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Gabri
{
    public partial class ProfileWindow : Window
    {
        public ProfileWindow()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar ProfileWindow:\n{ex.ToString()}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                throw;
            }
        }

        private void BtnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Perfil guardado.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnUpdatePassword_Click(object sender, RoutedEventArgs e)
        {
            // Obtain the PasswordBox controls by name at runtime in case the generated fields are not present
            var pwdNewBox = this.FindName("pwdNew") as PasswordBox;
            var pwdConfirmBox = this.FindName("pwdConfirm") as PasswordBox;

            if (pwdNewBox == null || pwdConfirmBox == null)
            {
                MessageBox.Show("Errores en la ventana: controles de password no encontrados.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pwdNewBox.Password != pwdConfirmBox.Password)
            {
                MessageBox.Show("Las contraseñas no coinciden.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(pwdNewBox.Password))
            {
                MessageBox.Show("Introduce la nueva contraseña.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Contraseña actualizada.", "OK", MessageBoxButton.OK, MessageBoxImage.Information);
            pwdNewBox.Clear();
            pwdConfirmBox.Clear();
        }

        private void ThemeRadio_Checked(object sender, RoutedEventArgs e)
        {
            // placeholder for theme changes
            // you can implement real theme switching here
        }

        private void ToggleNotifications_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Notificaciones activadas.", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ToggleNotifications_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Notificaciones desactivadas.", "Informacion", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}