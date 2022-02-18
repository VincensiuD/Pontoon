using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pontoon.Data
{
    public class DataSeeder
    {

        public DataSeeder(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Card>()
            //    .HasData(
            //        new Card { Id = 1, Suit = "Heart", Rank = "A", Value = 1, Colour = "RED", DisplayCode = "HeartA"},
            //        new Card { Id = 2, Suit = "Heart", Rank = "2", Value = 2, Colour = "RED", DisplayCode = "Heart2" },
            //        new Card { Id = 3, Suit = "Heart", Rank = "3", Value = 3, Colour = "RED", DisplayCode = "Heart3" },
            //        new Card { Id = 4, Suit = "Heart", Rank = "4", Value = 4, Colour = "RED", DisplayCode = "Heart4" },
            //        new Card { Id = 5, Suit = "Heart", Rank = "5", Value = 5, Colour = "RED", DisplayCode = "Heart5" },
            //        new Card { Id = 6, Suit = "Heart", Rank = "6", Value = 6, Colour = "RED", DisplayCode = "Heart6" },
            //        new Card { Id = 7, Suit = "Heart", Rank = "7", Value = 7, Colour = "RED", DisplayCode = "Heart7" },
            //        new Card { Id = 8, Suit = "Heart", Rank = "8", Value = 8, Colour = "RED", DisplayCode = "Heart8" },
            //        new Card { Id = 9, Suit = "Heart", Rank = "9", Value = 9, Colour = "RED", DisplayCode = "Heart9" },
            //        new Card { Id = 10, Suit = "Heart", Rank = "10", Value = 10, Colour = "RED", DisplayCode = "Heart10" },
            //        new Card { Id = 11, Suit = "Heart", Rank = "J", Value = 10, Colour = "RED", DisplayCode = "HeartJ" },
            //        new Card { Id = 12, Suit = "Heart", Rank = "Q", Value = 10, Colour = "RED", DisplayCode = "HeartQ" },
            //        new Card { Id = 13, Suit = "Heart", Rank = "K", Value = 10, Colour = "RED", DisplayCode = "HeartK" },
            //        new Card { Id = 14, Suit = "Spade", Rank = "A", Value = 1, Colour = "BLK", DisplayCode = "SpadeA" },
            //        new Card { Id = 15, Suit = "Spade", Rank = "2", Value = 2, Colour = "BLK", DisplayCode = "Spade2" },
            //        new Card { Id = 16, Suit = "Spade", Rank = "3", Value = 3, Colour = "BLK", DisplayCode = "Spade3" },
            //        new Card { Id = 17, Suit = "Spade", Rank = "4", Value = 4, Colour = "BLK", DisplayCode = "Spade4" },
            //        new Card { Id = 18, Suit = "Spade", Rank = "5", Value = 5, Colour = "BLK", DisplayCode = "Spade5" },
            //        new Card { Id = 19, Suit = "Spade", Rank = "6", Value = 6, Colour = "BLK", DisplayCode = "Spade6" },
            //        new Card { Id = 20, Suit = "Spade", Rank = "7", Value = 7, Colour = "BLK", DisplayCode = "Spade7" },
            //        new Card { Id = 21, Suit = "Spade", Rank = "8", Value = 8, Colour = "BLK", DisplayCode = "Spade8" },
            //        new Card { Id = 22, Suit = "Spade", Rank = "9", Value = 9, Colour = "BLK", DisplayCode = "Spade9" },
            //        new Card { Id = 23, Suit = "Spade", Rank = "10", Value = 10, Colour = "BLK", DisplayCode = "Spade10" },
            //        new Card { Id = 24, Suit = "Spade", Rank = "J", Value = 10, Colour = "BLK", DisplayCode = "SpadeJ" },
            //        new Card { Id = 25, Suit = "Spade", Rank = "Q", Value = 10, Colour = "BLK", DisplayCode = "SpadeQ" },
            //        new Card { Id = 26, Suit = "Spade", Rank = "K", Value = 10, Colour = "BLK", DisplayCode = "SpadeK" },
            //        new Card { Id = 27, Suit = "Diamond", Rank = "A", Value = 1, Colour = "RED", DisplayCode = "DiamondA" },
            //        new Card { Id = 28, Suit = "Diamond", Rank = "2", Value = 2, Colour = "RED", DisplayCode = "Diamond2" },
            //        new Card { Id = 29, Suit = "Diamond", Rank = "3", Value = 3, Colour = "RED", DisplayCode = "Diamond3" },
            //        new Card { Id = 30, Suit = "Diamond", Rank = "4", Value = 4, Colour = "RED", DisplayCode = "Diamond4" },
            //        new Card { Id = 31, Suit = "Diamond", Rank = "5", Value = 5, Colour = "RED", DisplayCode = "Diamond5" },
            //        new Card { Id = 32, Suit = "Diamond", Rank = "6", Value = 6, Colour = "RED", DisplayCode = "Diamond6" },
            //        new Card { Id = 33, Suit = "Diamond", Rank = "7", Value = 7, Colour = "RED", DisplayCode = "Diamond7" },
            //        new Card { Id = 34, Suit = "Diamond", Rank = "8", Value = 8, Colour = "RED", DisplayCode = "Diamond8" },
            //        new Card { Id = 35, Suit = "Diamond", Rank = "9", Value = 9, Colour = "RED", DisplayCode = "Diamond9" },
            //        new Card { Id = 36, Suit = "Diamond", Rank = "10", Value = 10, Colour = "RED", DisplayCode = "Diamond10" },
            //        new Card { Id = 37, Suit = "Diamond", Rank = "J", Value = 10, Colour = "RED", DisplayCode = "DiamondJ" },
            //        new Card { Id = 38, Suit = "Diamond", Rank = "Q", Value = 10, Colour = "RED", DisplayCode = "DiamondQ" },
            //        new Card { Id = 39, Suit = "Diamond", Rank = "K", Value = 10, Colour = "RED", DisplayCode = "DiamondK" },
            //        new Card { Id = 40, Suit = "Club", Rank = "A", Value = 1, Colour = "BLK", DisplayCode = "ClubA" },
            //        new Card { Id = 41, Suit = "Club", Rank = "2", Value = 2, Colour = "BLK", DisplayCode = "Club2" },
            //        new Card { Id = 42, Suit = "Club", Rank = "3", Value = 3, Colour = "BLK", DisplayCode = "Club3" },
            //        new Card { Id = 43, Suit = "Club", Rank = "4", Value = 4, Colour = "BLK", DisplayCode = "Club4" },
            //        new Card { Id = 44, Suit = "Club", Rank = "5", Value = 5, Colour = "BLK", DisplayCode = "Club5" },
            //        new Card { Id = 45, Suit = "Club", Rank = "6", Value = 6, Colour = "BLK", DisplayCode = "Club6" },
            //        new Card { Id = 46, Suit = "Club", Rank = "7", Value = 7, Colour = "BLK", DisplayCode = "Club7" },
            //        new Card { Id = 47, Suit = "Club", Rank = "8", Value = 8, Colour = "BLK", DisplayCode = "Club8" },
            //        new Card { Id = 48, Suit = "Club", Rank = "9", Value = 9, Colour = "BLK", DisplayCode = "Club9" },
            //        new Card { Id = 49, Suit = "Club", Rank = "10", Value = 10, Colour = "BLK", DisplayCode = "Club10" },
            //        new Card { Id = 50, Suit = "Club", Rank = "J", Value = 10, Colour = "BLK", DisplayCode = "ClubJ" },
            //        new Card { Id = 51, Suit = "Club", Rank = "Q", Value = 10, Colour = "BLK", DisplayCode = "ClubQ" },
            //        new Card { Id = 52, Suit = "Club", Rank = "K", Value = 10, Colour = "BLK", DisplayCode = "ClubK" }

            //    );

            modelBuilder.Entity<CardSequence>()
                .HasData(
                new CardSequence {Id = 1, OwnerName = "Dealer" },
                new CardSequence { Id = 2, OwnerName = "Player" },
                new CardSequence { Id = 3, OwnerName = "PlayerSplit1"},
                new CardSequence { Id = 4, OwnerName = "PlayerSplit2"}
                
                );


            //modelBuilder.Entity<Wager>()
            //   .HasData(
            //   new Wager { Id = 1, PairsWager = 0, MainWager = 0 }
            //   );

            //modelBuilder.Entity<Wallet>()
            //    .HasData(
            //    new Wallet { Id = 1, Money = 300 }
            //    );

        }

    }

}
