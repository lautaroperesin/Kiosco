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
    public class DeleteState : IFormState
    {
        private ClientesView _form;

        public DeleteState(ClientesView form)
        {
            _form = form;
        }

        public void OnAgregar() {}

        public void OnBuscar() {}

        public void OnCancelar() {}

        public void OnEditar() {}

        public async void OnEliminar()
        {
            var cliente = (Cliente)_form.listaClientes.Current;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar la cliente {cliente.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await _form.clienteService.DeleteAsync(cliente.Id);
                _form.SetState(_form.initialDisplayState);
                await _form.currentState.UpdateUI();
            }
            else
                _form.SetState(_form.initialDisplayState);
           
        }

        public void OnGuardar() {}

        public void OnSalir() {}

        public Task UpdateUI()
        {
            return Task.CompletedTask;
        }
    }
}
