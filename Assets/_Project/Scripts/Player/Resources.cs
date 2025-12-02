using System;

namespace Game.Player
{
    public static class Resources
    {
        public static int Gold { get; set; }
        public static int Food { get; set; }
        public static int PeopleSatisfaction { get; set; }
        public static int CastleStrength { get; set; }

        public static event Action<int> GoldChanged;
        public static event Action<int> FoodChanged;
        public static event Action<int> PeopleSatisfactionChanged;
        public static event Action<int> CastleStrengthChanged;

        public static void ChangeGold(int delta) { 
            Gold = Math.Max(Gold + delta, 0);
            GoldChanged?.Invoke(Gold);
        }

        public static void ChangeFood(int delta) { 
            Food = Math.Max(Food + delta, 0);
            FoodChanged?.Invoke(Food);

        }

        public static void ChangePeopleSatisfaction(int delta) {
            PeopleSatisfaction = Math.Max(PeopleSatisfaction + delta, 0);
            PeopleSatisfactionChanged?.Invoke(PeopleSatisfaction);
        }

        public static void ChangeCastleStrength(int delta) { 
            CastleStrength = Math.Max(CastleStrength + delta, 0);
            CastleStrengthChanged?.Invoke(CastleStrength);

        }

        public static bool IsAnyResourceZero() {
            return Gold <= 0 || Food <= 0 || PeopleSatisfaction <= 0 ||
                CastleStrength <= 0;
        }

        public static bool IsAllResourcesZero() {
            return Gold <= 0 && Food <= 0 && PeopleSatisfaction <= 0 &&
                CastleStrength <= 0;
        }

        public static bool IsAnyResourceBiggerThenAmount(int amount) {
            return Gold >= amount ||
                    Food >= amount ||
                    PeopleSatisfaction >= amount ||
                    CastleStrength >= amount;
        }
    }    
}
