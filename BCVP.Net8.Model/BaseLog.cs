using SqlSugar;
using System;

namespace BCVP.Net8.Model
{
    public abstract class BaseLog : RootEntityTkey<long>
    {
        [SplitField]
        public DateTime? DateTime { get; set; }

        [SugarColumn(IsNullable = true)]
        public string Level { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string Message { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string MessageTemplate { get; set; }

        [SugarColumn(IsNullable = true, ColumnDataType = "longtext,text,clob")]
        public string Properties { get; set; }
    }
}