using Bidding;

namespace Conventions {
    class Conventions {
        private static readonly List<string> TwoOverOne = new List<string>() {
            "1m opening allows 5M", "1NT opening NT style", "1NT opening range 15-17", "1NT opening shape 5422", "1M-3M inviting", "1N-2S transfer to clubs", "1N-3C transfer to diamonds", "1X-(Y)-2Z forcing", "1X-(1Y)-2Z strong", "Blackwood 0314", "Cappelletti", "Cue bid", "DOPI", "Fit showing jumps", "Forcing 1NT", "Fourth suit game force", "Gambling", "Gerber", "Imposible 2S", "Inverted minors", "Jacoby 2NT", "Jordan Truscott 2NT", "King ask by 5NT", "Lebensohl after 1NT", "Lebensohl after double", "Maximal Doubles", "Michaels Cuebid", "Minor Suit Stayman after 2NT", "Mixed raise", "Natural 3N entering style", "New Minor Forcing", "Quantitative 4NT", "Responsive double", "Reverse drury", "ROPI", "Shape Bergen structure", "SMOLEN", "Soloway Jump Shifts", "Splinter", "Super acceptance after NT", "Support double redouble", "Texas", "Two suit takeout double", "Two way game tries", "Unusual 1NT", "Unusual 2NT", "Weak natural 2D", "Weak natural 2M"
        };
        private static readonly List<string> SAYC = new List<string>() {
            "1m opening allows 5M", "1NT opening NT style", "1NT opening range 15-17", "1NT opening shape 5422", "1M-3M inviting", "1N-2S transfer to clubs", "1N-3C transfer to diamonds", "1X-(Y)-2Z forcing", "1X-(1Y)-2Z weak", "5NT pick a slam", "Bergen", "Blackwood 1430", "Cue bid", "DOPI", "Exclusion", "Fit showing jumps", "Fourth suit", "Gambling", "Gerber", "Imposible 2S", "Inverted minors", "Jacoby 2NT", "Jordan Truscott 2NT", "King ask by 5NT", "Leaping Michaels", "Lebensohl after 1NT", "Lebensohl after double", "Maximal Doubles", "Michaels Cuebid", "Minor Suit Stayman after 2NT", "Mixed raise", "Multi-Landy", "Natural 3N entering style", "Ogust", "Quantitative 4NT", "Responsive double", "ROPI", "SMOLEN", "Splinter", "Strength Lawrence structure", "Super acceptance after NT", "Support double redouble", "Texas", "Two suit takeout double", "Two way game tries", "Two Way New Minor Forcing", "Unusual 1NT", "Unusual 2NT", "Unusual 4NT", "Weak natural 2D", "Weak natural 2M"
        };
        private static readonly List<string> Acol = new List<string>() {
            "1m opening allows 5M", "1NT opening natural", "1NT opening range 12-14", "1M-3M inviting", "1N-2S transfer to clubs", "1N-3C transfer to diamonds", "1X-(Y)-2Z forcing", "1X-(1Y)-2Z weak", "5NT pick a slam", "Blackwood 0314", "BROMAD", "Cappelletti", "Crosswood 0314", "Cue bid", "DOPI", "Drury", "Exclusion", "Fit showing jumps", "Fourth suit", "Gambling", "Gerber", "Gerber only for NT openings", "Jacoby 2NT", "Jordan Truscott 2NT", "King ask by 5NT", "Leaping Michaels", "Maximal Doubles", "Michaels Cuebid", "Minor Suit Transfers after 2NT", "Mixed raise", "Natural 3N entering style", "New Minor Forcing", "Quantitative 4NT", "Responsive double", "ROPI", "Rubensohl after 1NT", "Rubensohl after 1m", "Rubensohl after double", "Soloway Jump Shifts", "Splinter", "Strength Lawrence structure", "Super acceptance after NT", "Support double redouble", "Texas", "Two suit takeout double", "Two way game tries", "Unusual 1NT", "Unusual 2NT", "Unusual 4NT", "Weak natural 2D"
        };
        private static readonly List<string> WJ = new List<string>() {
            "1D opening with 5 cards", "1NT opening NT style", "1NT opening range 15-17", "1NT opening shape 5422", "1M-3M blocking", "1N-2S transfer to clubs", "1N-3C transfer to diamonds", "1X-(1Y)-2Z weak", "5NT pick a slam", "Blackwood 0314", "BROMAD", "Crosswood 0314", "Cue bid", "DOPI", "Drury", "Exclusion", "Fit showing jumps", "Fourth suit", "Gambling", "Gerber", "Inverted minors", "Jacoby 2NT", "Jordan Truscott 2NT", "King ask by 5NT", "Leaping Michaels", "Maximal Doubles", "Michaels Cuebid", "Minor Suit Transfers after 2NT", "Mixed raise", "Multi-Landy", "Namyats", "Natural 3N entering style", "Quantitative 4NT", "Responsive double", "Reverse Bergen", "ROPI", "Rubensohl after 1NT", "Rubensohl after 1m", "Rubensohl after double", "SMOLEN", "Splinter", "Strength Lawrence structure", "Super acceptance after NT", "Support 1NT", "Support double redouble", "Texas", "Two suit takeout double", "Two way game tries", "Two Way New Minor Forcing", "Unusual 1NT", "Unusual 2NT", "Unusual 4NT", "Weak natural 2D", "Weak natural 2M"
        };
        private static readonly Dictionary<Bidding.System, List<string>> ActiveConventions = new Dictionary<Bidding.System, List<string>>() {
            {Bidding.System.TwoOverOne, TwoOverOne},
            {Bidding.System.SAYC, SAYC},
            {Bidding.System.Acol, Acol},
            {Bidding.System.WJ, WJ},
            {Bidding.System.Precision, new List<string>()} // Jonathan Yue, who plays Precision, says that we don't need any conventions over and above what BBA bids.
        };
        public static bool GetConvention(Bidding.System system, string convention) {
            return ActiveConventions[system].Contains(convention);
        }
    }
}