using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace TaskAPI_2_1.DAL.Migrations
{
    [Migration(2)]
    public class DotNetAgentMigration:Migration
    {        
        public override void Down()
        {
            Delete.Table("dotnetagentmetrics");
        }

        public override void Up()
        {
            Create.Table("dotnetagentmetrics")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("agentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
        }
    }
}
