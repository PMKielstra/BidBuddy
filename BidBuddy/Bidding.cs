using BlazorBootstrap;
using EPBot64;
using Microsoft.AspNetCore.StaticFiles;

namespace Bidding {
    public enum Suit {
        Clubs,
        Diamonds,
        Hearts,
        Spades,
        Notrump
    }

    public enum Position {
        North,
        East,
        South,
        West
    }
    public enum Vulnerability {
        Neither,
        EW,
        NS,
        Both
    }

    public enum System {
        TwoOverOne,
        SAYC,
        WJ,
        Precision,
        Acol
    }

    public abstract record Bid;
    public record Pass: Bid;
    public record Double: Bid;
    public record Redouble: Bid;
    public record Raise(int level, Suit suit): Bid;
    public class AuctionCheck {
        private static bool OpponentsJustDid<bidType>(List<Bid> auction) {
            return auction.Count > 0 &&
                    (auction.Last() is bidType
                    || (
                        auction.Count > 2 && (
                            auction.Last() is Pass
                            && auction[^2] is Pass
                            && auction[^3] is bidType
                        )
                    ));
        }
        public static bool Allowed(Bid bid, List<Bid> auction) {
            List<Raise> raises = auction.OfType<Raise>().ToList();
            return bid switch {
                Pass => true,
                Raise(int newLevel, Suit newSuit) => (
                    raises.Count == 0
                    || raises.Last().level < newLevel
                    || raises.Last().level == newLevel && raises.Last().suit < newSuit    
                ),
                Double => OpponentsJustDid<Raise>(auction),
                Redouble => OpponentsJustDid<Double>(auction),
                _ => false
            };
            
        }
        public static string BidName(Bid bid) {
            return bid switch {
                Bidding.Pass => "Pass",
                Bidding.Double => "X",
                Bidding.Redouble => "XX",
                Bidding.Raise(int level, Suit suit) => level.ToString() + (suit switch {
                    Suit.Clubs => "♣",
                    Suit.Diamonds => "♦",
                    Suit.Hearts => "♥",
                    Suit.Spades => "♠",
                    Suit.Notrump => "NT"
                })
            };
        }
    }
    public class Hand {
        public Position seat;
        public Position dealer;
        public Vulnerability vulnerability;
        public List<(Suit, int)> cards;
        public Hand(Position seat, Position dealer, Vulnerability vulnerability, List<(Suit, int)> cards) {
            this.seat = seat;
            this.dealer = dealer;
            this.vulnerability = vulnerability;
            this.cards = cards;
        }

        private readonly string[] cardNames = {"2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "A"};

        private string SuitString(Suit suit) {
            return cards.Where(card => card.Item1 == suit)
                        .Select(card => card.Item2)
                        .Order()
                        .Select(x => cardNames[x])
                        .Aggregate((cur, next) => next + cur); // Reverse the order so that higher cards end up on top
        }

        public string[] CardArray() {
            return new[] {SuitString(Suit.Clubs), SuitString(Suit.Diamonds), SuitString(Suit.Hearts), SuitString(Suit.Spades)};
        }

    }
    public class Bidder {
        private System system;
        private List<string> conventions;
        private bool IMPs;
        private Hand hand;
        
        public List<Bid> auction { get; } = [];
        public Bidder(Hand hand, bool IMPs, System system, List<String> conventions) {
            this.IMPs = IMPs;
            this.hand = hand;
            this.system = system;
            this.conventions = conventions;
        }

        public void AddBid(Bid bid) {
            this.auction.Append(bid);
        }

        private static int EPBidID(Bid bid) => bid switch {
            Pass => 0,
            Double => 1,
            Redouble => 2,
            Raise(int level, Suit suit) => 4 * level + (int) suit + 1,
            _ => throw new Exception()
        };

        public Bid MakeBid() {
            if (((int) this.hand.dealer + this.auction.Count()) % 4 != (int) this.hand.seat) return null;

            EPBot bot = new();

            var cards = this.hand.CardArray();
            bot.new_hand((int) this.hand.seat, ref cards, (int) this.hand.dealer, (int) this.hand.vulnerability);
            bot.scoring = this.IMPs ? 1 : 0;
            
            foreach (int seat in new[] {0, 1}) {
                bot.set_system_type(seat, (int) this.system);
                foreach (string convention in this.conventions) {
                    bot.set_conventions(seat, convention, true);
                }
            }

            var bid_array = this.auction.Select(bid => EPBidID(bid).ToString("00")).ToArray();
            bot.set_arr_bids(ref bid_array);

            int new_bid = bot.get_bid();
            
            Bid result = new_bid switch {
                0 => new Pass(),
                1 => new Double(),
                2 => new Redouble(),
                int x => new Raise((x - 1 - (x - 1) % 4) / 4, (Suit) ((x - 1) % 4))
            };

            this.AddBid(result);

            return result;
        }

        public static List<string> SupportedConventions() {
            EPBot bot = new EPBot();
            return Enumerable.Range(0, 255).Select(bot.get_convention_name).Where(n => n.Equals("Not defined")).ToList();
        }

    }
}