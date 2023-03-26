namespace P02_FootballBetting.Data.Models.Enums
{
    // Enum are not entities in the DB
    // Enum are string representation of int values
    // In the DB -> int
    public enum Prediction
    {
        Draw = 0,
        Win = 1,
        Lose = 2
    }
}
