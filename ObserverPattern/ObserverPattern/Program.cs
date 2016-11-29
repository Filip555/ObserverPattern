using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ObserverPattern
{
    public interface ISubject
    {
        int currentPrice { get; set; }

        void addObserver(IObserver o);
        void deleteObserver(IObserver o);
        void tellObserver();
    }
    public interface IObserver
    {
        void updateData();
    }
    public class PriceList : ISubject
    {
        //list of objects that will observe the objects of this class
        private List<IObserver> _listOfObservers = new List<IObserver>();

        public int currentPrice { get; set; }

        public void addObserver(IObserver o)
        {
            _listOfObservers.Add(o);
        }

        public void deleteObserver(IObserver o)
        {
            _listOfObservers.Remove(o);
        }
        public void tellObserver()
        {
            foreach (var item in _listOfObservers)
            {
                item.updateData();
            }
        }
        public int changePrice(int newPrice)
        {
            return currentPrice = newPrice;
        }
    }
    class User : IObserver
    {
        //private field for storing the current price
        private int _currentPrice;
        //reference to observed object
        private ISubject priceList;
        //user name
        private string _name;
        public User(string name, ISubject subject)
        {
            priceList = subject;
            _name = name;
        }
        public void updateData()
        {
            _currentPrice = priceList.currentPrice;
            Console.WriteLine("\nHey {0}, price has just changed to {1} PLN", _name, _currentPrice);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            PriceList pl = new PriceList();
            User u1 = new User("Pawel", pl);
            User u2 = new User("Sabina", pl);

            // adding observers
            pl.addObserver(u1);
            pl.addObserver(u2);

            // change/set current price
            pl.changePrice(20);
            pl.tellObserver();

            // remove one of observers and change price
            pl.deleteObserver(u2);
            pl.changePrice(15);
            pl.tellObserver();

            Console.ReadKey();
        }
    }
}
