using Boardgames.DataProcessor.ImportDto;
using System.Xml.Serialization;
using static Boardgames.DataProcessor.ExportDto.ExportBoardgame;

namespace Boardgames.DataProcessor.ExportDto
{
    public class ExportCreator
    {
        [XmlType("Creator")]
        public class ExportCreatorDto
        {
            [XmlAttribute("BoardgamesCount")]
            public int BoardgamesCount { get; set; }

            [XmlElement("CreatorName")]
            public string CreatorName { get; set; } = null!;

            [XmlArray("Boardgames")]
            public ExportBoardgameDto[] Boardgames { get; set; }
        }
    }
}
