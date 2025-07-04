﻿namespace CompanyAPI.Application.Command
{
    public abstract class CompanyCommandBase
    {
        public required string Isin { get; set; }
        public required string Name { get; set; }
        public required string Exchange { get; set; }
        public required string Ticker { get; set; }
        public string? Website { get; set; }
    }
}
