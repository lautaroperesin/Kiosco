using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Interfaces
{
    public interface IFormState
    {
        void OnBuscar();
        void OnAgregar();
        void OnEditar();
        void OnEliminar();
        void OnCancelar();
        void OnGuardar();
        void OnSalir();
        Task UpdateUI();
    }
}
