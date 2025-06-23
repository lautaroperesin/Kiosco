using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Interfaces;
using Service.Services;

namespace Desktop.States.Productos
{
    public class InitialDisplayState : IFormState
    {
        private ProductosView _form;
        public InitialDisplayState(ProductosView form) 
        {
            _form = form;
        }

        public async void OnBuscar()
        {
            await UpdateUI();
        }

        public void OnSalir()
        {
            _form.Close();
        }

        public async Task UpdateUI()
        {
            _form.listaProductos.DataSource = await _form.productoService.GetAllAsync();
            _form.dataGridProductos.DataSource = _form.listaProductos;
            _form.tabControl.SelectTab(_form.tabPageLista);
            _form.tabControl.Selecting += (sender, e) =>
            {
                if (e.TabPage == _form.tabPageAgregarEditar && (_form.currentState == _form.addState|| _form.currentState == _form.editState))
                {
                    e.Cancel = true;
                }
            };
        }

        public void OnAgregar() {}
        public void OnGuardar(){}
        public void OnEditar() {}
        public void OnEliminar() {}
        public void OnCancelar(){}
    }
}