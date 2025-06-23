using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Interfaces;
using Service.Models;
using Service.Services;

namespace Desktop.States.Clientes
{
    public class AddState : IFormState
    {
        private ClientesView _form;

        public AddState(ClientesView form)
        {
            _form = form;
        }

        public void OnCancelar()
        {
            _form.txtNombre.Clear();
            _form.txtDireccion.Clear();
            _form.txtTelefonos.Clear();
            _form.dateTimeFechaNacimiento.Value = DateTime.Now;
            _form.comboLocalidades.SelectedIndex = -1; // Reset to no selection
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }

        public async void OnGuardar()
        {
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre del cliente es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var cliente = new Cliente
            {
                Nombre = _form.txtNombre.Text,
                Direccion = _form.txtDireccion.Text,
                Telefonos = _form.txtTelefonos.Text,
                LocalidadId = (int)_form.comboLocalidades.SelectedValue,
                FechaNacimiento = _form.dateTimeFechaNacimiento.Value
            };
            await _form.clienteService.AddAsync(cliente);

            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            _form.txtNombre.Text = string.Empty;
            _form.txtDireccion.Text = string.Empty;
            _form.txtTelefonos.Text = string.Empty;
            _form.dateTimeFechaNacimiento.Value = DateTime.Now;
            //_form.comboLocalidades.SelectedIndex = 0; // Reset to first item

            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
            _form.tabControl.Selecting += (sender, e) =>
            {
                if (e.TabPage == _form.tabPageLista)
                {
                    e.Cancel = true; // Prevent switching to the Add/Edit tab
                }
            };
            return Task.CompletedTask;
        }

        public void OnAgregar() 
        {
            UpdateUI();
        }

        public void OnEditar() {}
        public void OnBuscar() {}
        public void OnEliminar() {}
        public void OnSalir() {}
    }
}
