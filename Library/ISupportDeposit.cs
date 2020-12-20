namespace Library
{
    public interface ISupportDeposit
    {
        void StartDeposit(int playerId, decimal amount, string currency);
    }
}