namespace ADO.NET
{
    public static class SqlQueries
    {
        public const string GetAllVillainsAndCountTheirMinions = 
            @"  SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                  FROM Villains AS v 
                  JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
              GROUP BY v.Id, v.Name 
                HAVING COUNT(mv.VillainId) > 3 
              ORDER BY COUNT(mv.VillainId)";

        public const string GetVillainNameById = @"SELECT Name FROM Villains WHERE Id = @Id";

        public const string GetAllMinionsByVillainId = 
            @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) AS RowNum,
                                         m.Name, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId =                          m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";
    }

    
}
