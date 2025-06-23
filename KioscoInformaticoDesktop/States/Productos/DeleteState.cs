using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Models;

namespace Desktop.States.Productos
{
    public class DeleteState : IFormState
    {
        private ProductosView _form;

        public DeleteState(ProductosView form)
        {
            _form = form;
        }

        public void OnAgregar() {}

        public void OnBuscar() {}

        public void OnCancelar() {}

        public void OnEditar() {}

        public async void OnEliminar()
        {
            var producto = (Producto)_form.listaProductos.Current;
            var result = MessageBox.Show($"¿Está seguro que desea eliminar la producto {producto.Nombre}?", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                await _form.productoService.DeleteAsync(producto.Id);
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
