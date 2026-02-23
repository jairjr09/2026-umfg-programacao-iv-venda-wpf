using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using umfg.venda.app.Interfaces;

namespace umfg.venda.app.Abstracts
{
    internal abstract class AbstractViewModel : AbstractNotifyPropertyChange, ISubject
    {
        private readonly ICollection<IObserver> _observers = [];
        private string _titulo = string.Empty;

        public string Titulo 
        {
            get => _titulo;
            set => SetField(ref _titulo, value);
        }

        protected AbstractViewModel(string titulo)
        {
            Titulo = titulo;
        }

        public void Add(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Remove(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            throw new NotImplementedException();
        }
    }
}
