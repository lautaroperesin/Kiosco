using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;

namespace Desktop.States.Clientes
{
    public class EditState : IFormState
    {
        private ClientesView _form;

        public EditState(ClientesView form)
        {
            _form = form;
        }
        public void OnCancelar()
        {
            _form.txtNombre.Clear();
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }

        public async void OnGuardar()
        {
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre de la cliente es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _form.clienteCurrent.Nombre = _form.txtNombre.Text;
            _form.clienteCurrent.Direccion = _form.txtDireccion.Text;
            _form.clienteCurrent.Telefonos = _form.txtTelefonos.Text;
            _form.clienteCurrent.LocalidadId = (int)_form.comboLocalidades.SelectedValue;
            _form.clienteCurrent.FechaNacimiento = _form.dateTimeFechaNacimiento.Value;
            await _form.clienteService.UpdateAsync(_form.clienteCurrent);

            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            _form.clienteCurrent = _form.dataGridClientes.CurrentRow?.DataBoundItem as Cliente;
            _form.txtNombre.Text = _form.clienteCurrent.Nombre;
            _form.txtDireccion.Text = _form.clienteCurrent.Direccion;
            _form.txtTelefonos.Text = _form.clienteCurrent.Telefonos;
            _form.dateTimeFechaNacimiento.Value = _form.clienteCurrent.FechaNacimiento;
            _form.comboLocalidades.SelectedValue = _form.clienteCurrent.LocalidadId;

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

        public void OnEditar() 
        {
            UpdateUI();
        }
        public void OnAgregar() {}
        public void OnBuscar() { }
        public void OnEliminar() { }
        public void OnSalir() { }
    }
}
