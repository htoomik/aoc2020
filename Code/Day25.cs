namespace aoc2020.Code
{
    public class Day25
    {
        public long Solve(long cardPublicKey, long doorPublicKey)
        {
            var cardValue = 1L;
            var doorValue = 1L;

            var cardLoop = 0;
            var cardPrivateKey = 20201227;
            while (true)
            {
                cardLoop++;
                cardValue *= 7;
                cardValue %= cardPrivateKey;

                if (cardValue == cardPublicKey)
                {
                    break;
                }
            }

            var encryptionKey = 1L;
            for (var i = 0; i < cardLoop; i++)
            {
                encryptionKey *= doorPublicKey;
                encryptionKey %= cardPrivateKey;
            }

            return encryptionKey;
        }
    }
}