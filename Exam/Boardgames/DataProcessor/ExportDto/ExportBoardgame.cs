using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportBoardgame
    {
        [XmlType("Boardgame")]
        public class ExportBoardgameDto
        {
            [XmlElement("BoardgameName")]
            public string BoardgameName { get; set; } = null!;

            [XmlElement("BoardgameYearPublished")]
            public int BoardgameYearPublished { get; set; }
        }
    }
}