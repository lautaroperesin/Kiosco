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

namespace Desktop.States.Productos
{
    public class AddState : IFormState
    {
        private ProductosView _form;

        public AddState(ProductosView form)
        {
            _form = form;
        }

        public void OnCancelar()
        {
            _form.txtNombre.Clear();
            _form.numericPrecio.Value = 0;
            _form.SetState(_form.initialDisplayState);
            _form.currentState.UpdateUI();
        }

        public async void OnGuardar()
        {
            if (string.IsNullOrEmpty(_form.txtNombre.Text))
            {
                MessageBox.Show("El nombre del producto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var producto = new Producto
            {
                Nombre = _form.txtNombre.Text,
                Precio = _form.numericPrecio.Value
            };
            await _form.productoService.AddAsync(producto);

            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            _form.txtNombre.Text = string.Empty;
            _form.numericPrecio.Value = 0;

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
