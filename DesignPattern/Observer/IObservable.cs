namespace DesignPattern.Observer
{
    internal interface IObservable
    {
        void Add(IObserver observer);
        void Remove(IObserver observer);
        void Notify();
        double GetTemprature(double buffer);
    }
}