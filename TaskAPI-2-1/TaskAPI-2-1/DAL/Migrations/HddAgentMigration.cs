using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentMigrator;

namespace TaskAPI_2_1.DAL.Migrations
{
    [Migration(3)]
    public class HddAgentMigration:Migration
    {
        public override void Down()
        {
            Delete.Table("hddagentmetrics");
        }

        public override void Up()
        {
            Create.Table("hddagentmetrics")
                 .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                 .WithColumn("agentId").AsInt32()
                 .WithColumn("Value").AsInt32()
                 .WithColumn("Time").AsInt64();
        }
    }
}
