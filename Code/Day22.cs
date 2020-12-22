using System.Collections.Generic;
using System.Linq;

namespace aoc2020.Code
{
    public class Day22
    {
        public int Solve(string input)
        {
            var cleaned = input.Replace("\r\n", "\n").Trim();
            var sections = cleaned.Split("\n\n");
            var deck1 = ParseDeck(sections[0]);
            var deck2 = ParseDeck(sections[1]);

            var winner = Play(deck1, deck2);
            var score = Score(winner);
            return score;
        }

        public int Solve2(string input)
        {
            return 0;
        }

        private int Score(IEnumerable<int> deck)
        {
            var list = deck.ToList();
            var i = list.Count();
            return list.Sum(card => card * i--);
        }

        private static Queue<int> ParseDeck(string input)
        {
            var deck1 = input.Split("\n").Skip(1).Select(int.Parse);

            var q1 = new Queue<int>();
            foreach (var card in deck1)
            {
                q1.Enqueue(card);
            }

            return q1;
        }

        private IEnumerable<int> Play(Queue<int> deck1, Queue<int> deck2)
        {
            while (true)
            {
                if (!deck1.Any())
                {
                    return deck2;
                }
                if (!deck2.Any())
                {
                    return deck1;
                }

                var card1 = deck1.Dequeue();
                var card2 = deck2.Dequeue();
                if (card1 > card2)
                {
                    deck1.Enqueue(card1);
                    deck1.Enqueue(card2);
                }
                else
                {
                    deck2.Enqueue(card2);
                    deck2.Enqueue(card1);
                }
            }
        }
    }
}