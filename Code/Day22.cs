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
            var cleaned = input.Replace("\r\n", "\n").Trim();
            var sections = cleaned.Split("\n\n");
            var deck1 = ParseDeck(sections[0]);
            var deck2 = ParseDeck(sections[1]);

            var (_, winningDeck) = Play2(deck1, deck2);
            var score = Score(winningDeck);
            return score;
        }

        private (int winner, IEnumerable<int> winningDeck) Play2(Queue<int> deck1, Queue<int> deck2)
        {
            var previousStates1 = new HashSet<string>();
            var previousStates2 = new HashSet<string>();

            while (true)
            {
                if (!deck1.Any())
                {
                    return (2, deck2);
                }
                if (!deck2.Any())
                {
                    return (1, deck1);
                }

                var state1 = string.Join(",", deck1);
                var state2 = string.Join(",", deck2);

                if (previousStates1.Contains(state1) ||
                    previousStates2.Contains(state2))
                {
                    return (1, deck1);
                }

                previousStates1.Add(state1);
                previousStates2.Add(state2);

                var card1 = deck1.Dequeue();
                var card2 = deck2.Dequeue();

                var left1 = deck1.Count;
                var left2 = deck2.Count;

                int winner;
                if (card1 > left1 ||
                    card2 > left2)
                {
                    winner = card1 > card2 ? 1 : 2;
                }
                else
                {
                    var newDeck1 = CreateQueue(deck1.Take(card1));
                    var newDeck2 = CreateQueue(deck2.Take(card2));

                    var (innerWinner, _) = Play2(newDeck1, newDeck2);
                    winner = innerWinner;
                }

                if (winner == 1)
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

        private int Score(IEnumerable<int> deck)
        {
            var list = deck.ToList();
            var i = list.Count;
            return list.Sum(card => card * i--);
        }

        private static Queue<int> ParseDeck(string input)
        {
            var cards = input.Split("\n").Skip(1).Select(int.Parse);
            return CreateQueue(cards);
        }

        private static Queue<int> CreateQueue(IEnumerable<int> cards)
        {
            var q1 = new Queue<int>();
            foreach (var card in cards)
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