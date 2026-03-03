using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using umfg.venda.app.Interfaces;

namespace umfg.venda.app.Abstracts
{
    internal abstract class AbstractViewModel : AbstractNotifyPropertyChange, ISubject
    {
        private readonly ICollection<IObserver> _observers = [];

        private string _titulo = string.Empty;

        public UserControl UserControl { get; protected set; }
        
        public IObserver MainWindow { get; protected set; }

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
            _observers.Add(observer);
        }

        public void Remove(IObserver observer)
        {
            throw new NotImplementedException();
        }

        public void Notify()
        {
            foreach (var observer in _observers)
                observer.Update(this);
        }
    }
}
