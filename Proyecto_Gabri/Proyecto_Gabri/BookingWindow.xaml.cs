using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Proyecto_Gabri
{
    public partial class BookingWindow : Window
    {
        public BookingWindow()
        {
            InitializeComponent();
        }

        private void BtnReservar_Click(object sender, RoutedEventArgs e)
        {
            var nombre = txtNombreBooking.Text?.Trim();
            var email = txtEmailBooking.Text?.Trim();
            var telefono = txtTelefono.Text?.Trim();
            var fecha = dpFecha.SelectedDate;
            var hora = cbHora.Text?.Trim();
            var tipo = (cbTipoHabitacion.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? string.Empty;
            var huespedesText = txtHuespedes.Text?.Trim();

            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(email) || !fecha.HasValue || string.IsNullOrEmpty(hora) || string.IsNullOrEmpty(tipo))
            {
                MessageBox.Show("Rellena todos los campos obligatorios (nombre, correo, fecha, hora, tipo de habitación).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Correo no válido.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtEmailBooking.Focus();
                return;
            }

            if (fecha.Value.Date < DateTime.Today)
            {
                MessageBox.Show("La fecha no puede ser anterior a hoy.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                dpFecha.Focus();
                return;
            }

            if (!int.TryParse(huespedesText, out int huespedes) || huespedes <= 0)
            {
                MessageBox.Show("Introduce un número de huéspedes válido (> 0).", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                txtHuespedes.Focus();
                return;
            }

            // Aquí integrar lógica real de reserva (API, base de datos, etc.)
            var resumen = $"Reserva realizada:\n\nNombre: {nombre}\nCorreo: {email}\nTeléfono: {telefono}\nFecha: {fecha:dd/MM/yyyy}\nHora: {hora}\nHabitación: {tipo}\nHuéspedes: {huespedes}";
            MessageBox.Show(resumen, "Reserva confirmada", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnVolver_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mainWindow = new MainWindow()
                {
                    WindowStartupLocation = WindowStartupLocation.CenterScreen
                };

                mainWindow.Show();
                Application.Current.MainWindow = mainWindow;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir la ventana principal:\n{ex.Message}", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            try
            {
                return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            }
            catch
            {
                return false;
            }
        }
    }
}