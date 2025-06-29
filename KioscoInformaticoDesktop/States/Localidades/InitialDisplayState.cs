﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Interfaces;
using KioscoInformaticoDesktop.Views;
using Service.Interfaces;
using Service.Services;

namespace Desktop.States.Localidades
{
    public class InitialDisplayState : IFormState
    {
        private LocalidadesView _form;
        public InitialDisplayState(LocalidadesView form) 
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
            _form.listaLocalidades.DataSource = await _form.localidadService.GetAllAsync(_form.txtFiltro.Text);
            _form.dataGridLocalidades.DataSource = _form.listaLocalidades;
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