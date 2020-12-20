namespace Library
{
    public interface ISupportWithdrawal
    {
        void StartWithdrawal(int playerId, decimal amount, string currency);
    }
}