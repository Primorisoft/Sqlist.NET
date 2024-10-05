﻿namespace Sqlist.NET.Sql
{
    public class NpgsqlEnclosure : Enclosure
    {
        public const char DI = '\"';

        public override string? Wrap(string? val)
        {
            return DI + val + DI;
        }

        public override string? Replace(string? val)
        {
            return val?.Replace('`', DI);
        }
    }
}
