using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;

namespace Desktop.States.Localidades
{
    public class AddState : IFormState
    {
        private LocalidadesView _form;

        public AddState(LocalidadesView form)
        {
            _form = form;
        }

        public void OnCancelar()
        {
        }

        public void OnGuardar()
        {
        }

        public Task UpdateUI()
        {
            _form.tabControl.SelectTab(_form.tabPageAgregarEditar);
            return Task.CompletedTask;
        }

        public void OnAgregar() {}
        public void OnEditar() {}
        public void OnBuscar() {}
        public void OnEliminar() {}
        public void OnSalir() {}
    }
}
