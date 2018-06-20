namespace OrderApp.Service.Taxations
{
    public abstract class Taxation
    {
        public abstract string Name { get; }

        public abstract decimal Percentual { get; }

        public abstract decimal Apply(decimal amount);
    }
}
