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
    public class EditState : IFormState
    {
        private ProductosView _form;

        public EditState(ProductosView form)
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
                MessageBox.Show("El nombre de la producto es obligatorio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _form.productoCurrent.Nombre = _form.txtNombre.Text;
            _form.productoCurrent.Precio = _form.numericPrecio.Value;

            await _form.productoService.UpdateAsync(_form.productoCurrent);

            _form.SetState(_form.initialDisplayState);
            await _form.currentState.UpdateUI();
        }

        public Task UpdateUI()
        {
            _form.productoCurrent = _form.dataGridProductos.CurrentRow?.DataBoundItem as Producto;
            _form.txtNombre.Text = _form.productoCurrent.Nombre;
            _form.numericPrecio.Value = _form.productoCurrent.Precio;

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
