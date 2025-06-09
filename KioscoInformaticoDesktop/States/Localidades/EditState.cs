using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;

namespace Desktop.States.Localidades
{
    public class EditState : IFormState
    {
        private LocalidadesView _form;

        public EditState(LocalidadesView form)
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
                MessageBox.Show("El nombre de la localidad es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _form.localidadCurrent.Nombre = _form.txtNombre.Text;
            await _form.localidadService.UpdateAsync(_form.localidadCurrent);

            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
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
